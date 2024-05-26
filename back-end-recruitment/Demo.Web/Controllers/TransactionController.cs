using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly TransactionModelValidator _validatorModel;

        public TransactionController(ITransactionService transactionService, TransactionModelValidator validatorModel)
        {
            _transactionService = transactionService;
            _validatorModel = validatorModel;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _transactionService.GetAllAsync();
            var total = list.Count();

            return Ok(new Result<DataObject<TransactionModel>>
            {
                Status = true,
                Data = new DataObject<TransactionModel>(list, total)
            });
        }

        [HttpGet]
        [Route("get-paging")]
        public async Task<IActionResult> GetPaging([FromQuery] BaseFilter page)
        {
            var resultObject = await _transactionService.GetPagingAsync(page);

            return Ok(new Result<DataObject<UserTransactionModel>>
            {
                Status = true,
                Data = resultObject
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionModel model)
        {
            var validatorResult = await _validatorModel.ValidateAsync(model);
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
                var isSuccessed = await _transactionService.CreateAsync(model);
                if (isSuccessed)
                {
                    return Ok(new Result<object>
                    {
                        Status = true,
                        Message = "Thêm mới thành công"
                    });
                }
                return Ok(new Result<object>
                {
                    Status = false,
                    Message = "Thêm mới thất bại"
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
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccessed = await _transactionService.Delete(id);
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
