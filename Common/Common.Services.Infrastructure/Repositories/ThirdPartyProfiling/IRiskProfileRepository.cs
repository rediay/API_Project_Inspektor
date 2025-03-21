using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.ThirdPartyProfiling
{
    public interface IRiskProfileRepository
    {
        Task<PagedResponseDTO<List<RiskProfile>>> GetAll(ContextSession session,
            PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        Task<RiskProfile> Get(int id, ContextSession session, bool includeDeleted = false);
        Task<List<RiskProfile>> GetAll(ContextSession session, int companyId, 
            bool includeDeleted = false);
        Task<RiskProfile> Edit(RiskProfile user, ContextSession session);
    }
}