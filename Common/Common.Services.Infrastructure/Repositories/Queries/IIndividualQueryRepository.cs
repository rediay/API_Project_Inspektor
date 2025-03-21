/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.DTO.IndividualQueryExternal;
using Common.DTO.Queries;
using Common.Entities;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Queries
{
    public interface IIndividualQueryRepository
    {
    
        Task<IndividualQueryResponseDTO> makeQuery(IndividualQueryParamsDTO individualQueryParamsDTO, ContextSession session);
        Task<IndividualQueryExternalResponseEsDTO> makeQueryExternal(IndividualQueryExternalParamsDTO individualQueryParamsDTO, ContextSession session);
        Task<QueryDTO> previusQuery(IndividualQueryParamsDTO individualQueryParamsDTO, ContextSession session);
        Task<IndividualQueryResponseDTO> getQuery(int IdQuery, ContextSession session);
    }
}