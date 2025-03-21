using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Log;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Services;

namespace Common.Services
{
    public class LogService : BaseService, ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ICurrentContextProvider contextProvider, ILogRepository logRepository) : base(contextProvider)
        {
            _logRepository = logRepository;
        }

        public async Task<PagedResponseDTO<List<LogDTO<T>>>> GetAll<T>(LogPaginationFilterDTO paginationFilter)
            where T : BaseEntity, new()
        {
            var pagedResponse = await _logRepository.GetAll<T>(Session, paginationFilter);
            return pagedResponse;
        }

        public async Task<PagedResponseDTO<List<LogMappedDTO<R>>>> GetAll<T, R>(LogPaginationFilterDTO paginationFilter)
            where T : BaseEntity, new() where R : BaseDTO
        {
            var pagedResponse = await _logRepository.GetAll<T, R>(Session, paginationFilter);
            return pagedResponse;
        }
    }
}