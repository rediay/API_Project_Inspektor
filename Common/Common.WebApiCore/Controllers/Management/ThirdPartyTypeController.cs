using Common.DTO;
using Common.Services.Infrastructure.Management;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers.Management
{
    [Route("ThirdPartyType")]
    public class ThirdPartyTypeController : BaseApiController
    {

        protected readonly IThirdPartyTypeService _thirdPartyTypeService;

        public ThirdPartyTypeController(IThirdPartyTypeService thirdPartyTypeService)
        {
            this._thirdPartyTypeService = thirdPartyTypeService;
        }

        [HttpGet]
        [Route(nameof(ThirdPartyTypeController.GetByCompanyID))]

        public async Task<IActionResult> GetByCompanyID()
        {
            var thirdPartyType = await _thirdPartyTypeService.GetByCompanyID();
            return Ok(thirdPartyType);
        }

        [HttpPost]
        [Route(nameof(ThirdPartyTypeController.UpdateThirdPartyType))]
        [ValidateIdCompany]
        public async Task<IActionResult> UpdateThirdPartyType(ThirdPartyTypeDTO thirdPartyTypeDTO)
        {

            var result = await _thirdPartyTypeService.UpdateThirdPartyType(thirdPartyTypeDTO);
            if (result != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route(nameof(ThirdPartyTypeController.CreateThirdPartyType))]
        [ValidateIdCompany]
        public async Task<IActionResult> CreateThirdPartyType(ThirdPartyTypeDTO thirdPartyTypeDTO)
        {

            var result = await _thirdPartyTypeService.UpdateThirdPartyType(thirdPartyTypeDTO);
            if (result!=null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Route(nameof(ThirdPartyTypeController.DeleteThirdPartyType))]
        public async Task<IActionResult> DeleteThirdPartyType(string ThirdPartyTypeId)
        {

            bool result = await _thirdPartyTypeService.DeleteThirdPartyType(Convert.ToInt32(ThirdPartyTypeId));

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
