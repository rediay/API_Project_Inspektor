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
    public interface IAccessRepository<TAccess> where TAccess : Access
    {
        Task Delete(int id, ContextSession session);
        Task<ICollection<TAccess>> Get(ContextSession session);
        Task<TAccess> GetById(int id, ContextSession session);            
        Task<TAccess> Edit(TAccess Job, ContextSession session);
        Task<ICollection<TAccess>> GetAccesByCompany( ContextSession session);
        Task<ICollection<TAccess>> GetAccesByIdCompany(ContextSession session, int IdCompany);
    }
}
