using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.ThirdPartyProfiling;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.ThirdPartyProfiling;
using Common.Services.Infrastructure.Services.ThirdPartyProfiling;
using Common.Utils;

namespace Common.Services.ThirdPartyProfiling
{
    public class ParameterizeVariableService : BaseService, IParameterizeVariableService
    {
        private float _TOTAL_WEIGHT_LIMIT = 100;
        private readonly IRiskProfileVariableRepository _riskProfileVariableRepository;
        private readonly ICategoryVariableRepository _categoryVariableRepository;
        private readonly IRiskProfileRepository _riskProfileRepository;
        private readonly IUserService _userService;

        public ParameterizeVariableService(ICurrentContextProvider contextProvider,
            IRiskProfileVariableRepository riskProfileVariableRepository,
            ICategoryVariableRepository categoryVariableRepository,
            IRiskProfileRepository riskProfileRepository,
            IUserService userService
        ) : base(contextProvider)
        {
            _riskProfileVariableRepository = riskProfileVariableRepository;
            _categoryVariableRepository = categoryVariableRepository;
            _riskProfileRepository = riskProfileRepository;
            _userService = userService;
        }

        public async Task<PagedResponseDTO<List<RiskProfileVariableDTO>>> GetAllRiskProfileVariables(
            PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse =
                await _riskProfileVariableRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            var list = pagedResponse.Data.Select(data => data.MapTo<RiskProfileVariableDTO>()).ToList();
            return pagedResponse.CopyWith(list);
        }

        public async Task<PagedResponseDTO<List<CategoryVariableDTO>>> GetAllCategoryVariables(
            PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _categoryVariableRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            var list = pagedResponse.Data.Select(data => data.MapTo<CategoryVariableDTO>()).ToList();
            return pagedResponse.CopyWith(list);
        }

        public async Task<PagedResponseDTO<List<CategoryVariableDTO>>> GetAllCategoryVariablesByRiskProfileVariableId(
            PaginationFilterDTO paginationFilterDto, int riskProfileVariableId, int personType,
            bool includeDeleted = false)
        {
            var pagedResponse =
                await _categoryVariableRepository.GetAllCategoryVariablesByRiskProfileVariableId(Session,
                    paginationFilterDto, riskProfileVariableId, personType, includeDeleted);
            var list = pagedResponse.Data.Select(data => data.MapTo<CategoryVariableDTO>()).ToList();
            return pagedResponse.CopyWith(list);
        }

        public async Task<ResponseDTO<RiskProfileVariableDTO>> EditRiskProfileVariable(RiskProfileVariableDTO dto)
        {
            var riskProfileVariable = await _riskProfileVariableRepository.Get(dto.Id, Session, true);

            if (riskProfileVariable == null)
            {
                throw new Exception("Variable no encontrada");
            }

            var totalWeight = await _riskProfileVariableRepository.GetTotalWeight(Session, riskProfileVariable.Id);

            totalWeight += dto.Weight;

            if (totalWeight > _TOTAL_WEIGHT_LIMIT)
            {
                throw new Exception(
                    $"La suma total de los pesos de las variables esta excediendo el limite total de {_TOTAL_WEIGHT_LIMIT}");
            }

            var riskProfileVariableData = dto.MapTo<RiskProfileVariable>();
            var newRiskProfileVariable =
                await _riskProfileVariableRepository.Edit(riskProfileVariableData, Session);
            var newRiskProfileVariableMapped = newRiskProfileVariable.MapTo<RiskProfileVariableDTO>();
            var response = new ResponseDTO<RiskProfileVariableDTO>(newRiskProfileVariableMapped);

            return response;
        }

        public async Task<ResponseDTO<CategoryVariableDTO>> EditCategoryVariable(CategoryVariableDTO dto)
        {
            var currentUserId = Session.UserId;
            var currentUser = await _userService.GetById(currentUserId);
            var companyId = currentUser.CompanyId;
            
            dto.CompanyId = companyId;
            
            var categoryVariable = await _categoryVariableRepository.Get(dto.Id, Session, true);

            if (categoryVariable != null)
            {
                var categoryVariableData = dto.MapTo<CategoryVariable>();
                var newRiskProfileVariable = await _categoryVariableRepository.Edit(categoryVariableData, Session);
                var newRiskProfileVariableMapped = newRiskProfileVariable.MapTo<CategoryVariableDTO>();
                var response = new ResponseDTO<CategoryVariableDTO>(newRiskProfileVariableMapped);

                return response;
            }

            throw new System.NotImplementedException();
        }

        public async Task<PagedResponseDTO<List<RiskProfileDTO>>> GetAllRiskProfiles(PaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var pagedResponse =
                await _riskProfileRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            var list = pagedResponse.Data.Select(data => data.MapTo<RiskProfileDTO>()).ToList();
            return pagedResponse.CopyWith(list);
        }

        public async Task<ResponseDTO<RiskProfileDTO>> EditRiskProfile(RiskProfileDTO dto)
        {
            var riskProfile = await _riskProfileRepository.Get(dto.Id, Session, true);

            if (riskProfile != null)
            {
                var currentUserId = Session.UserId;
                var currentUser = await _userService.GetById(currentUserId);
                var companyId = currentUser.CompanyId;
            
                dto.CompanyId = companyId;
                
                var riskProfileData = dto.MapTo<RiskProfile>();
                var newRiskProfileVariable = await _riskProfileRepository.Edit(riskProfileData, Session);
                var newRiskProfileVariableMapped = newRiskProfileVariable.MapTo<RiskProfileDTO>();
                var response = new ResponseDTO<RiskProfileDTO>(newRiskProfileVariableMapped);

                return response;
            }

            throw new System.NotImplementedException();
        }
    }
}