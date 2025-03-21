/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Management;
using Common.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services
{
    public class AccessService<TAccess> : BaseService, IAccessService where TAccess : Access, new()
    {
        protected readonly IAccessRepository<TAccess> _AccessRepository;

        public AccessService(ICurrentContextProvider contextProvider, IAccessRepository<TAccess> AccessRepository) : base(contextProvider)
        {
            this._AccessRepository = AccessRepository;
        }

        public async Task<bool> Delete(int id)
        {
            await _AccessRepository.Delete(id, Session);
            return true;
        }

        public async Task<AccessDTO> Edit(AccessDTO dto)
        {
            var Access = dto.MapTo<TAccess>();
            var result = await _AccessRepository.Edit(Access, Session);

            return result.MapTo<AccessDTO>();
        }

        public async Task<ICollection<AccessDTO>> Get()
        {
            var ls = await _AccessRepository.Get(Session);
            return ls.MapTo<ICollection<AccessDTO>>();
        }

        public async Task<AccessDTO> GetById(int id)
        {
            var user = await _AccessRepository.GetById(id, Session);
            return user.MapTo<AccessDTO>();
        }

        public async Task<ICollection<AccessDTO>> GetAccesByCompany()
        {
            var ls = await _AccessRepository.GetAccesByCompany(Session);
            return ls.MapTo<ICollection<AccessDTO>>();
        }

        public async Task<ICollection<AccessDTO>> GetAccesByIdCompany(int IdCompany)
        {
            var ls = await _AccessRepository.GetAccesByIdCompany(Session, IdCompany);
            return ls.MapTo<ICollection<AccessDTO>>();
        }


    }
}
