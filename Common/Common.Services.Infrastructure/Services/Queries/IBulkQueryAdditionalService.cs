using Common.DTO.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Services.Queries
{
    public interface IBulkQueryAdditionalService
    {
        Task BulkQueryAdditionalAsync(BulkQueryServicesAdditionalRequestDTO bulkQueryRequestDTO);
        Task<List<BulkQueryServicesAdditionalDTO>> getBulkQueryServiceAdditionalTable();
        Task<BulkQueryServicesAdditionalResponseDTO> getBulkQueryServiceAdditional(int QueryId);

    }
}
