/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO.Queries;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Queries
{
    public interface IBulkQueryService
    {
        Task<BulkQueryResponseDTO> BulkQuery(BulkQueryRequestDTO request);
        Task<BulkQueryResponseDTO> getQuery(int QueryId);        


    }
}