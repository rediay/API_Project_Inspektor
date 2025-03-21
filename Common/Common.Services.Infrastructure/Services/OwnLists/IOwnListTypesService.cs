/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Common.Services.Infrastructure.Management
{
    public interface IOwnListTypesService
    {
        Task<List<OwnListTypesDTO>> GetOwnListTypes(int CompanyId);
        Task<bool> UpdateOwnListType(OwnListTypesDTO ownListTypesDTO);
        Task<bool> CreateOwnListType(OwnListTypesDTO ownListTypesDTO);
        Task<bool> DeleteOwnListType(int id);
        Task<bool> ImportOwnLists(int ownListTypeId, IFormFile templateFile);
        Task<bool> DeleteImportedOwnListsByType(int ownListTypeId);

        // Task<NotificationSettingsDTO> Edit(NotificationSettingsDTO dto);

    }
}