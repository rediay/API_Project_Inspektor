using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.ThirdPartyProfiling
{
    public interface IThirdPartyProfilingRepository
    {
        Task<PagedResponseDTO<List<Entities.ThirdPartyProfiling>>> GetAll(ContextSession session,
            PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        Task<bool> Import(ContextSession session, List<Entities.ThirdPartyProfiling> ownLists);
    }
}