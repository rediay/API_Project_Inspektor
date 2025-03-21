using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.ThirdPartyProfiling
{
    public interface IRiskProfileVariableRepository
    {
        Task<PagedResponseDTO<List<RiskProfileVariable>>> GetAll(ContextSession session,
            PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        Task<RiskProfileVariable> Get(int id, ContextSession session, bool includeDeleted = false);
        
        Task<RiskProfileVariable> Edit(RiskProfileVariable user, ContextSession session);
        
        Task<float> GetTotalWeight(ContextSession session, int? id = null);
        
        Task<List<RiskProfileVariable>> GetAll(ContextSession session, int companyId, 
            bool includeDeleted = false);
    }
}