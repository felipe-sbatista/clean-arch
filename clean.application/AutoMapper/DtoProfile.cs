using AutoMapper;
using clean.application.Models;
using clean.domain.Models;

namespace clean.application.AutoMapper
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();

        }
    }
}
