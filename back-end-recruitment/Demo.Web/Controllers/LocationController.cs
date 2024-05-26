using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly LocationModelValidator _locationModelValidator;

        public LocationController(LocationModelValidator locationModelValidator, ILocationService locationService)
        {
            _locationModelValidator = locationModelValidator;
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var locationsModel = await _locationService.GetAllAsync();
            var total = locationsModel?.Count();

            return Ok(new Result<DataObject<LocationModel>>
            {
                Status = true,
                Data = new DataObject<LocationModel>(locationsModel, total ?? 0)
            });
        }


        [HttpGet]
        [Route("get-paging")]
        public async Task<IActionResult> GetPaging([FromQuery] BaseFilter page)
        {
            var resultObject = await _locationService.GetPagingAsync(page);

            return Ok(new Result<DataObject<LocationModel>>
            {
                Status = true,
                Data = resultObject
            });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] LocationModel model)
        {
            var validator = _locationModelValidator.Validate(model);
            if (!validator.IsValid)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = "Thêm mới thất bại",
                    Error = new BaseException(400, null, null, validator.Errors)
                });
            }
            var isSuccess = await _locationService.CreateAsync(model);
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
        public async Task<IActionResult> Update(int id, [FromBody] LocationModel model)
        {
            var validatorResult = await _locationModelValidator.ValidateAsync(model);
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
                var isSuccess = await _locationService.UpdateAsync(id, model);

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
            var isSuccessed = await _locationService.Delete(id);
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
    }
}
