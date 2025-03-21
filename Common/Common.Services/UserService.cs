/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.Management;
using Common.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Services
{
    public class UserService<TUser> : BaseService, IUserService where TUser : User, new()
    {
        protected readonly IUserRepository<TUser> userRepository;
        protected readonly IUserPhotoRepository userPhotoRepository;
        protected readonly IPermissionsRepository _permissionsRepository;

        public UserService(ICurrentContextProvider contextProvider, IUserRepository<TUser> userRepository,
            IUserPhotoRepository userPhotoRepository, IPermissionsRepository permissionsRepository) : base(contextProvider)
        {
            this.userRepository = userRepository;
            this.userPhotoRepository = userPhotoRepository;
            this._permissionsRepository = permissionsRepository;
        }

        public async Task<bool> Delete(int id)
        {
            await userRepository.Delete(id, Session);
            return true;
        }

        public async Task<UserDTO> Edit(UserDTO dto)
        {
            var user = dto.MapTo<TUser>();
            await userRepository.Edit(user, Session);
            return user.MapTo<UserDTO>();
        }

        public async Task<byte[]> GetUserPhoto(int userId)
        {
            var photoContent = await userPhotoRepository.Get(userId, Session);
            return photoContent?.Image;
        }

        public async Task<UserDTO> GetById(int id, bool includeDeleted = false)
        {
            var user = await userRepository.Get(id, Session, includeDeleted);
            var userDTO = user.MapTo<UserDTO>();
            userDTO.UserPermissions = new List<ModulesDTO>();
            var modules = _permissionsRepository.GetModules(Session).Result.ToList();
            var noAut = user.Permissions.Where(x => x.Status == false).
               ToList();
            foreach (var item in noAut)
            {
                foreach (var item2 in modules)
                {
                    if(item2.SubModules.Contains(item.SubModule))
                    item2.SubModules.Remove(item.SubModule);
                }               
            }
            modules = modules.Where(x => x.SubModules.Count > 0).ToList();

            userDTO.UserPermissions = modules.MapTo<List<ModulesDTO>>();
            //user.Permissions.MapTo<ICollection<PermissionsDTO>>().Where(x => x.Status == true).
            //ToList().ForEach(x => userDTO.UserPermissions.Add(x.ModulesDTO));

            return userDTO;
        }

        public async Task<UserDTO> GetByLogin(string login, bool includeDeleted = false)
        {
            var user = await userRepository.GetByLogin(login, Session, includeDeleted);
            return user.MapTo<UserDTO>();
        }
        public async Task<UserDTO> Get(string login, bool includeDeleted = false)
        {
            var user = await userRepository.GetByLogin(login, Session, includeDeleted);
            return user.MapTo<UserDTO>();
        }

        public async Task<bool> ValidateCompanyIsActive(int userId)
        {
            var result = await userRepository.ValidateCompanyIsActive(userId, Session);
            
            return result;
        }
    }
}