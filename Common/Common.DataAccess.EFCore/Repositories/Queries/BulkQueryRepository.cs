using Amazon.Runtime.Internal.Transform;
using Common.DTO;
using Common.DTO.Queries;
using Common.Entities;
using Common.Entities.SPsData;
using Common.Services.Infrastructure.Queries;
using Common.Services.Infrastructure.Services.Files;
using Common.Utils;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTable = System.Data.DataTable;
using Query = Common.Entities.Query;

namespace Common.DataAccess.EFCore.Repositories.Queries
{
    public class BulkQueryRepository : IBulkQueryRepository
    {
        private readonly DataContext _context;
        private int batchSize = 50;
        private int batchSizeOwnList = 1000;
        private readonly IFileShare fileShare;
        public BulkQueryRepository(DataContext dataContext, IFileShare fileShare)
        {
            _context = dataContext;
            _context.Database.SetCommandTimeout(System.TimeSpan.FromHours(5));
            this.fileShare = fileShare;
            //_context.Database.SetCommandTimeout(System.TimeSpan.FromDays(1));
        }


        public async Task<QueryDetailAndQueryDTO> BulkQuery(BulkQueryRequestDTO bulkQueryRequestDTO, ContextSession session)
        {
            try
            {
                string notIn;
                IEnumerable<ListResponse> lists = Enumerable.Empty<ListResponse>();
                var userId = session.UserId;
                var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
                bulkQueryRequestDTO.CompanyId = user.CompanyId;



                Query query = _context.Queries.FromSqlInterpolated($"dbo.addNewQuery {user.CompanyId}, {user.Id},{"2"},''").AsEnumerable().FirstOrDefault();
                var tvpParameter = new Microsoft.Data.SqlClient.SqlParameter("@TVP_BulkQueryData", SqlDbType.Structured)
                {
                    Value = bulkQueryRequestDTO.dataTablefromFile,
                    TypeName = "starter_core.TVP_BulkQueryData"
                };

                var Data = _context.QueryDetails.FromSqlInterpolated($"[starter_core].[addBulkQueryDetail] {query.Id}, {tvpParameter}").AsEnumerable().ToList();

                var QueryDetailData = new QueryDetailAndQueryDTO()
                {
                    Query = query,
                    QueryDetail = Data
                };
                return QueryDetailData;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }


        #region Consultas por Prioridades
        public IEnumerable<ListResponse> Priority1(BulkQueryRequestDTO bulkQueryRequestDTO)
        {

            IEnumerable<ListResponse> lists = new List<ListResponse>();

            DataTable dataTable = bulkQueryRequestDTO.dataSetPriorities.Tables[0];


            int totalRows = dataTable.Rows.Count;

            for (int startIndex = 0; startIndex < totalRows; startIndex += batchSize)
            {
                //int endIndex = Math.Min(startIndex + batchSize, totalRows);

                IEnumerable<DataRow> batchRows = dataTable.Rows.Cast<DataRow>()
                                                .Skip(startIndex)
                                                .Take(batchSize);
                DataTable batchDataTable = batchRows.CopyToDataTable();
                var tvpParameter = new Microsoft.Data.SqlClient.SqlParameter("@TVP_BulkQueryData", SqlDbType.Structured)
                {
                    Value = batchDataTable,
                    TypeName = "starter_core.TVP_BulkQueryData"
                };
                lists = lists.Union(_context.ListResponse.FromSqlInterpolated($"[starter_core].[BulkQueryPriority1] {bulkQueryRequestDTO.CompanyId}, {tvpParameter}")).ToList();


            }

            return lists;
        }
        public IEnumerable<ListResponse> Priority2(BulkQueryRequestDTO bulkQueryRequestDTO, string notIn)
        {

            DataTable dataTable = bulkQueryRequestDTO.dataSetPriorities.Tables[1];
            var tvpParameter = new Microsoft.Data.SqlClient.SqlParameter("@TVP_BulkQueryData", SqlDbType.Structured)
            {
                Value = dataTable,
                TypeName = "starter_core.TVP_BulkQueryData"
            };
            IEnumerable<ListResponse> lists = _context.ListResponse.FromSqlInterpolated($"[starter_core].[BulkQueryPriority2] {tvpParameter} ,{notIn},{bulkQueryRequestDTO.CompanyId} ");

            return lists;
        }
        public IEnumerable<ListResponse> Priority3(BulkQueryRequestDTO bulkQueryRequestDTO, string notIn)
        {

            IEnumerable<ListResponse> lists = new List<ListResponse>();

            DataTable dataTable = bulkQueryRequestDTO.dataSetPriorities.Tables[2];


            int totalRows = dataTable.Rows.Count;

            for (int startIndex = 0; startIndex < totalRows; startIndex += batchSize)
            {
                //int endIndex = Math.Min(startIndex + batchSize, totalRows);

                IEnumerable<DataRow> batchRows = dataTable.Rows.Cast<DataRow>()
                                                .Skip(startIndex)
                                                .Take(batchSize);
                DataTable batchDataTable = batchRows.CopyToDataTable();
                var tvpParameter = new Microsoft.Data.SqlClient.SqlParameter("@TVP_BulkQueryData", SqlDbType.Structured)
                {
                    Value = batchDataTable,
                    TypeName = "starter_core.TVP_BulkQueryData"
                };
                lists = lists.Union(_context.ListResponse.FromSqlInterpolated($"[starter_core].[BulkQueryPriority3] {tvpParameter},{notIn},{bulkQueryRequestDTO.CompanyId}")).ToList();


            }

            return lists;
        }
        public IEnumerable<ListResponse> Priority4(BulkQueryRequestDTO bulkQueryRequestDTO, string notIn)
        {

            IEnumerable<ListResponse> lists = new List<ListResponse>();

            DataTable dataTable = bulkQueryRequestDTO.dataSetPriorities.Tables[2];


            int totalRows = dataTable.Rows.Count;

            for (int startIndex = 0; startIndex < totalRows; startIndex += batchSize)
            {
                //int endIndex = Math.Min(startIndex + batchSize, totalRows);

                IEnumerable<DataRow> batchRows = dataTable.Rows.Cast<DataRow>()
                                                .Skip(startIndex)
                                                .Take(batchSize);
                DataTable batchDataTable = batchRows.CopyToDataTable();
                var tvpParameter = new Microsoft.Data.SqlClient.SqlParameter("@TVP_BulkQueryData", SqlDbType.Structured)
                {
                    Value = batchDataTable,
                    TypeName = "starter_core.TVP_BulkQueryData"
                };
                var result = _context.ListResponse.FromSqlInterpolated($"[starter_core].[BulkQueryPriority4] {tvpParameter},{notIn},{bulkQueryRequestDTO.CompanyId}").AsNoTracking().ToList();
                //var result = _context.ListResponse.FromSqlInterpolated($"Exec [starter_core].[BulkQueryPriority4] @TVP_BulkQueryData = {tvpParameter} , @notIn = {notIn},@companyId = {bulkQueryRequestDTO.CompanyId}");
                lists = lists.Union(result).ToList();
            }
            return lists;
        }
        public void UpdateQuantityUserPerList(DataTable Quantities)
        {
            try
            {
                var tvpParameter = new Microsoft.Data.SqlClient.SqlParameter("@Updates", SqlDbType.Structured)
                {
                    Value = Quantities,
                    TypeName = "starter_core.TVP_UpdateQuantityQueries"
                };
                var result = _context.QueryDetails.FromSqlInterpolated($"[starter_core].[UpdateQuantityQueries] {tvpParameter}").AsEnumerable().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion


        public async Task<BulkQueryResponseDTO> getBulkQuery(int QueryId, ContextSession session)
        {
            var userId = session.UserId;
            var currentUSer = _context.Users.Find(userId);
            var listSearch = await _context.CompanyTypeList.Where(obj => obj.Company.Id == currentUSer.CompanyId)
            .Include(x => x.ListType)
            .Include(x => x.User)
            .Include(x => x.ListType)
            .ThenInclude(ListType => ListType.ListGroup)
            .Include(l => l.ListType)
            .ThenInclude(c => c.Country)
            .Include(l => l.ListType)
            .ThenInclude(c => c.Periodicity)
            .AsNoTracking().ToListAsync();

            var obj = await _context.QueryDetails.Where(x => x.QueryId == QueryId).AsNoTracking().ToListAsync();

            var image = (from sw in _context.Companies where sw.Id == currentUSer.CompanyId select sw.Image).FirstOrDefault();

            BulkQueryResponseDTO responseDto = await fileShare.FileDownloadAsync<BulkQueryResponseDTO>(QueryId);
            //BulkQueryResponseDTO responseDto = FilesHelper.getBulkQuery(QueryId);
            if (responseDto != null && responseDto.query.CompanyId != currentUSer.CompanyId)
            {
                responseDto = null;
                new ForbidResult("No tiene permisos");
                return null;
            }
            responseDto.query.User = currentUSer.MapTo<UserDTO>();
            responseDto.listsearch = listSearch.MapTo<List<CompanyTypeListDTO>>();
            responseDto.QueryDetails = obj.MapTo<List<QueryDetailDTO>>();
            responseDto.image = image;
            return await Task.FromResult<BulkQueryResponseDTO>(responseDto);
        }
        public IEnumerable<OwnListResponse> OwnLists(DataTable dataTable, string CompanyId)
        {

            IEnumerable<OwnListResponse> lists = new List<OwnListResponse>();
            int totalRows = dataTable.Rows.Count;

            for (int startIndex = 0; startIndex < totalRows; startIndex += batchSizeOwnList)
            {
                int endIndex = Math.Min(startIndex + batchSizeOwnList, totalRows);

                IEnumerable<DataRow> batchRows = dataTable.Rows.Cast<DataRow>()
                                                .Skip(startIndex)
                                                .Take(batchSizeOwnList);
                DataTable batchDataTable = batchRows.CopyToDataTable();
                var tvpParameter = new Microsoft.Data.SqlClient.SqlParameter("@TVP_BulkQueryData", SqlDbType.Structured)
                {
                    Value = batchDataTable,
                    TypeName = "starter_core.TVP_BulkQueryData"
                };
                lists = lists.Union(_context.OwnListResponse.FromSqlInterpolated($" starter_core.BulkQueryOwnList {tvpParameter}, {CompanyId}")).ToList();
                //lists = lists.Concat(_context.OwnListResponse.FromSqlInterpolated($" starter_core.BulkQueryOwnList {tvpParameter}, {CompanyId}")).ToList();

            }

            return lists;
        }
    }
}
