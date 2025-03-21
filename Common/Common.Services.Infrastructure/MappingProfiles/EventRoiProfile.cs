using AutoMapper;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class EventRoiProfile: Profile
    {
        public EventRoiProfile()
        {
            CreateMap<EventRoi, EventRoiDTO>().ReverseMap();
        }
    }
}