using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Log;
using Common.DTO.Users;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Common.Utils;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class LogRepository : BaseRepository<Log, DataContext>, ILogRepository
    {
        public LogRepository(DataContext context) : base(context)
        {
        }

        public async Task<PagedResponseDTO<List<LogDTO<T>>>> GetAll<T>(ContextSession session,
            LogPaginationFilterDTO paginationFilter)
            where T : BaseEntity, new()
        {
            return await GetLogs<T>(session, paginationFilter);
        }

        public async Task<PagedResponseDTO<List<LogMappedDTO<R>>>> GetAll<T, R>(ContextSession session,
            LogPaginationFilterDTO paginationFilter)
            where T : BaseEntity, new()
            where R : BaseDTO
        {
            var response = await GetLogs<T>(session, paginationFilter);
            var responseMappped = response.Data.Select(record =>
            {
                var newRecord = LogMappedDTO<R>.FromLogDTO(record);
                newRecord.Record = record.Record.MapTo<R>();
                newRecord.CurrentRecord = record.CurrentRecord.MapTo<R>();
                newRecord.User = record.User.MapTo<UserManagementDto>();
                return newRecord;
            }).ToList();

            return response.CopyWith(responseMappped);
        }

        private async Task<PagedResponseDTO<List<LogDTO<T>>>> GetLogs<T>(ContextSession session,
            LogPaginationFilterDTO paginationFilter)
            where T : BaseEntity, new()
        {
            IQueryable<Log> logEntities = GetEntities(session);

            if (paginationFilter.StartDate is not null && paginationFilter.EndDate is not null &&
                paginationFilter.StartDate < paginationFilter.EndDate)
            {
                var startDate = paginationFilter.StartDate;
                var endDate = paginationFilter.EndDate;

                logEntities = GetQueryableEntities(session, logEntities);
                logEntities = logEntities
                    .Where(obj => obj.CreatedAt >= startDate && obj.CreatedAt <= endDate);
            }

            if (paginationFilter.User is not null)
            {
                var user = paginationFilter.User;

                logEntities = GetQueryableEntities(session, logEntities);
                logEntities = logEntities.Where(obj =>
                    (obj.User.Name + " " + obj.User.LastName).Contains(user)
                    || obj.User.Login.Contains(user)
                );
            }

            if (logEntities is not null)
            {
                var modelName = typeof(T).Name;

                logEntities = logEntities.Where(obj => obj.ModelName.Contains(modelName));
                logEntities = logEntities.Include(obj => obj.User);

                var totalSkipped = (paginationFilter.PageNumber - 1) * paginationFilter.PerPage;

                var logs = await logEntities
                    .Skip(totalSkipped)
                    .Take(paginationFilter.PerPage)
                    .ToListAsync();

                var currentRecords = await getLogCurrentRecords<T>(logs, session);
                var records = LogDTO<T>.GetLogsWithCurrentRelatedRecords(logs, currentRecords);

                var total = await logEntities.CountAsync();
                var pageNumber = paginationFilter.PageNumber;
                var perPage = paginationFilter.PerPage;

                var pagedResponseDto = new PagedResponseDTO<List<LogDTO<T>>>(records, pageNumber, perPage, total);
                return pagedResponseDto;
            }

            var empty = new List<LogDTO<T>>();

            return new PagedResponseDTO<List<LogDTO<T>>>(empty, paginationFilter.PageNumber, paginationFilter.PerPage,
                0);
        }

        private IQueryable<Log> GetQueryableEntities(ContextSession session, IQueryable<Log> queryable)
        {
            return queryable ?? GetEntities(session);
        }

        public virtual async Task<List<T>> getLogCurrentRecords<T>(List<Log> logs, ContextSession session)
            where T : BaseEntity, new()
        {
            var baseEntity = new T();
            var modelsId = logs.Select(log => log.ModelId).ToList();
            
            //GetContext(session).Set<T>().GetAllIncluding(baseEntity.RelationshipsNames);
            
            //var currentRecords = await GetContext(session).Set<T>().AsQueryable()
            var currentRecords = await GetContext(session).Set<T>().GetAllIncluding(baseEntity.RelationshipNames).AsQueryable()
                //.AsNoTracking()
                .Where(obj => modelsId.Contains(obj.Id)).ToListAsync();

            return currentRecords;
        }
    }
}