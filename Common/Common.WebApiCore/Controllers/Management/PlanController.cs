using Common.DTO;
using Common.Services.Infrastructure.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Common.WebApiCore.Controllers.Management
{
    [Route("Plan")]
    public class PlanController : BaseApiController
    {
        protected readonly IPlanService _planService;
        public PlanController(IPlanService planService)
        {
            this._planService = planService;
        }



        [HttpGet]
        [Route(nameof(PlanController.GetPlans))]
        public async Task<IActionResult> GetPlans()
        {
            var plans = await _planService.GetPlans();
            return Ok(plans);
        }

        [HttpGet]
        [Route(nameof(PlanController.GetPlanById))]
        public async Task<IActionResult> GetPlanById (int id)
        {
            var plans = await _planService.GetPlanById(id);
            return Ok(plans);
        }

        [HttpPost]
        [Route(nameof(PlanController.UpdatePlan))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> UpdatePlan(PlanDTO planDTO)
        {
            var result = await _planService.UpdatePlan(planDTO);

            if (result.Id != planDTO.Id)
                return BadRequest();

            return Ok(result);


        }
        [HttpPost]
        [Route(nameof(PlanController.AddPlan))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> AddPlan(PlanDTO planDTO)
        {
            

            var result = await _planService.UpdatePlan(planDTO);
            if (result == null)
                return BadRequest(result);

            return Ok(result);


        }
        [HttpPost]
        [Route(nameof(PlanController.Delete))]
        [Authorize(Policy = "SuperAdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var plans = await _planService.DeletePlan(id);
            return Ok(plans);
        }

    }


}

