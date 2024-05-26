using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Domain.Common;
using Demo.Domain.filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    [Route("api/contactus")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _contactUsService;
        private readonly ContactUsModelValidator _contactUsModelValidator;

        public ContactUsController(IContactUsService contactUsService, ContactUsModelValidator contactUsModelValidator)
        {
            _contactUsService = contactUsService;
            _contactUsModelValidator = contactUsModelValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaging([FromQuery] FilterContactUs page)
        {
            var resultObject = await _contactUsService.GetPagingAsync(page);

            return Ok(new Result<DataObject<ContactUsModel>>
            {
                Status = true,
                Data = resultObject
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactUsModel model)
        {
            var validator = _contactUsModelValidator.Validate(model);
            if (!validator.IsValid)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = "Gửi liên hệ thất bại",
                    Error = new BaseException(400, null, null, validator.Errors)
                });
            }
            var isSuccess = await _contactUsService.CreateAsync(model);
            if (isSuccess)
            {
                return Ok(new Result<object>
                {
                    Status = true,
                    Message = "Gửi liên hệ thành công"
                });
            }

            return BadRequest(new Result<object>
            {
                Status = false,
                Message = "Gửi liên hệ thất bại",
            });
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ContactUsModel model)
        {
            var validatorResult = await _contactUsModelValidator.ValidateAsync(model);
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
                var isSuccess = await _contactUsService.UpdateAsync(id, model);

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

        [HttpPut]
        [Route("process-contacts")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ProcessContacts([FromBody] List<long> ids)
        {
            if(ids == null || ids.Count() == 0)
            {
                return BadRequest();
            }
            var isSuccessed = await _contactUsService.ProcessRangeAsync(ids);
            if (isSuccessed)
            {
                return Ok((new Result<object>
                {
                    Status = true
                }));
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccessed = await _contactUsService.Delete(id);
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
