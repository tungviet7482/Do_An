using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Application.Service;
using Demo.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace Demo.Web.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies([FromQuery]BaseFilter filter)
        {
            var dataObject = await _companyService.GetPagingAsync(filter);

            return Ok(new Result<DataObject<CompanyModel>>
            {
                Status = true,
                Data = dataObject
            });
        }

        [HttpGet]
        [Route("{slug}")]
        public async Task<IActionResult> GetCompanyBySlug(string slug)
        {
            var company = await _companyService.GetCompanyBySlug(slug);

            return Ok(new Result<CompanyModel>
            {
                Status = true,
                Data = company
            });
        }
    }
}
