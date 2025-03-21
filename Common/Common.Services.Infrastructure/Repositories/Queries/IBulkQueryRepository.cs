/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.DTO.Queries;
using Common.Entities;
using Common.Entities.SPsData;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Queries
{
    public interface IBulkQueryRepository
    {
    
        Task<QueryDetailAndQueryDTO> BulkQuery(BulkQueryRequestDTO bulkQueryRequestDTO, ContextSession session);
        //Task<QueryDetailAndQueryDTO> BulkQueryAndDetail(BulkQueryRequestDTO bulkQueryRequestDTO, ContextSession session);
        Task<BulkQueryResponseDTO> getBulkQuery(int IdQuery, ContextSession session);
        IEnumerable<ListResponse> Priority1(BulkQueryRequestDTO bulkQueryRequestDTO);
        IEnumerable<ListResponse> Priority2(BulkQueryRequestDTO bulkQueryRequestDTO,string notIn);
        IEnumerable<ListResponse> Priority3(BulkQueryRequestDTO bulkQueryRequestDTO,string notIn);
        IEnumerable<ListResponse> Priority4(BulkQueryRequestDTO bulkQueryRequestDTO,string notIn);
        //QueryResultRepositoryDTO Priority_3And4(DataTable dt,string names, string notIn,string CompanyId,List<QueryDetail> Data,Dictionary<int, int> Count_Priority3And4);
        IEnumerable<OwnListResponse> OwnLists(DataTable dt,string CompanyId);
        void UpdateQuantityUserPerList(DataTable Quantities);
    }
}