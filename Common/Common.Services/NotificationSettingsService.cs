/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using AutoMapper;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Utils;
using System.Threading.Tasks;

namespace Common.Services
{
    public class NotificationSettingsService : BaseService, INotificationSettingsService
    {
        protected readonly INotificationSettingsRepository notificationSettingsRepository;
        private readonly IMapper _mapper;
        public NotificationSettingsService(ICurrentContextProvider contextProvider, INotificationSettingsRepository notificationSettingsRepository, IMapper mapper) : base(contextProvider)
        {
            this.notificationSettingsRepository = notificationSettingsRepository;
            _mapper = mapper;
        }

     

        public async Task<NotificationSettingsDTO> Edit(NotificationSettingsDTO dto)
        {
            var settings = dto.MapTo<NotificationSettings>();
            await notificationSettingsRepository.Edit(settings, Session);
            return settings.MapTo<NotificationSettingsDTO>();
        }

        public async Task<NotificationSettingsDTO> GetByCompanyId(int CompanyId)
        {

            var settings = await notificationSettingsRepository.GetByCompanyId(CompanyId, Session);
            var map = settings.MapTo<NotificationSettingsDTO>();
            return map;
        }

      
    }
}
