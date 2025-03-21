using Common.DTO.Management;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Management
{
    public interface IAccessSubModulesService
    {

        Task<AccessSubModulesDTO> Update(AccessSubModulesDTO accessSubModulesDTO);
        Task<AccessSubModulesDTO> GetAccessJson(int id);
      


    }
}