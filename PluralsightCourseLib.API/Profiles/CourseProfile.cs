using AutoMapper;
using PluralsightCourseLib.API.Entities;
using PluralsightCourseLib.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PluralsightCourseLib.API.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<CourseToCreateDto, Course>();
            CreateMap<CourseToUpdateDto, Course>();
            CreateMap<Course, CourseToUpdateDto>();
        }
    }
}
