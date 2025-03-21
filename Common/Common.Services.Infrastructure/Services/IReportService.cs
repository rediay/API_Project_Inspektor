using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.RestrictiveLists;
using Common.Entities;

namespace Common.Services.Infrastructure.Services
{
    public interface IReportService
    {
        Task<PagedResponseDTO<List<QueryDetailDTO>>> GetAll(ReportPaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        ResponseDTO<QueryJsonFileDTO> GetQueryLists(int queryId);
    }
}