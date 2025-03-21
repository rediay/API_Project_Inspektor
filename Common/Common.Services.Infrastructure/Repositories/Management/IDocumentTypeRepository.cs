/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Management
{
    public interface IDocumentTypeRepository<TDocumentType> where TDocumentType : DocumentType
    {
        Task Delete(int id, ContextSession session);
        Task<ICollection<TDocumentType>> Get(ContextSession session);
        Task<TDocumentType> GetById(int id, ContextSession session);            
        Task<TDocumentType> Edit(TDocumentType documentType, ContextSession session);
        
    }
}
