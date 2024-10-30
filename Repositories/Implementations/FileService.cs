
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Project.Entities;
using Server.Repositories.Contracts;
using System;

namespace Server.Repositories.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

    private readonly AppDbContext _appDbContext;

    public FileService(IWebHostEnvironment env, AppDbContext appDbContext)
        {
            _env = env;
        _appDbContext = appDbContext;
    }
        

        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var uploadResult = await _appDbContext.Uploads.FirstOrDefaultAsync(u => u.StoredFileName.Equals(fileName));

            if (uploadResult == null)
            {
                return new NotFoundObjectResult("File not found.");
            }

            var path = Path.Combine(_env.ContentRootPath, "uploads", fileName);

            if (!System.IO.File.Exists(path))
            {
                return new NotFoundObjectResult("File not found.");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return new FileStreamResult(memory, uploadResult.ContentType) { FileDownloadName = Path.GetFileName(path) };
        }

       
        public async Task<ActionResult<List<UploadResult>>> UploadFile(List<IFormFile> files)
        {
            List<UploadResult> uploadResults = new List<UploadResult>();

            if (files == null || !files.Any())
            {
                return new BadRequestObjectResult("No files uploaded.");
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var uploadResult = new UploadResult();

                    // File name provided by the user (untrusted)
                    var untrustedFileName = file.FileName;
                    uploadResult.FileName = untrustedFileName;

                    // Create a trusted file name (sanitizing the untrusted name)
                    string trustedFileNameForFileStorage = Path.GetFileName(untrustedFileName);

                    // Define where to store the file on disk
                    var path = Path.Combine(_env.ContentRootPath, "uploads", trustedFileNameForFileStorage);

                    // Ensure the directory exists
                    var directory = Path.GetDirectoryName(path);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Save the file to disk
                    await using FileStream fs = new(path, FileMode.Create);
                    await file.CopyToAsync(fs);

                    // Set the file details for the result
                    uploadResult.StoredFileName = trustedFileNameForFileStorage;
                    uploadResult.ContentType = file.ContentType;
                    uploadResults.Add(uploadResult);
                }
            }

            // Return the result to the client
            return new OkObjectResult(uploadResults);
        }

    }
}
