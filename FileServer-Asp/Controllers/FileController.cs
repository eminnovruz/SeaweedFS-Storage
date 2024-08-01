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

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileModel fileToUpload)
        {
            try
            {
                return Ok(await _fileService.UploadFileAsync(fileToUpload));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        [HttpGet("Download/{fid}")]
        public async Task<IActionResult> DownloadFile(string fid)
        {
            try
            {
                return Ok(await _fileService.ReadFileAsync(fid));
            }
            catch (Exception exception)
            {
                throw new ArgumentNullException(exception.Message);
            }
        }

        [HttpDelete("Delete/{fid}")]
        public async Task<IActionResult> RemoveFile(string fid)
        {
            try
            {
                return Ok(await _fileService.RemoveFileAsync(fid));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

    }
}
