using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Entities.Relations_Countrys;

namespace Common.Services.Infrastructure.Repositories.Relations_Countrys
{
    public interface ICountryRepository
    {
        Task<PagedResponseDTO<List<Countries>>> GetAll(ContextSession session, PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        Task<IEnumerable<Countries>> GetCountries(ContextSession session);
        Task<Countries> Edit(Countries country, ContextSession session);
        Task<bool> BulkCreate(List<Countries> lists, ContextSession session);
        Task<bool> DeleteItem(int id, ContextSession session);
    }
}