using Microsoft.AspNetCore.Mvc;
using Shared.Project.Entities;

namespace Server.Repositories.Contracts
{
    public interface IFileService
    {
        Task<IActionResult> DownloadFile(string fileName);
        Task<ActionResult<List<UploadResult>>> UploadFile(List<IFormFile> files);
    }
}
