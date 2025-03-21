using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Log;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories
{
    public interface ILogRepository
    {
        Task<PagedResponseDTO<List<LogDTO<T>>>> GetAll<T>(ContextSession session,
            LogPaginationFilterDTO paginationFilter) where T : BaseEntity, new();

        Task<PagedResponseDTO<List<LogMappedDTO<R>>>> GetAll<T, R>(ContextSession session,
            LogPaginationFilterDTO paginationFilter)
            where T : BaseEntity, new()
            where R : BaseDTO;
    }
}