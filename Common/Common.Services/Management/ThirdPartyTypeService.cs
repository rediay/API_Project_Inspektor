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

namespace Common.Services.Management
{
    public class ThirdPartyTypeService : BaseService, IThirdPartyTypeService
    {
        private readonly IMapper _mapper;
        protected readonly IThirdPartyTypeRepository _thirdPartyTypeRepository;

        public ThirdPartyTypeService(ICurrentContextProvider contextProvider, IThirdPartyTypeRepository companyTypeListRepository, IMapper mapper) : base(contextProvider)
        {
            this._thirdPartyTypeRepository = companyTypeListRepository;
            _mapper = mapper;
        }

        public async Task<List<ThirdPartyTypeDTO>> GetByCompanyID()
        {
            var thirPartyType = await _thirdPartyTypeRepository.GetByCompanyId( Session);
            var map = thirPartyType.MapTo<List<ThirdPartyTypeDTO>>();
            return map;
        }
        public async Task<ThirdPartyTypeDTO> UpdateThirdPartyType(ThirdPartyTypeDTO thirdPartyTypeDTO)
        {
            try
            {
                var thirdPartyType = thirdPartyTypeDTO.MapTo<ThirdPartyType>();
                var objthirdPartyType = await _thirdPartyTypeRepository.UpdateThirdPartyType(thirdPartyType, Session);
                var dto =objthirdPartyType.MapTo<ThirdPartyTypeDTO>();
                dto.CompanyId = objthirdPartyType.CompanyId;
                dto.UserId = (int) objthirdPartyType.UserId;
                return dto;

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ThirdPartyTypeDTO());
            }
        }


        public async Task<bool> DeleteThirdPartyType(int id)
        {
            try
            {
                return await _thirdPartyTypeRepository.DeleteThirdPartyType(id, Session);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }
    }
}
