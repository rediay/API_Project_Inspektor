using AutoMapper;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Management;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Common.Services.Management
{
    public class CompanyService : BaseService, ICompanyService
    {
        private readonly IMapper _mapper;
        protected readonly ICompanyRepository _companyRepository;
        public CompanyService(ICurrentContextProvider contextProvider, ICompanyRepository companyRepository, IMapper mapper) : base(contextProvider)
        {
            this._companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CompanyDTO> GetCompany()
        {
            var company = await _companyRepository.GetCompany(Session);
            var map = company.MapTo<CompanyDTO>();
            return map;
        }

        public async Task<List<CompanyDTO>> GetAllCompanies()
        {
            var companies = await _companyRepository.GetAllCompanies(Session);
            var map = companies.MapTo<List<CompanyDTO>>();
            return map;
        }


        public async Task<CompanyDTO> UpdateCompany(CompanyDTO companyDTO)
        {
            try
            {

                //companyDTO.ContractDate = Convert.ToDateTime(companyDTO.ContractDate).ToString("yyyy-MM-dd");
                var company = companyDTO.MapTo<Company>();

                var companyDTOres = await _companyRepository.UpdateCompany(company, Session);
                return companyDTOres.MapTo<CompanyDTO>();
            }
            catch (Exception ex)
            {
                return await new Task<CompanyDTO>(null);
            }
        }

        public async Task<bool> DeleteCompany(int id)
        {
            try
            {
                return await _companyRepository.DeleteCompany(id, Session);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }

        }
        
    }
}
