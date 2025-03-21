/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.DTO.IndividualQueryExternal;
using Common.DTO.Queries;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure
{
    public interface IIndividualQueryService
    {
        Task<IndividualQueryResponseDTO> makeQuery(IndividualQueryParamsDTO dto);
        Task<IndividualQueryExternalResponseEsDTO> makeQueryExternal(IndividualQueryExternalParamsDTO dto);
        Task<QueryDTO> previusQuery(IndividualQueryParamsDTO dto);
        Task<IndividualQueryResponseDTO> getQuery(int QueryId);
        
    }
}