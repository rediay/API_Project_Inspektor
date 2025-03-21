using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Queries;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.RestrictiveLists;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.RestrictiveLists
{
    public class ListRepository : BaseLoggableRepository<List, DataContext>, IListRepository
    {
        public ListRepository(DataContext context) : base(context)
        {
        }

        public async Task<PagedResponseDTO<List<List>>> GetAll(ContextSession session,
            ListPaginationFilterDTO paginationFilter, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilter.PageNumber - 1) * paginationFilter.PerPage;

            var queryEntities = GetEntities(session);

            if (paginationFilter.Alias is not null)
                queryEntities = queryEntities.Where(obj => obj.Alias.Contains(paginationFilter.Alias));

            if (paginationFilter.Entity is not null)
                queryEntities = queryEntities.Where(obj => obj.Entity.Contains(paginationFilter.Entity));

            if (paginationFilter.ListTypeId is not null)
                queryEntities = queryEntities.Where(obj => obj.ListTypeId == paginationFilter.ListTypeId);

            if (paginationFilter.Activated is not null)
                queryEntities = queryEntities.Where(obj => obj.Activated == paginationFilter.Activated);

            if (paginationFilter.Source is not null)
                queryEntities = queryEntities.Where(obj => obj.Source.Contains(paginationFilter.Source));

            if (paginationFilter.Zone is not null)
                queryEntities = queryEntities.Where(obj => obj.Zone.Contains(paginationFilter.Source));

            if (paginationFilter.Validated is not null)
                queryEntities = queryEntities.Where(obj => obj.Validated == paginationFilter.Validated);

            if (paginationFilter.UserId is not null)
                queryEntities = queryEntities.Where(obj => obj.UserId == paginationFilter.UserId);

            if (paginationFilter.CountryId is not null)
               queryEntities = queryEntities.Where(obj => obj.CountryId == paginationFilter.CountryId);

            if (paginationFilter.thirdPartyId is not null)
                queryEntities = queryEntities.Where(obj => obj.ThirdListId == int.Parse(paginationFilter.thirdPartyId));


            queryEntities = queryEntities.Include(p => p.ListType);
            queryEntities = queryEntities.Include(y => y.ThirdList);

            var newsList = await queryEntities
                .Skip(totalSkipped)
                .Take(paginationFilter.PerPage)
                .ToListAsync();

            var total = await queryEntities.CountAsync();
            var pageNumber = paginationFilter.PageNumber;
            var perPage = paginationFilter.PerPage;

            var pagedResponseDto = new PagedResponseDTO<List<List>>(newsList, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<PagedResponseDTO<List<List>>> GetAllByValidation(ContextSession session,
            ListPaginationFilterDTO paginationFilter, bool includeDeleted = false)
        {
            var query = paginationFilter.query;
            var totalSkipped = (paginationFilter.PageNumber - 1) * paginationFilter.PerPage;

            var allTempRecords = await GetEntities(session)
                .Where(obj => obj.TempData != null).Include(t => t.ThirdList)
                .ToListAsync();

            //if (paginationFilter.query is not null)
            //    allTempRecords = allTempRecords.Where(obj => obj.TempData.Name.Contains(query)
            //                                                 || obj.TempData.Document.Contains(query)
            //                                                 || obj.TempData.Alias.Contains(query)
            //                                                 || obj.TempData.Crime.Contains(query)
            //    ).ToList();

            if (paginationFilter.query is not null)
            {
                allTempRecords = allTempRecords.Where(obj =>
                    obj.TempData.Alias.Contains(query) ||
                    obj.TempData.Crime.Contains(query)
                ).ToList();

                allTempRecords = allTempRecords.Select(obj => obj.TempData).ToList();
            }


            var newsList = allTempRecords
                .Skip(totalSkipped)
                .Take(paginationFilter.PerPage)
                .ToList();

            var total = allTempRecords.Count();
            var pageNumber = paginationFilter.PageNumber;
            var perPage = paginationFilter.PerPage;

            var pagedResponseDto = new PagedResponseDTO<List<List>>(newsList, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<List> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session)
                .Where(obj => obj.Id == id).Include(y => y.ThirdList)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> BulkCreate(List<List> lists, ContextSession session)
        {
            var context = GetContext(session);
            await context.AddRangeAsync(lists);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidateRecords(IEnumerable<int> listId, ContextSession session)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id", typeof(int));

                foreach (var item in listId)
                {
                    table.Rows.Add(item);
                }

                var context = GetContext(session);

                var tvpParameter = new Microsoft.Data.SqlClient.SqlParameter("@TVP_BulkValidationRecords", SqlDbType.Structured)
                {
                    Value = table,
                    TypeName = "starter_core.TVP_BulkValidationRecords"
                };
                
                await context.Database.ExecuteSqlRawAsync("EXEC [dbo].[BulkValidationRecords] @TVP_BulkValidationRecords", tvpParameter);
                return true;
            }
            catch (System.Exception ex)
            {

                return false;
            }
        }
    }
}