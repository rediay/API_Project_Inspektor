using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Entities.Relations_Countrys;

namespace Common.Services.Infrastructure.Repositories.Relations_Countrys
{
    public interface ICitiesRepository
    {
        Task<PagedResponseDTO<List<Cities>>> GetAll(ContextSession session, PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        Task<IEnumerable<Cities>> GetCitiesbyId(int idcountry,int stateid, ContextSession session);
        Task<Cities> Edit(Cities cities, ContextSession session);
        Task<bool> BulkCreate(List<Cities> statelists, ContextSession session);
        Task<bool> Delete(int id, ContextSession session);
    }
}