using AutoMapper;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Management;
using Common.Services.Infrastructure.Repositories.Management;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Common.DTO.Management;

namespace Common.Services.Management
{
    public class PermissionsService : BaseService, IPermissionsService
    {
        private readonly IMapper _mapper;
        protected readonly IPermissionsRepository _permissionsRepository;

        public PermissionsService(ICurrentContextProvider contextProvider, IPermissionsRepository permissionsService, IMapper mapper) : base(contextProvider)
        {
            this._permissionsRepository = permissionsService;
            _mapper = mapper;
        }
        public async Task<RoleUserDTO> Update(RoleUserDTO roleUserDTO)
        {
            List<Permissions> PermissionbyUser = new List<Permissions>();
            var permissions = SetPermissions(roleUserDTO);
            var perByUser = _permissionsRepository.GetPermissionsByUser(roleUserDTO.user,Session).Result.ToList();
            for (int i = 0;i< permissions.Count; i++)
            {
                var SubModuleId = permissions.ToList()[i].SubModuleId;
                var perUser = perByUser.FirstOrDefault(p => p.SubModuleId == SubModuleId);

                if (perUser != null)
                {
                    perUser.Status = permissions.ToList()[i].Status;
                    PermissionbyUser.Add(perUser);
                }
            }
            await _permissionsRepository.Update(PermissionbyUser, Session);
            return roleUserDTO;
        }


        public async Task<RoleUserDTO> GetPermissionsByUserId(int UserId)
        {            
            var permissions = await _permissionsRepository.GetPermissionsByUser(UserId, Session);

            return PermissionsToRoleUserDTO(permissions.ToList());
        }

