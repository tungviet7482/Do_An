using Demo.Application.Interface;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public FileController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpPost]
        [RequestSizeLimit(1_073_741_824)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var res = await _mediaService.DownloadFile(file);
            if (res != null)
                return Ok(new Result<MediaFileModel>
                {
                    Status = true,
                    Data = res
                });
            return BadRequest(new Result<object>
            {
                Status = false,
                Message = "Lỗi tải lên",
            });
        }
    }
}
