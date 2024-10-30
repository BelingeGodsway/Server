using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeController : ControllerBase
    {
        [HttpPost("SaveBarcode")]
        public async Task<IActionResult> SaveBarcode([FromBody] string barcodeText)
        {
            try
            {
                // Define the folder path where the file will be saved
                var folderPath = @"C:\bizsapp\Client\barcode";

                // Check if the directory exists, if not, create it
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string sanitizedFileName = string.Concat(barcodeText.Split(Path.GetInvalidFileNameChars()));

                // Combine folder path and file name to get the full file path
           
                var filePath = Path.Combine(folderPath, $"{sanitizedFileName}.txt");

                // Write the barcode text to the file
                await System.IO.File.WriteAllTextAsync(filePath, barcodeText);

                return Ok(new { Message = $"URL saved successfully to {filePath}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error saving URL: {ex.Message}" });
            }
        }
    }
}
