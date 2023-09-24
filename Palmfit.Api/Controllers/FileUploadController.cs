using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Core.Implementations;
using Palmfit.Core.Services;

namespace Palmfit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {

        private readonly IFileUploadRepository _fileUploadRepository;

        public FileUploadController(IFileUploadRepository fileUploadRepository)
        {
            _fileUploadRepository = fileUploadRepository;
        }
        [HttpPost("image/FIle-upload")]
        public async Task<IActionResult> UploadImageOrFile(IFormFile file)
        {
            var uploadedImage = await _fileUploadRepository.UploadImageToCloudinaryAndSave(file);

            if (uploadedImage == null)
            {
                return NotFound(ApiResponse.Failed(uploadedImage));
            }
            return Ok(ApiResponse.Success(uploadedImage));
        }
    }
}
