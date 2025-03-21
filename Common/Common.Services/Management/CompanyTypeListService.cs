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
    public class CompanyTypeListService : BaseService, ICompanyTypeListService
    {
        private readonly IMapper _mapper;
        protected readonly ICompanyTypeListRepository _companyTypeListRepository;
        public CompanyTypeListService(ICurrentContextProvider contextProvider, ICompanyTypeListRepository companyTypeListRepository, IMapper mapper) : base(contextProvider)
        {
            this._companyTypeListRepository = companyTypeListRepository;
            _mapper = mapper;
        }

        public async Task<List<CompanyTypeListDTO>> GetTypeList()
        {
            var companylist  = await _companyTypeListRepository.GetTypeList( Session);
            var map = companylist.MapTo<List<CompanyTypeListDTO>>();            
            return map;
        }
      

        public async Task<bool> UpdateTypeList(CompanyTypeListDTO typeListDto)
        {
            try
            {
                
                    var typelist = typeListDto.MapTo<CompanyTypeList>();
                    return await _companyTypeListRepository.UpdateTypeList(typelist, Session);
                
            }catch(Exception ex)
            {
                return await Task.FromResult(false); 
            }
        }
        public async Task<bool> UpdateRangeTypeList(bool status)
        {
            try
            {
                                
                return await _companyTypeListRepository.UpdateRangeTypeList(status, Session);

            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }



    }
}
