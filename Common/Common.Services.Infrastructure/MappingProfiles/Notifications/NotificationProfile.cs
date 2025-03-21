using AutoMapper;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationSentDTO>()
                .ForMember(d => d.User, opt => opt.MapFrom(src => src.User.Name))
                .ReverseMap();
        }
    }
}