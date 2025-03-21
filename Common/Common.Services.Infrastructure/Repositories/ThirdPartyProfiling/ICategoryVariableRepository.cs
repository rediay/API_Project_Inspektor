using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.ThirdPartyProfiling
{
    public interface ICategoryVariableRepository
    {
        Task<PagedResponseDTO<List<CategoryVariable>>> GetAll(ContextSession session,
            PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        Task<PagedResponseDTO<List<CategoryVariable>>> GetAllCategoryVariablesByRiskProfileVariableId(
            ContextSession session, PaginationFilterDTO paginationFilterDto, int riskProfileVariableId,
            int personTypeId,
            bool includeDeleted = false);

        Task<CategoryVariable> Get(int id, ContextSession session, bool includeDeleted = false);

        Task<List<CategoryVariable>> Get(ContextSession session, int companyId, List<int> categoryIds, 
            bool includeDeleted = false);

        Task<CategoryVariable> Edit(CategoryVariable user, ContextSession session);
    }
}