using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PluralsightCourseLib.API.Helpers;

namespace PluralsightCourseLib.API.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Entities.Author, Model.AuthorDto>()
                 .ForMember(
                        dest => dest.Name,
                        opt => opt.MapFrom(x => $"{x.FirstName} {x.LastName}"))
                 .ForMember(
                        dest => dest.Age,
                        opt => opt.MapFrom(x => x.DateOfBirth.GetCurrentAge()));
            CreateMap<Model.AuthorToCreateDto, Entities.Author>();
              
               
        }
         
    }
}
