using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Application.Service;
using Demo.Domain.Common;
using Demo.Domain.DataObejct;
using Demo.Domain.filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.Web.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly CommentModelValidator _commentModelValidator;
        private readonly IUserService _userService;

        public CommentController(ICommentService commentService, CommentModelValidator commentModelValidator, IUserService userService)
        {
            _commentService = commentService;
            _commentModelValidator = commentModelValidator;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging([FromQuery] BaseFilter page)
        {
            var resultObject = await _commentService.GetPagingAsync(page);

            return Ok(new Result<DataComment<CommentModel>>
            {
                Status = true,
                Data = resultObject
            });
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Create([FromBody] CommentModel model)
        {
            var user = await GetCurrentAccount();
            if (user == null)
            {
                return NotFound(new Result<Object>
                {
                    Status = false,
                    Message = "User không tồn tại"
                });
            }

            model.UserId = user.Id;

            var validator = _commentModelValidator.Validate(model);
            if (!validator.IsValid)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = "Tạo đánh giá thất bại",
                    Error = new BaseException(400, null, null, validator.Errors)
                });
            }
            try
            {
                var isSuccess = await _commentService.CreateAsync(model);
                if (isSuccess)
                {
                    return Ok(new Result<object>
                    {
                        Status = true,
                        Message = "Tạo thành công. Chờ kiểm duyệt nội dung"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = ex.Message,
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
        public async Task<IActionResult> Update(int id, [FromBody] CommentModel model)
        {
            var validatorResult = await _commentModelValidator.ValidateAsync(model);
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
                var isSuccess = await _commentService.UpdateAsync(id, model);

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
            var isSuccessed = await _commentService.Delete(id);
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

        private async Task<UserModel?> GetCurrentAccount()
        {
            var id = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            return await _userService.GetUserByIdAsync(id);
        }
    }
}
