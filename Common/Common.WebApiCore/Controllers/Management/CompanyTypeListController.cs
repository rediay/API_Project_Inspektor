using Common.DTO;
using Common.Services.Infrastructure.Management;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers.Management
{
    [Route("CompanyTypeList")]
    public class CompanyTypeListController : BaseApiController
    {
        protected readonly ICompanyTypeListService _companyTypeListService;
        public CompanyTypeListController(ICompanyTypeListService companyTypeListService)
        {
            this._companyTypeListService = companyTypeListService;
        }

        [HttpGet]
        [Route(nameof(CompanyTypeListController.GetTypeList))]
        public async Task<IActionResult> GetTypeList()
        {
            var companyTypeList = await _companyTypeListService.GetTypeList();
            return Ok(companyTypeList);
        }

        [HttpPost]
        [Route(nameof(CompanyTypeListController.UpdateTypeList))]
        [ValidateIdCompany]
        public async Task<IActionResult> UpdateTypeList(CompanyTypeListDTO typeListDTO)
        {
            bool result = await _companyTypeListService.UpdateTypeList(typeListDTO);
            if (result)
            {
             return Ok();
            }
            else
            {
               return BadRequest();
            }

        }

        [HttpPost]
        [Route(nameof(CompanyTypeListController.UpdateRangeTypeList))]
        public async Task<IActionResult> UpdateRangeTypeList(string status)
        {
            bool result = await _companyTypeListService.UpdateRangeTypeList(Convert.ToBoolean(status));
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

