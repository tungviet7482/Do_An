using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.filter;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.Web.Controllers
{
    [Route("api/jobs")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IUserService _userService;
        private readonly IValidator<JobModel> _validator;

        public JobController(IJobService jobService, IValidator<JobModel> validator, IUserService userService)
        {
            _jobService = jobService;
            _validator = validator;
            _userService = userService;
        }

        [HttpPost]
        [Route("save-job/{jobId}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> SaveJob(int jobId)
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
            var model = new Job_UserModel
            {
                JobId = jobId,
                UserId = user.Id
            };
            var res = await _userService.SaveJobAsync(model);
            if (res != null)
            {
                return Ok(new Result<object>
                {
                    Status = true,
                    Message = res ?? false ? "Đã thêm vào mục theo dõi" : "Đã xóa khỏi mục theo dõi"
                });
            }

            return BadRequest(new Result<object>
            {
                Status = false,
                Message = "Lỗi! Không thể theo dõi",
            });
        }

        [HttpGet]
        [Route("get-saved")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetSavedJobPaging([FromQuery] FilterJob filter)
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
            var resultObject = await _userService.getSavedJobsPagingAsync(user.Id, filter);

            return Ok(new Result<DataObject<JobModel>>
            {
                Status = true,
                Data = resultObject
            });
        }

        [HttpGet]
        [Route("get-applied")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetAppliedJobPaging([FromQuery] FilterJob filter)
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
            var resultObject = await _userService.getAppliedJobsPagingAsync(user.Id, filter);

            return Ok(new Result<DataObject<JobModel>>
            {
                Status = true,
                Data = resultObject
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetJobsPaging([FromQuery] FilterJob filter)
        {
            var dataObject = await _jobService.GetPagingAsync(filter);

            return Ok(new Result<DataObject<JobModel>>
            {
                Status = true,
                Data = dataObject
            });
        }

        [HttpGet]
        [Route("expiry_date/{slug}")]
        public async Task<IActionResult> GetJobBySlug(string slug)
        {
            var job = await _jobService.GetJobBySlug(slug);

            return Ok(new Result<JobModel>
            {
                Status = true,
                Data = job
            });
        }

        [HttpPost]
        [Route("apply-job/{jobId}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> ApplyJob(int jobId)
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
            var model = new Job_UserModel
            {
                JobId = jobId,
                UserId = user.Id,
                Applied = true,
            };
            try
            {
                var res = await _userService.ApplyJob(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = ex.Message,
                });
            }
            return Ok(new Result<object>
            {
                Status = true,
                Message = "Ứng tuyển thành công"
            });
        }

        //[HttpGet]
        //[Route("expired_date")]
        //public async Task<IActionResult> getExpiredJobsPaging([FromQuery] FilteringRequest page)
        //{
        //    var dataObject = await _jobService.GetPagingAsync(page, Domain.Enum.JobStatus.EXPIRED);

        //    return Ok(new Result<DataObject<JobModel>>
        //    {
        //        Status = true,
        //        Data = dataObject
        //    });
        //}

        [HttpPost]
        [Authorize(Roles = "company,admin")]
        public async Task<IActionResult> Create([FromBody] JobModel model)
        {
            var currentAcount = await GetCurrentAccount();
            if (currentAcount.Roles[0] == "company")
            {
                model.UserId = currentAcount.Id;
                model.Published = false;
            }
            var validatorResult = await _validator.ValidateAsync(model);
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
                var isSuccess = await _jobService.CreateAsync(model);
                return Ok(new Result<object>
                {
                    Status = true,
                    Message = "Thêm mới thành công"
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
        [Route("{id}")]
        [Authorize(Roles = "company,admin")]
        public async Task<IActionResult> Update(int id, [FromBody] JobModel model)
        {
            var validatorResult = await _validator.ValidateAsync(model);
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
                var data = await _jobService.GetPagingAsync(new FilterJob { PageIndex = 0, JobId = model.Id });

                if (data.Items.FirstOrDefault() == null || data.Items.FirstOrDefault().Id != model.Id)
                {
                    return BadRequest(new Result<Object>
                    {
                        Status = false,
                        Message = "Tài khoản không có quyền thực hiện chức năng này"
                    });
                }
                var isSuccess = await _jobService.UpdateAsync(id, model);

                return Ok(new Result<object>
                {
                    Status = isSuccess,
                    Message = "Cập nhật thành công"
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
        [Authorize(Roles = "company,admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccessed = await _jobService.DeleteAsync(id);
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