        #region Map
        public List<Permissions> SetPermissions(RoleUserDTO roleUserDTO)
        {
            var permissions = new List<Permissions>();
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.logoempresa[0].id, Status = roleUserDTO.logoempresa[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.changepassword[0].id, Status = roleUserDTO.changepassword[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.thirdparties[0].id, Status = roleUserDTO.thirdparties[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.typeslistbyquery[0].id, Status = roleUserDTO.typeslistbyquery[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.procurator[0].id, Status = roleUserDTO.procurator[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.users[0].id, Status = roleUserDTO.users[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.setting[0].id, Status = roleUserDTO.setting[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.sentto[0].id, Status = roleUserDTO.sentto[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.monitoring[0].id, Status = roleUserDTO.monitoring[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.individual[0].id, Status = roleUserDTO.individual[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.massive[0].id, Status = roleUserDTO.massive[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.type[0].id, Status = roleUserDTO.type[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.manager[0].id, Status = roleUserDTO.manager[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.historyreport[0].id, Status = roleUserDTO.historyreport[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.getlog[0].id, Status = roleUserDTO.getlog[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.queriesandmatches[0].id, Status = roleUserDTO.queriesandmatches[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.coincidencedetailing[0].id, Status = roleUserDTO.coincidencedetailing[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.viewpastconsultations[0].id, Status = roleUserDTO.viewpastconsultations[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.certificationlistupdates[0].id, Status = roleUserDTO.certificationlistupdates[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.parameterscategory[0].id, Status = roleUserDTO.parameterscategory[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.news[0].id, Status = roleUserDTO.news[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.roi[0].id, Status = roleUserDTO.roi[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.faqs[0].id, Status = roleUserDTO.faqs[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.signal[0].id, Status = roleUserDTO.signal[0].status });
            permissions.Add(new Permissions() { UserId = roleUserDTO.user, SubModuleId = roleUserDTO.access[0].id, Status = roleUserDTO.access[0].status });

            return permissions;
        }

        public  RoleUserDTO PermissionsToRoleUserDTO (List<Permissions> permissions)
        {
            RoleUserDTO roleUserDTO = new RoleUserDTO();
            try
            {
                if (permissions.Count > 0)
                {
                    roleUserDTO.user = permissions.FirstOrDefault().UserId;
                    roleUserDTO.logoempresa = new Logoempresa[1] { new Logoempresa() { id = permissions[0].SubModuleId, status = permissions[0].Status } };
                    roleUserDTO.changepassword = new Changepassword[1] { new Changepassword() { id = permissions[1].SubModuleId, status = permissions[1].Status } };
                    roleUserDTO.thirdparties = new Thirdparty[1] { new Thirdparty() { id = permissions[2].SubModuleId, status = permissions[2].Status } };
                    roleUserDTO.typeslistbyquery = new Typeslistbyquery[1] { new Typeslistbyquery() { id = permissions[3].SubModuleId, status = permissions[3].Status } };
                    roleUserDTO.procurator = new Procurator[1] { new Procurator() { id = permissions[4].SubModuleId, status = permissions[4].Status } };
                    roleUserDTO.users = new DTO.Management.User[1] { new DTO.Management.User() { id = permissions[5].SubModuleId, status = permissions[5].Status } };
                    roleUserDTO.setting = new Setting[1] { new Setting() { id = permissions[6].SubModuleId, status = permissions[6].Status } };
                    roleUserDTO.sentto = new Sentto[1] { new Sentto() { id = permissions[7].SubModuleId, status = permissions[7].Status } };
                    roleUserDTO.monitoring = new Monitoring[1] { new Monitoring() { id = permissions[8].SubModuleId, status = permissions[8].Status } };
                    roleUserDTO.individual = new Individual[1] { new Individual() { id = permissions[9].SubModuleId, status = permissions[9].Status } };
                    roleUserDTO.manager = new Manager[1] { new Manager() { id = permissions[10].SubModuleId, status = permissions[10].Status } };
                    roleUserDTO.massive = new Massive[1] { new Massive() { id = permissions[11].SubModuleId, status = permissions[11].Status } };
                    roleUserDTO.type = new DTO.Management.Type[1] { new DTO.Management.Type() { id = permissions[12].SubModuleId, status = permissions[12].Status } };
                    roleUserDTO.historyreport = new Historyreport[1] { new Historyreport() { id = permissions[13].SubModuleId, status = permissions[13].Status } };
                    roleUserDTO.getlog = new Getlog[1] { new Getlog() { id = permissions[14].SubModuleId, status = permissions[14].Status } };
                    roleUserDTO.queriesandmatches = new Queriesandmatch[1] { new Queriesandmatch() { id = permissions[15].SubModuleId, status = permissions[15].Status } };
                    roleUserDTO.coincidencedetailing = new Coincidencedetailing[1] { new Coincidencedetailing() { id = permissions[16].SubModuleId, status = permissions[16].Status } };
                    roleUserDTO.viewpastconsultations = new Viewpastconsultation[1] { new Viewpastconsultation() { id = permissions[17].SubModuleId, status = permissions[17].Status } };
                    roleUserDTO.certificationlistupdates = new Certificationlistupdate[1] { new Certificationlistupdate() { id = permissions[18].SubModuleId, status = permissions[18].Status } };
                    roleUserDTO.parameterscategory = new Parameterscategory[1] { new Parameterscategory() { id = permissions[19].SubModuleId, status = permissions[19].Status } };
                    roleUserDTO.news = new News[1] { new News() { id = permissions[20].SubModuleId, status = permissions[20].Status } };
                    roleUserDTO.roi = new Roi[1] { new Roi() { id = permissions[21].SubModuleId, status = permissions[21].Status } };
                    roleUserDTO.faqs = new Faq[1] { new Faq() { id = permissions[22].SubModuleId, status = permissions[22].Status } };
                    roleUserDTO.signal = new Signal[1] { new Signal() { id = permissions[23].SubModuleId, status = permissions[23].Status } };
                    roleUserDTO.access = new DTO.Management.Access[1] { new DTO.Management.Access() { id = permissions[24].SubModuleId, status = permissions[24].Status } };
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine("");                
            }                        
            return roleUserDTO;
        }


        #endregion
    }
}
