using AutoMapper;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class NotificationsMonitoringProfile : Profile
    {
        public NotificationsMonitoringProfile()
        {
            CreateMap<Notification, NotificationsMonitoringDTO>()                
                .ReverseMap();
        }
    }
}