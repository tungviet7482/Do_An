using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Application.Service;
using Demo.Domain.Common;
using Demo.Domain.filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly NewsModelValidator _newsModelValidator;

        public NewsController(INewsService newsService, NewsModelValidator newsModelValidator)
        {
            _newsService = newsService;
            _newsModelValidator = newsModelValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging([FromQuery] FilterNews page)
        {
            var resultObject = await _newsService.GetPagingAsync(page);

            return Ok(new Result<DataObject<NewsModel>>
            {
                Status = true,
                Data = resultObject
            });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] NewsModel model)
        {
            var validator = _newsModelValidator.Validate(model);
            if (!validator.IsValid)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = "Thêm mới thất bại",
                    Error = new BaseException(400, null, null, validator.Errors)
                });
            }
            var isSuccess = await _newsService.CreateAsync(model);
            if (isSuccess)
            {
                return Ok(new Result<object>
                {
                    Status = true,
                    Message = "Thêm mới thành công"
                });
            }

            return BadRequest(new Result<object>
            {
                Status = false,
                Message = "Thêm mới thất bại",
            });
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(int id, [FromBody] NewsModel model)
        {
            var validatorResult = await _newsModelValidator.ValidateAsync(model);
            if (!validatorResult.IsValid)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Error = new BaseException(400, "", "", validatorResult.Errors)
                });
            }

            try
            {
                var isSuccess = await _newsService.UpdateAsync(id, model);

                return Ok(new Result<object>
                {
                    Status = isSuccess,
                    Message = isSuccess ? "Cập nhật thành công" : "Cập nhật thất bại"
                });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Result<object>
                {
                    Status = false,
                    Error = new BaseException(500, ex.Message, "Hệ thống đang gặp sự cố vui lòng thử lại sau", null)
                });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccessed = await _newsService.Delete(id);
            if (isSuccessed)
            {
                return Ok(new Result<object>
                {
                    Status = true,
                    Message = "Xóa thành công"
                });
            }

            return BadRequest(new Result<object>
            {
                Status = false,
                Message = "Xóa thất bại",
            });
        }

        [HttpGet]
        [Route("{slug}")]
        public async Task<IActionResult> GetNewsBySlug(string slug)
        {
            var news = await _newsService.GetNewsBySlug(slug);

            return Ok(new Result<NewsModel>
            {
                Status = true,
                Data = news
            });
        }
    }
}
