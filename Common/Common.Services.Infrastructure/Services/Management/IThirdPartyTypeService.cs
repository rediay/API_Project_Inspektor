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
    public interface IThirdPartyTypeService
    {
        Task<List<ThirdPartyTypeDTO>> GetByCompanyID();
        Task<ThirdPartyTypeDTO> UpdateThirdPartyType(ThirdPartyTypeDTO thirdPartyTypeDTO);        
        Task<bool> DeleteThirdPartyType(int id);
      

    }
}