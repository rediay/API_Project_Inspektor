/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Management
{
    public interface IAccessService
    {
        Task<AccessDTO> GetById(int id);

        Task<AccessDTO> Edit(AccessDTO AccessDTO);
        Task<bool> Delete(int id);
        Task<ICollection<AccessDTO>> Get();
        Task<ICollection<AccessDTO>> GetAccesByCompany();

        Task<ICollection<AccessDTO>> GetAccesByIdCompany(int IdCompany);



    }
}