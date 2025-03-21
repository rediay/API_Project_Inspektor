/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using AutoMapper;
using Common.DTO;
using Common.DTO.Queries;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Queries;
using Common.Utils;
using System.Threading.Tasks;
using Common.DTO.OwnLists;
using Common.DTO.IndividualQueryExternal;

namespace Common.Services
{
    public class IndividualQueryService : BaseService, IIndividualQueryService
    {
        protected readonly IIndividualQueryRepository individualQueryRepository;
        private readonly IMapper _mapper;
        public IndividualQueryService(ICurrentContextProvider contextProvider, IIndividualQueryRepository individualQueryRepository, IMapper mapper) : base(contextProvider)
        {
            this.individualQueryRepository = individualQueryRepository;
            _mapper = mapper;
        }

        public async Task<IndividualQueryResponseDTO> makeQuery(IndividualQueryParamsDTO dto)
        {
           return  await individualQueryRepository.makeQuery(dto, Session);
        }
        public async Task<IndividualQueryExternalResponseEsDTO> makeQueryExternal(IndividualQueryExternalParamsDTO dto)
        {
           return  await individualQueryRepository.makeQueryExternal(dto, Session);
        }
        public async Task<QueryDTO> previusQuery(IndividualQueryParamsDTO dto)
        {
           return  await individualQueryRepository.previusQuery(dto, Session);
        }
        public async Task<IndividualQueryResponseDTO> getQuery(int idQuery)
        {
           return  await individualQueryRepository.getQuery(idQuery, Session);
        }

        //public async Task<NotificationSettingsDTO> Edit(NotificationSettingsDTO dto)
        //{
        //    var settings = dto.MapTo<NotificationSettings>();
        //    await notificationSettingsRepository.Edit(settings, Session);
        //    return settings.MapTo<NotificationSettingsDTO>();
        //}

        //public async Task<NotificationSettingsDTO> GetByCompanyId(int CompanyId)
        //{

        //    var settings = await notificationSettingsRepository.GetByCompanyId(CompanyId, Session);
        //    var map = settings.MapTo<NotificationSettingsDTO>();
        //    return map;
        //}


    }
}
