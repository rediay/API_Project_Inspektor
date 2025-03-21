using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Users;
using Common.Entities;

namespace Common.Services.Infrastructure.Services
{
    public interface IAdditionalCompanyServiceService
    {
        Task<ResponseDTO<List<AdditionalCompanyService>>> GetAll(int companyId);
        Task<ResponseDTO<AdditionalCompanyService>> Edit(int companyId, AdditionalCompanyService dto);
        Task<ResponseDTO<List<AdditionalCompanyService>>> Create(int companyId);
    }
}