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
    public interface IDocumentTypeService
    {
        Task<DocumentTypeDTO> GetById(int id);

        Task<DocumentTypeDTO> Edit(DocumentTypeDTO DocumentTypeDTO);
        Task<bool> Delete(int id);
        Task<ICollection<DocumentTypeDTO>> Get();
        
        


    }
}