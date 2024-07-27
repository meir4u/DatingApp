using AutoMapper;
using DatingApp.Application.DTOs.Member;
using DatingApp.Application.DTOs.Message;
using DatingApp.Application.DTOs.Photo;
using DatingApp.Application.DTOs.Register;
using DatingApp.Application.Helpers;
using DatingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingApp.Application.Profiles
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()))
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.DefaultIfEmpty<Photo>().FirstOrDefault().Url))
                //.ReverseMap()
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos));

            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Photo, PhotoForApprovalDto>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.KnownAs)).ReverseMap();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<Message, MessageDto>()
                .ForMember(d => d.SenderPhotoUrl, o => o.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.RecipientPhotoUrl, o => o.MapFrom(s => s.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));

            CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
            CreateMap<DateTime?, DateTime?>().ConvertUsing(d => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null);
        }
    }
}
