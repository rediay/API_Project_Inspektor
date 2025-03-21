using System.Threading.Tasks;
using Common.DTO.Users;
using Common.Entities;
using Common.Services.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers
{
    [Route("additional-services")]
    public class AdditionalServiceController : BaseApiController
    {
        private readonly IAdditionalCompanyServiceService _additionalCompanyServiceService;

        public AdditionalServiceController(IAdditionalCompanyServiceService additionalCompanyServiceService)
        {
            _additionalCompanyServiceService = additionalCompanyServiceService;
        }

        [HttpGet]
        [Route("companies/{companyId:int}")]
        public async Task<IActionResult> GetAll(int companyId)
        {
            var response = await _additionalCompanyServiceService.GetAll(companyId);
            return Ok(response);
        }
        
        [HttpPut]
        [Route("companies/{companyId:int}")]
        [Authorize]
        public async Task<IActionResult> Edit(int companyId, AdditionalCompanyService dto)
        {
            if (companyId != dto.CompanyId)
                return BadRequest();

            var result = await _additionalCompanyServiceService.Edit(companyId, dto);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}