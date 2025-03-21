using AutoMapper;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Management;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Common.Services.Management
{
    public class PlanService : BaseService, IPlanService
    {
        private readonly IMapper _mapper;
        protected readonly IPlanRepository _planRepository;
        public PlanService(ICurrentContextProvider contextProvider, IPlanRepository planRepository, IMapper mapper) : base(contextProvider)
        {
            this._planRepository = planRepository;
            _mapper = mapper;
        }


        public async Task<List<PlanDTO>> GetPlans()
        {
            var plans = await _planRepository.GetPlans(Session);
            var map = plans.MapTo<List<PlanDTO>>();
            return map;
        }


        public async Task<PlanDTO> UpdatePlan(PlanDTO planDTO)
        {
            try
            {
                var plan = planDTO.MapTo<Plan>();
                
                plan.UserId = Session.UserId;
                
                var planDTOs = await _planRepository.Edit(plan, Session);
                return planDTOs.MapTo<PlanDTO>();
            }
            catch (Exception ex)
            {
                return await new Task<PlanDTO>(null);
            }
        }

        public async Task<PlanDTO> GetPlanById(int id)
        {
            var plans = await _planRepository.Get(id, Session);
            var map = plans.MapTo<PlanDTO>();
            return map;
        }

        public async Task<bool> DeletePlan(int id)
        {
            await _planRepository.Delete(id, Session);
            return true;
        }
    }
}
