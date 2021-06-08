using AutoMapper;
using HospitalAPI.Core.Dtos;
using HospitalAPI.Core.Models;

namespace HospitalAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(d => d.UserId, o => o.MapFrom(i => i.Id));
        }
    }
}
