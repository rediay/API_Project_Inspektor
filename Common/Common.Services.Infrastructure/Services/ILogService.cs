using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Log;
using Common.Entities;

namespace Common.Services.Infrastructure.Services
{
    public interface ILogService
    {
        Task<PagedResponseDTO<List<LogDTO<T>>>> GetAll<T>(LogPaginationFilterDTO paginationFilter) where T : BaseEntity, new();
        Task<PagedResponseDTO<List<LogMappedDTO<R>>>> GetAll<T, R>(LogPaginationFilterDTO paginationFilter)
            where T : BaseEntity, new()
            where R : BaseDTO;
    }
}