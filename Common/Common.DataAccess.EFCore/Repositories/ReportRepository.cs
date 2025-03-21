using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Common.Utils;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class ReportRepository : BaseRepository<QueryDetail, DataContext>, IReportRepository
    {
        public ReportRepository(DataContext context) : base(context)
        {
        }

        public async Task<PagedResponseDTO<List<QueryDetailDTO>>> GetAll(ContextSession session,
            ReportPaginationFilterDTO paginationFilter, bool includeDeleted = false)
        {
            // IQueryable<QueryDetail> queryEntities = null;
            IQueryable<QueryDetail> queryEntities = GetEntities(session);

            if (paginationFilter.CompanyId is not null)
            {
                queryEntities = GetQueryEntities(session, queryEntities);
                queryEntities = queryEntities.Where(obj => obj.Query.CompanyId == paginationFilter.CompanyId);
            }

            if (paginationFilter.IdQueryCompany is not null)
            {
                queryEntities = GetQueryEntities(session, queryEntities);
                queryEntities = queryEntities.Where(obj => obj.Query.IdQueryCompany == paginationFilter.IdQueryCompany);
            }

            if (paginationFilter.QueryTypeId is not null)
            {
                queryEntities = GetQueryEntities(session, queryEntities);
                queryEntities = queryEntities.Where(obj => obj.Query.QueryTypeId == paginationFilter.QueryTypeId);
            }

            if (paginationFilter.StartDate is not null && paginationFilter.EndDate is not null &&
                paginationFilter.StartDate < paginationFilter.EndDate)
            {
                var startDate = paginationFilter.StartDate;
                var endDate = paginationFilter.EndDate;

                queryEntities = GetQueryEntities(session, queryEntities);
                queryEntities = queryEntities.Where(obj => obj.CreatedAt >= startDate && obj.CreatedAt < endDate);
            }

            if (paginationFilter.Name is not null)
            {
                var name = paginationFilter.Name;

                queryEntities = GetQueryEntities(session, queryEntities);
                queryEntities = queryEntities.Where(obj => obj.Name.Contains(name));

                /*queryEntities = queryEntities.Where(query =>
                    query.QueryDetails.Any(queryDetail => queryDetail.Name.Contains(name))
                );*/

                /*queryEntities = queryEntities.Where(query =>
                    query.QueryDetails.Where(queryDetail => queryDetail.Name.Contains(name)).ToList().Count > 0
                );*/
            }

            if (paginationFilter.Identification is not null)
            {
                var identification = paginationFilter.Identification;

                queryEntities = GetQueryEntities(session, queryEntities);
                queryEntities = queryEntities.Where(obj => obj.Identification == identification);

                /*queryEntities = queryEntities.Where(query =>
                    query.QueryDetails.Any(queryDetail => queryDetail.Identification == identification)
                );*/
            }

            if (paginationFilter.User is not null)
            {
                var user = paginationFilter.User;

                queryEntities = GetQueryEntities(session, queryEntities);
                queryEntities = queryEntities.Where(obj =>
                    (obj.Query.User.Name + " " + obj.Query.User.LastName).Contains(user)
                    || obj.Query.User.Login.Contains(user)
                );
            }

            if (queryEntities is not null)
            {
                // queryEntities = queryEntities.Include(p => p.Query);
                queryEntities = queryEntities.Include(p => p.Query.User);
                queryEntities = queryEntities.Include(p => p.Query.QueryType);
                queryEntities = queryEntities.Include(p => p.Query.Company);

                var totalSkipped = (paginationFilter.PageNumber - 1) * paginationFilter.PerPage;

                var records = await queryEntities
                    .Skip(totalSkipped)
                    .Take(paginationFilter.PerPage)
                    .ToListAsync();

                var total = await queryEntities.CountAsync();
                var pageNumber = paginationFilter.PageNumber;
                var perPage = paginationFilter.PerPage;

                var mappedRecords = records.Select(item =>
                {
                    var newItem = item.MapTo<QueryDetailDTO>();
                    var queryId = newItem.QueryId;
                    var queryJsonResponse = GetQueryLists(session, queryId);
                    
                    newItem.Query.Lists = queryJsonResponse.Data.Lists;
                    
                    return newItem;
                }).ToList();
                
                var pagedResponseDto = new PagedResponseDTO<List<QueryDetailDTO>>(mappedRecords, pageNumber, perPage, total);

                return pagedResponseDto;
            }

            var empty = new List<QueryDetailDTO>();
            
            return new PagedResponseDTO<List<QueryDetailDTO>>(empty, paginationFilter.PageNumber, paginationFilter.PerPage, 0);
        }

        public ResponseDTO<QueryJsonFileDTO> GetQueryLists(ContextSession session, int queryId)
        {
            var queryJsonFileDto = FilesHelper.getQuery(queryId);
            var response = new ResponseDTO<QueryJsonFileDTO>(queryJsonFileDto);
            //  response.Succeeded = queryJsonFileDto;
            return response;
        }

        private IQueryable<QueryDetail> GetQueryEntities(ContextSession session, IQueryable<QueryDetail> queryable)
        {
            return queryable ?? GetEntities(session);
        }
    }
}
