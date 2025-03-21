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
    public interface IPlanRepository
    {
        Task<Plan> Get(int id,ContextSession session);
        Task<List<Plan>> GetPlans(ContextSession session);
        Task<Plan> UpdatePlan(Plan plan, ContextSession session);
        Task Delete (int id, ContextSession session);
        Task<Plan> Edit(Plan plan, ContextSession session);        
    }
}