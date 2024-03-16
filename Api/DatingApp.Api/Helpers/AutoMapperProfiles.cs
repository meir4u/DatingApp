using AutoMapper;
using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;
using DatingApp.Api.Extensions;

namespace DatingApp.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.Age, opt=> opt.MapFrom(src=>src.DateOfBirth.CalculateAge()))
                .ForMember(dest=>dest.PhotoUrl, opt=>opt.MapFrom(src=>src.Photos.DefaultIfEmpty<Photo>().FirstOrDefault().Url))
                //.ReverseMap()
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos));

            CreateMap<Photo, PhotoDto>().ReverseMap();
        }
    }
}
