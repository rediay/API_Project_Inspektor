using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Services
{
    public interface IListPeriodicityService
    {
        Task<PagedResponseDTO<List<Periodicity>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);        
    }
}