using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Services;

namespace Common.Services
{
    public class AdditionalCompanyServiceService : BaseService, IAdditionalCompanyServiceService
    {
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IAdditionalCompanyServiceRepository _additionalCompanyServiceRepository;

        public AdditionalCompanyServiceService(ICurrentContextProvider contextProvider,
            IAdditionalServiceRepository additionalServiceRepository,
            IAdditionalCompanyServiceRepository additionalCompanyServiceRepository) : base(contextProvider)
        {
            _additionalServiceRepository = additionalServiceRepository;
            _additionalCompanyServiceRepository = additionalCompanyServiceRepository;
        }

        public async Task<ResponseDTO<List<AdditionalCompanyService>>> GetAll(int companyId)
        {
            var records = await _additionalCompanyServiceRepository.GetAll(Session, companyId);
            var response = new ResponseDTO<List<AdditionalCompanyService>>(records);
            return response;
        }

        public async Task<ResponseDTO<AdditionalCompanyService>> Edit(int companyId, AdditionalCompanyService dto)
        {
            var record = await _additionalCompanyServiceRepository.Edit(dto, Session);
            var response = new ResponseDTO<AdditionalCompanyService>(record);
            return response;
        }

        public async Task<ResponseDTO<List<AdditionalCompanyService>>> Create(int companyId)
        {
            var additionalServices = await _additionalServiceRepository.GetAll(Session);
            var additionalCompanyServices = new List<AdditionalCompanyService>();

            foreach (var additionalService in additionalServices)
            {
                var additionalServiceId = additionalService.Id;
                var additionalCompanyService = new AdditionalCompanyService
                {
                    Active = true,
                    CompanyId = (int) companyId, 
                    AdditionalServiceId = additionalServiceId
                };
                    
                var newAdditionalCompanyService = await _additionalCompanyServiceRepository.Edit(additionalCompanyService, Session);
                additionalCompanyServices.Add(newAdditionalCompanyService);
            }
            
            var response = new ResponseDTO<List<AdditionalCompanyService>>(additionalCompanyServices);
            return response;
        }
    }
}