using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.ThirdPartyProfiling;
using Common.Entities;

namespace Common.Services.Infrastructure.Services.ThirdPartyProfiling
{
    public interface IParameterizeVariableService
    {
        Task<PagedResponseDTO<List<RiskProfileVariableDTO>>> GetAllRiskProfileVariables(
            PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        Task<PagedResponseDTO<List<CategoryVariableDTO>>> GetAllCategoryVariables(
            PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        Task<PagedResponseDTO<List<CategoryVariableDTO>>> GetAllCategoryVariablesByRiskProfileVariableId(
            PaginationFilterDTO paginationFilterDto, int riskProfileVariableId, int personType,
            bool includeDeleted = false);

        Task<ResponseDTO<RiskProfileVariableDTO>> EditRiskProfileVariable(RiskProfileVariableDTO dto);

        Task<ResponseDTO<CategoryVariableDTO>> EditCategoryVariable(CategoryVariableDTO dto);
        
        Task<PagedResponseDTO<List<RiskProfileDTO>>> GetAllRiskProfiles(
            PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        
        Task<ResponseDTO<RiskProfileDTO>> EditRiskProfile(RiskProfileDTO dto);
    }
}