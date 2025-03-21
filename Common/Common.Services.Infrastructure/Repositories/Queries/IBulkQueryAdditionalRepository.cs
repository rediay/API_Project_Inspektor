using Common.DTO.Queries;
using Common.Entities;
using Common.Entities.BulkQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Repositories.Queries
{
    public interface IBulkQueryAdditionalRepository
    {
        Task<BulkQueryServicesAdditionalResponseDTO> getBulkQueryServiceAdditional(int IdQuery, ContextSession session);
        Task<BulkQueryServicesAdditional> AddFileTable(BulkQueryServicesAdditional bulkQueryServicesAdditional, int userId);
        Task<List<BulkQueryServicesAdditionalDTO>> getBulkQueryServiceAdditionalTable(ContextSession session);
    }
}
