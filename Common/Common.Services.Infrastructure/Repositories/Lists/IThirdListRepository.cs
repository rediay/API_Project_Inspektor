/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.DTO.Lists;
using Common.DTO.RestrictiveLists;
using Common.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Repositories.Lists
{
    public interface IThirdListRepository
    {
        Task<PagedResponseDTO<List<ThirdList>>> GetAllQuery(ContextSession session, ListPaginationFilterThirdDTO paginationFilter,
            bool includeDeleted = false);
        Task<PagedResponseDTO<List<ThirdList>>> GetAll(ContextSession session, PaginationFilterDTO paginationFilterDto,
         bool includeDeleted = false);

        Task<PagedResponseDTO<List<ThirdList>>> GetAllToVerify(ContextSession session, PaginationFilterDTO paginationFilterDto,
        bool includeDeleted = false);

        Task<ThirdList> GetListById(string id, ContextSession session);
        Task<ThirdList> Edit(ThirdList record, ContextSession session);
        Task<bool> Delete(int id, ContextSession session);
        Task<bool> BulkCreate(List<ThirdList> dtos, ContextSession session);
    }


}