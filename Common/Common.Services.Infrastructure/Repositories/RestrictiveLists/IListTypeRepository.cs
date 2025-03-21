using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.RestrictiveLists
{
    public interface IListTypeRepository
    {
        Task<List<ListType>> GetAll(ContextSession session, bool includeDeleted = false);
        Task<PagedResponseDTO<List<ListType>>> GetAll(ContextSession session, PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        Task<ListType> Get(int id, ContextSession session, bool includeDeleted = false);

        Task<ListType> Edit(ListType listGroup, ContextSession session);
        
        Task Delete(int id, ContextSession session);
    }
}