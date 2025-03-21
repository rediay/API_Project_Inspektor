using Common.DTO;
using Common.Services.Infrastructure.Management;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Common.Services.Infrastructure.Services;
using Common.Entities;
using Common.Services.Management;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Common.WebApiCore.Controllers.Management
{
    [Route("Company")]
    public class CompanyController : BaseApiController
    {
        protected readonly ICompanyService _companyService;
        private readonly IAdditionalCompanyServiceService _additionalCompanyServiceService;

        public CompanyController(ICompanyService companyService, IAdditionalCompanyServiceService additionalCompanyServiceService)
        {
            _companyService = companyService;
            _additionalCompanyServiceService = additionalCompanyServiceService;
        }

        [HttpGet]
        [Route(nameof(CompanyController.GetCompany))]
        public async Task<IActionResult> GetCompany()
        {
            var company = await _companyService.GetCompany();
            return Ok(company);
        }

        [HttpGet]
        [Route(nameof(CompanyController.GetAllCompanies))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _companyService.GetAllCompanies();
            return Ok(companies);
        }

        [HttpPost]
        [Route(nameof(CompanyController.UpdateCompany))]
        [ValidateIdCompany]
        public async Task<IActionResult> UpdateCompany(CompanyDTO companyDTO)
        {
            var result = await _companyService.UpdateCompany(companyDTO);

            if (result.Id != companyDTO.Id)
                return BadRequest();

            return Ok(result);


        }
        [HttpPost]
        [Route(nameof(CompanyController.AddCompany))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> AddCompany(CompanyDTO companyDTO)
        {
            if (companyDTO.Id != null)
                return BadRequest();

            var result = await _companyService.UpdateCompany(companyDTO);
            var companyId = result.Id;

            if (companyId != null)
            {
                await _additionalCompanyServiceService.Create((int) companyId);
            }

            
            return Ok(result);


        }

        [HttpPost]
        [Route(nameof(CompanyController.DeleteCompany))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> DeleteCompany(string CompanyId)
        {

            bool result = await _companyService.DeleteCompany(Convert.ToInt32(CompanyId));

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }


}

