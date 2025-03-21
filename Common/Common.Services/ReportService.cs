using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Services;
using Common.Utils;

namespace Common.Services
{
    public class ReportService : BaseService, IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(ICurrentContextProvider contextProvider, IReportRepository reportRepository) : base(
            contextProvider)
        {
            _reportRepository = reportRepository;
        }

        // public async Task<PagedResponseDTO<List<QueryJsonFileDTO>>> GetAll(ReportPaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        public async Task<PagedResponseDTO<List<QueryDetailDTO>>> GetAll(ReportPaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _reportRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            return pagedResponse;
            /*var queryList = new List<QueryJsonFileDTO>();

            foreach (var queryDetail in pagedResponse.Data)
            {
                var queryJsonFileDTO = FilesHelper.getQuery(queryDetail.QueryId);
                queryList.Add(queryJsonFileDTO);
            }

            var newRepose = new PagedResponseDTO<List<QueryJsonFileDTO>>(queryList);
            return newRepose;*/

            /*var listGroups = pagedResponse.Data.Select(list => list.MapTo<ListDTO>()).ToList();
            return pagedResponse.CopyWith(listGroups);*/
        }

        public ResponseDTO<QueryJsonFileDTO> GetQueryLists(int queryId)
        {
            var response = _reportRepository.GetQueryLists(Session, queryId);
            return response;
        }
    }
}