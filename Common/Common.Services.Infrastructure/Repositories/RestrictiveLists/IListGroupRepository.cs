using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.RestrictiveLists
{
    public interface IListGroupRepository
    {
        Task<PagedResponseDTO<List<ListGroup>>> GetAll(ContextSession session, PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        Task<ListGroup> Get(int id, ContextSession session, bool includeDeleted = false);

        Task<ListGroup> Edit(ListGroup listGroup, ContextSession session);
        
        Task Delete(int id, ContextSession session);
    }
}