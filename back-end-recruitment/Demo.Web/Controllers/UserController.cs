using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.filter;
using Demo.Domain.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Demo.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserModelLoginValidator _validatorLogin;
        private readonly UserModelValidator _validatorModel;
        private readonly IJobService _jobService;

        public UserController(IUserService userService, UserModelLoginValidator validatorLogin, UserModelValidator validationModel, IJobService jobService)
        {
            _userService = userService;
            _validatorLogin = validatorLogin;
            _validatorModel = validationModel;
            this._jobService = jobService;
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetPagingUser([FromQuery] FilterUser? filter)
        {
            var resultObject = await _userService.GetPagingAsync("user",filter);

            return Ok(new Result<DataObject<UserModel>>
            {
                Status = true,
                Data = resultObject
            });
        }

        [HttpGet]
        [Route("companies")]
        public async Task<IActionResult> GetPagingCompany([FromQuery] FilterUser page)
        {
            var resultObject = await _userService.GetPagingAsync("company", page);

            return Ok(new Result<DataObject<UserModel>>
            {
                Status = true,
                Data = resultObject
            });
        }

        [HttpGet]
        [Route("admin/get-paging")]
        public async Task<IActionResult> GetPagingAdmin([FromQuery] FilterUser? page)
        {
            var resultObject = await _userService.GetPagingAsync("admin", page);

            return Ok(new Result<DataObject<UserModel>>
            {
                Status = true,
                Data = resultObject
            });
        }

        [HttpPost]
        [Route("candidate-classification")]
        public async Task<IActionResult> CandidateClassification([FromBody] Job_UserModel model)
        {
            if(model.JobId == 0)
            {
                return Ok();
            }    
            var user = await GetCurrentAccount();
            if (user == null)
            {
                return NotFound(new Result<Object>
                {
                    Status = false,
                    Message = "Tài khoản không tồn tại"
                });
            }

            var data = await _jobService.GetPagingAsync(new FilterJob { PageIndex = 0, JobId = model.JobId });

            if(data.Items.FirstOrDefault() == null || data.Items.FirstOrDefault().Id != model.JobId)
            {
                return BadRequest(new Result<Object>
                {
                    Status = false,
                    Message = "Tài khoản không có quyền thực hiện chức năng này"
                });
            }

            try
            {
                var res = await _userService.UpdateJobUser(model);
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
            });
        }

        [HttpGet]
        [Route("get-candidates-apllied")]
        public async Task<IActionResult> GetCandidatesAppliedPaging([FromQuery] FilterCandidate filter)
        {
            var resultObject = await _userService.GetCandidatesAppliedPaging(filter);

            return Ok(new Result<DataObject<UserModel>>
            {
                Status = true,
                Data = resultObject
            });
        }

        [HttpGet]
        [Route("get-candidates")]
        public async Task<IActionResult> GetCandidatesPaging([FromQuery] FilterCandidate filter)
        {
            var resultObject = await _userService.GetCandidatesPaging(filter);

            return Ok(new Result<DataObject<UserModel>>
            {
                Status = true,
                Data = resultObject
            });
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUserById(int id)
        {

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new Result<Object>
                {
                    Status = false,
                    Message = "User không tồn tại"
                });
            }
            return Ok(new Result<UserModel>
            {
                Status = true,
                Data = user
            }) ;
        }

        [HttpGet]
        [Route("user-infor")]
        [Authorize(Roles = "admin,user,company")]
        public async Task<IActionResult> GetUserInfor()
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
            return Ok(new Result<UserModel>
            {
                Status = true,
                Data = user
            });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserModel model)
        {
            var validator = _validatorLogin.Validate(model);
            if (!validator.IsValid)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = "Đăng nhập thất bại",
                    Error = new BaseException(400, "", "", validator.Errors)
                });
            }
            try
            {
                var token = await _userService.Authenticate(model);
                var roles = await _userService.GetRolesAsync(model);
                Dictionary<string, object> res = new Dictionary<string, object>();
                res.Add("token", token);
                res.Add("role", roles);

                if (!token.IsNullOrEmpty())
                    return Ok(new Result<Dictionary<string, object>>
                    {
                        Status = true,
                        Message = "Đăng nhập thành công",
                        Data = res 
                    });
                return Ok(new Result<object>
                {
                    Status = false,
                    Message = "Đăng nhập thất bại"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = "Đăng nhập thất bại",
                    Error = new BaseException(400, "", "", ex.Message)
                });
            }
           
        }

        [HttpPost]
        [Route("upload-cv/{fileCVId}")]
        [Authorize(Roles = "user")]
        [RequestSizeLimit(1_073_741_824)]
        public async Task<IActionResult> UploadCV(long fileCVId)
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
            user.FileCVId = fileCVId;
            var success = await _userService.Update(user.Id ,user);
            if (success)
                return Ok(new Result<MediaFileModel>
                {
                    Status = true,
                    Message = "Tải lên CV thành công",
                });
            return BadRequest(new Result<object>
            {
                Status = false,
                Message = "Lỗi tải lên",
            });
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok(new Result<string>
            {
                Status = true,
                Message = "Đăng xuất",
            });
        }

        [HttpPost("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserModel model)
        {
            var validator = _validatorModel.Validate(model);
            if (!validator.IsValid)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = "Đăng ký thất bại",
                    Error = new BaseException(400, "", "", validator.Errors)
                });
            }
            try
            {
                var isSuccess = await _userService.Register(model);
                if (isSuccess)
                    return Ok(new Result<Object>
                    {
                        Status = true,
                        Message = "Đăng ký thành công"
                    });
                return Ok(new Result<Object>
                {
                    Status = false,
                    Message = "Đăng ký thất bại"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = "Đăng ký thất bại",
                    Error = new BaseException(400, "", "", ex.Message)
                });
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserModel model)
        {
            var validator = _validatorModel.Validate(model);
            if (!validator.IsValid)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = "Cập nhật thất bại",
                    Error = new BaseException(400, "", "", validator.Errors)
                });
            }
            try
            {
                var isSuccess = await _userService.Update(id, model);
                if (isSuccess)
                    return Ok(new Result<Object>
                    {
                        Status = true,
                        Message = "Cập nhật thành công"
                    });
                return Ok(new Result<Object>
                {
                    Status = false,
                    Message = "Cập nhật thất bại"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = "Cập nhật thất bại",
                    Error = new BaseException(400, "", "", ex.Message)
                });
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var isSuccess = await _userService.Delete(id);
                if (isSuccess)
                {
                    return Ok(new Result<Object>
                    {
                        Status = true,
                        Message = "Xóa thành công"
                    });
                }

                return Ok(new Result<Object>
                {
                    Status = false,
                    Message = "Cập nhật thất bại"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = "Xóa thất bại",
                    Error = new BaseException(400, "", "", ex.Message)
                });
            }
        }

 
        private async Task<UserModel?> GetCurrentAccount()
        {
            var id = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            return await _userService.GetUserByIdAsync(id); 
        }
    }
}
