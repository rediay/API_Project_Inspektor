using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IListPeriodicityRepository
    {
        Task<PagedResponseDTO<List<Periodicity>>> GetAll(ContextSession session, PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
    }
}