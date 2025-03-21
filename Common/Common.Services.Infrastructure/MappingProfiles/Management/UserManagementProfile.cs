using AutoMapper;
using Common.DTO.Users;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class UserManagementProfile: Profile
    {
        public UserManagementProfile()
        {
            CreateMap<User, UserManagementDto>().ReverseMap();
        }
    }
}