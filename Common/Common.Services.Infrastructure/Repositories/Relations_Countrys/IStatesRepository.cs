using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Entities.Relations_Countrys;

namespace Common.Services.Infrastructure.Repositories.Relations_Countrys
{
    public interface IStatesRepository
    {
        Task<PagedResponseDTO<List<States>>> GetAll(ContextSession session, PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        Task<IEnumerable<States>> GetStatesById(int idcountry, ContextSession session);
        Task<States> Edit(States states, ContextSession session);
        Task<bool> BulkCreate(List<States> statelists, ContextSession session);
        Task<bool> Delete(int id, ContextSession session);
    }
}