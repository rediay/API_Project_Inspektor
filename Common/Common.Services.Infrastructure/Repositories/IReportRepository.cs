using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.RestrictiveLists;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IReportRepository
    {
        Task<PagedResponseDTO<List<QueryDetailDTO>>> GetAll(ContextSession session, ReportPaginationFilterDTO paginationFilter,
            bool includeDeleted = false);
        ResponseDTO<QueryJsonFileDTO> GetQueryLists(ContextSession session, int queryId);
    }
}