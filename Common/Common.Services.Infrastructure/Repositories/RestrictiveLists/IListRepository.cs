using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.RestrictiveLists;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.RestrictiveLists
{
    public interface IListRepository
    {
        Task<PagedResponseDTO<List<List>>> GetAll(ContextSession session, ListPaginationFilterDTO paginationFilter,
            bool includeDeleted = false);
        Task<PagedResponseDTO<List<List>>> GetAllByValidation(ContextSession session, ListPaginationFilterDTO paginationFilter,
            bool includeDeleted = false);
        Task<List> Get(int id, ContextSession session, bool includeDeleted = false);

        Task<List> Edit(List listGroup, ContextSession session);        
        Task<bool> BulkCreate(List<List> lists, ContextSession session);

        Task<bool> ValidateRecords(IEnumerable<int> listId, ContextSession session);
    }
}