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
            try
            {
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            try
            {
                return Ok(await _fileService.ReadFileAsync(fileName));
            }
            catch (Exception exception)
            {
                throw new ArgumentNullException(exception.Message);
            }
        }

    }
}
