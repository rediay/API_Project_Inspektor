/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on  of purchased license.
*/

using Common.DTO.OwnLists;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.OwnLists
{
    public interface IOwnListsService
    {
        Task<List<OwnListDTO>> GetOwnLists(int CompanyId);
        Task<bool> UpdateOwnList(OwnListDTO ownListDTO);
        Task<bool> CreateOwnList(OwnListDTO ownListDTO);
        Task<bool> DeleteOwnList(int id);

        // Task<NotificationSettingsDTO> Edit(NotificationSettingsDTO dto);

    }
}