using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FileServer_Asp.Models;
using FileServer_Asp.Services.Abstract;

namespace FileServer_Asp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileModel fileModel)
        {
            if (fileModel.File == null || fileModel.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var filePath = fileModel.File.FileName;
            using (var stream = new MemoryStream())
            {
                await fileModel.File.CopyToAsync(stream);
                var result = await _fileService.WriteFileAsync(filePath, stream.ToArray());

                if (result)
                {
                    return Ok(new { filePath });
                }
                else
                {
                    return StatusCode(500, "Error saving file.");
                }
            }
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var fileData = await _fileService.ReadFileAsync(fileName);

            if (fileData == null)
            {
                return NotFound("File not found.");
            }

            return File(fileData, "application/octet-stream", fileName);
        }
    }
}
