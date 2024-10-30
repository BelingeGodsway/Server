using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Server.Data;
using Shared.Project.Entities;




namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class DirectoryController : ControllerBase
    {
        //private readonly IOptions<JwtSection> _config;
        private readonly AppDbContext _context;
        private static int _idCounter = 1;
        private readonly IWebHostEnvironment _env;

        public DirectoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        [HttpGet("root-directories")]
        public IActionResult GetRootDirectories()
        {
            var rootDirectories = _context.FileSystemEntities
                .Where(e => e.ParentId < 0 && !e.FileType) // Ensure ParentId is greater than 0 for root directories
                .ToList();

            return Ok(rootDirectories);
        }


        [HttpGet("all-directory-contents/{parentId}")]
        public IActionResult GetAllDirectoryContents(int parentId)
        {
            var allDirectories = GetAllDirectories(parentId);
            return Ok(allDirectories);
        }

        private List<FileSystemEntity> GetAllDirectories(int parentId)
        {
            var results = new List<FileSystemEntity>();

            // Fetch immediate children that are directories
            var children = _context.FileSystemEntities
                .Where(e => e.ParentId == parentId && !e.FileType) // Filter out files
                .ToList();

            results.AddRange(children);

            // Fetch descendants recursively for directories
            foreach (var child in children)
            {
                results.AddRange(GetAllDirectories(child.Id));
            }

            return results;
        }
        [HttpGet("directory-contents/{parentId}")]
        public IActionResult GetDirectoryContents(int parentId)
        {
            var contents = _context.FileSystemEntities
                .Where(e => e.ParentId == parentId)
                .ToList();

            return Ok(contents);
        }

        [HttpGet("preview/{fileName}")]
        public IActionResult GetFilePreview(string fileName)
        {
            // Retrieve the first file entity from the database using the file name
            var fileEntity = _context.FileSystemEntities.FirstOrDefault(f => f.Name == fileName);
            if (fileEntity == null)
            {
                Console.WriteLine("File not found in the database.");
                return NotFound("File not found in the database.");
            }
            //  var filePath = Path.Combine(_env.ContentRootPath, "files", fileName);
            var filepath = fileEntity.Path;

            // Ensure the filepath is provided and the file exists
            if (string.IsNullOrWhiteSpace(filepath) || !System.IO.File.Exists(filepath))
            {
                Console.WriteLine("The file does not exist on the server.");
                return NotFound("The file does not exist on the server.");
            }

            var fileExtension = Path.GetExtension(filepath).ToLowerInvariant();
            var mimeType = fileExtension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream",
            };

            var fileBytes = System.IO.File.ReadAllBytes(filepath);
            var fileBase64 = Convert.ToBase64String(fileBytes);

            var filePreviewResponse = new
            {
                MimeType = mimeType,
                Content = fileBase64
            };

            return Ok(filePreviewResponse);
        }
        private string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream"; // Default for unknown types
            }
            return contentType;
        }
    } 

}


