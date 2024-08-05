using FileServer_Asp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FileServer_Asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileRegisterController : ControllerBase
    {
        private readonly IFileRegisterService _fileRegisterService;

        public FileRegisterController(IFileRegisterService fileRegisterService)
        {
            _fileRegisterService = fileRegisterService;
        }

        [HttpGet("ViewBySecretName")]
        public async Task<IActionResult> ViewBySecretName(string secret)
        {
            try
            {
                return Ok(await _fileRegisterService.ViewFileViaSecretName(secret));
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                return BadRequest(exception.Message);
            }
        }
    }
}
