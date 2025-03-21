/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.DTO.Management;
using Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Management
{
    public interface IPermissionsService
    {
        
        Task<RoleUserDTO> Update(RoleUserDTO roleUserDTO);
        Task<RoleUserDTO> GetPermissionsByUserId(int UserId);

        RoleUserDTO PermissionsToRoleUserDTO(List<Permissions> permissions);


    }
}