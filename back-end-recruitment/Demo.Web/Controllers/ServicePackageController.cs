using Demo.Application.Interface;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    [Route("api/service_packages")]
    [ApiController]
    public class ServicePackageController : ControllerBase
    {
        private readonly IServicePackageService _servicePackageService;
        private readonly ServicePackageModelValidator _servicePackageModelValidator;

        public ServicePackageController(IServicePackageService servicePackageService, ServicePackageModelValidator ervicePackageModelValidator)
        {
            _servicePackageService = servicePackageService;
            _servicePackageModelValidator = ervicePackageModelValidator;
        }


        [HttpGet]
        public async Task<IActionResult> GetALL()
        {
            var servicePkages = await _servicePackageService.GetAllAsync();
            var total = servicePkages.Count();

            return Ok(new Result<DataObject<ServicePackageModel>>
            {
                Status = true,
                Data = new DataObject<ServicePackageModel>(servicePkages, total)
            });
        }

        [HttpGet]
        [Route("get-paging")]
        public async Task<IActionResult> GetPaging([FromQuery] BaseFilter page)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServicePackageModel model)
        {
            var validator = _servicePackageModelValidator.Validate(model);
            if(!validator.IsValid)
            {
                return BadRequest(new Result<object>
                {
                    Status = false,
                    Message = "Thêm mới thất bại",
                    Error = new BaseException(400, null, null, validator.Errors)
                });
            } 
            var isSuccess = await _servicePackageService.CreateAsync(model);
            if(isSuccess)
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
        public async Task<IActionResult> Update(ServicePackageModel model)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccessed = await _servicePackageService.Delete(id);
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
