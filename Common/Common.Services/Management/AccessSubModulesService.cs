using AutoMapper;
using Common.DTO.Management;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Management;
using Common.Services.Infrastructure.Repositories.Management;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Services.Management
{
    public class AccessSubModuleService : BaseService, IAccessSubModulesService
    {
        private readonly IMapper _mapper;
        protected readonly IAccessSubModulesRepository _accessSubModuleRepository;
        protected readonly IAccessService _accessService;
        public AccessSubModuleService(ICurrentContextProvider contextProvider, IAccessService accessService, IAccessSubModulesRepository accessSubModuleService, IMapper mapper) : base(contextProvider)
        {
            this._accessSubModuleRepository = accessSubModuleService;
            _accessService = accessService;
            _mapper = mapper;
        }

        public async Task<AccessSubModulesDTO> Update(AccessSubModulesDTO accessSubModulesDTO)
        {
            var accessSubModule = SetAccessSubModule(accessSubModulesDTO);
            var perByAccess = _accessSubModuleRepository.GetAccess(accessSubModulesDTO.accessId, Session).Result.ToList();
            for (int i = 0; i < accessSubModule.Count; i++)
            {
                perByAccess[i].Status = accessSubModule.ToList()[i].Status;
            }
            await _accessSubModuleRepository.Update(perByAccess, Session);
            return accessSubModulesDTO;
        }


        public async Task<AccessSubModulesDTO> GetAccessJson(int id)
        {
            var accessSubModule = await _accessSubModuleRepository.GetAccess(id, Session);
            return AccessSubModuleToRoleUserDTO(accessSubModule.ToList());
        }

        #region Map
        public List<AccessSubModule> SetAccessSubModule(AccessSubModulesDTO accessSubModulesDTO)
        {

            var accessSubModule = new List<AccessSubModule>();
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 1, Status = accessSubModulesDTO.logoempresa });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 2, Status = accessSubModulesDTO.changepassword });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 3, Status = accessSubModulesDTO.thirdparties });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 4, Status = accessSubModulesDTO.thirdparties });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 5, Status = accessSubModulesDTO.procurator });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 6, Status = accessSubModulesDTO.users });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 7, Status = accessSubModulesDTO.setting });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 8, Status = accessSubModulesDTO.sentto });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 9, Status = accessSubModulesDTO.monitoring });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 10, Status = accessSubModulesDTO.individual });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 11, Status = accessSubModulesDTO.massive });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 12, Status = accessSubModulesDTO.type });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 13, Status = accessSubModulesDTO.manager });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 14, Status = accessSubModulesDTO.historyreport });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 15, Status = accessSubModulesDTO.getlog });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 16, Status = accessSubModulesDTO.queriesandmatches });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 17, Status = accessSubModulesDTO.coincidencedetailing });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 18, Status = accessSubModulesDTO.viewpastconsultations });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 19, Status = accessSubModulesDTO.certificationlistupdates });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 20, Status = accessSubModulesDTO.parameterscategory });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 21, Status = accessSubModulesDTO.news });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 22, Status = accessSubModulesDTO.roi });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 23, Status = accessSubModulesDTO.faqs });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 24, Status = accessSubModulesDTO.signal });
            accessSubModule.Add(new AccessSubModule() { AccessId = accessSubModulesDTO.accessId, SubModuleId = 25, Status = accessSubModulesDTO.access });

            return accessSubModule;
        }

        public AccessSubModulesDTO AccessSubModuleToRoleUserDTO(List<AccessSubModule> accessSubModule)
        {

            AccessSubModulesDTO accessSubModulesDTO = new AccessSubModulesDTO();
            accessSubModulesDTO.accessId = accessSubModule.FirstOrDefault().AccessId;
            accessSubModulesDTO.nameAccess = _accessService.GetAccesByCompany().Result.FirstOrDefault(x => x.Id == accessSubModulesDTO.accessId).Name;
            accessSubModulesDTO.logoempresa = accessSubModule[0].Status;
            accessSubModulesDTO.changepassword = accessSubModule[1].Status;
            accessSubModulesDTO.thirdparties = accessSubModule[2].Status;
            accessSubModulesDTO.typeslistbyquery = accessSubModule[3].Status;
            accessSubModulesDTO.procurator = accessSubModule[4].Status;
            accessSubModulesDTO.users = accessSubModule[5].Status;
            accessSubModulesDTO.setting = accessSubModule[6].Status;
            accessSubModulesDTO.sentto = accessSubModule[7].Status;
            accessSubModulesDTO.monitoring = accessSubModule[8].Status;
            accessSubModulesDTO.individual = accessSubModule[9].Status;
            accessSubModulesDTO.manager = accessSubModule[10].Status;
            accessSubModulesDTO.massive = accessSubModule[11].Status;
            accessSubModulesDTO.type = accessSubModule[12].Status;
            accessSubModulesDTO.historyreport = accessSubModule[13].Status;
            accessSubModulesDTO.getlog = accessSubModule[14].Status;
            accessSubModulesDTO.queriesandmatches = accessSubModule[15].Status;
            accessSubModulesDTO.coincidencedetailing = accessSubModule[16].Status;
            accessSubModulesDTO.viewpastconsultations = accessSubModule[17].Status;
            accessSubModulesDTO.certificationlistupdates = accessSubModule[18].Status;
            accessSubModulesDTO.parameterscategory = accessSubModule[19].Status;
            accessSubModulesDTO.news = accessSubModule[20].Status;
            accessSubModulesDTO.roi = accessSubModule[21].Status;
            accessSubModulesDTO.faqs = accessSubModule[22].Status;
            accessSubModulesDTO.signal = accessSubModule[23].Status;
            accessSubModulesDTO.access = accessSubModule[24].Status;
            return accessSubModulesDTO;
        }




        #endregion
    }
}
