using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly CategoryModelValidator _categoryModelValidator;

        public CategoryController(ICategoryService categoryService, CategoryModelValidator categoryModelValidator)
        {
            _categoryService = categoryService;
            _categoryModelValidator = categoryModelValidator;
        }


        [HttpGet]
        public async Task<IActionResult> GetPaging([FromQuery]BaseFilter page)
        {
            var resultObject = await _categoryService.GetPagingAsync(page);

            return Ok(new Result<DataObject<CategoryModel>>
            {
                Status = true,
                Data = resultObject
            });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody]CategoryModel model)
        {
            var validator = _categoryModelValidator.Validate(model);
            if (!validator.IsValid)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = "Thêm mới thất bại",
                    Error = new BaseException(400, null, null, validator.Errors)
                });
            }
            var isSuccess = await _categoryService.CreateAsync(model);
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
        public async Task<IActionResult> Update(int id, [FromBody] CategoryModel model)
        {
            var validatorResult = await _categoryModelValidator.ValidateAsync(model);
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
                var isSuccess = await _categoryService.UpdateAsync(id, model);

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
            var isSuccessed = await _categoryService.Delete(id);
            if(isSuccessed)
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
    }
}
