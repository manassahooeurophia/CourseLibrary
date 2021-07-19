using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PluralsightCourseLib.API.Entities;
using PluralsightCourseLib.API.Model;
using PluralsightCourseLib.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PluralsightCourseLib.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CourseController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICourseLibraryRepository _courselibrary;

        public CourseController(IMapper mapper,ICourseLibraryRepository courseLibrary)
        {
            _mapper = mapper?? throw new ArgumentNullException();
            _courselibrary = courseLibrary?? throw new ArgumentNullException();
        }

        [HttpGet]
        public IActionResult GetCourses(Guid authorid)
        {
            var response = _courselibrary.GetCourses(authorid);
            if (response == null)
            {
                return new NotFoundResult();
            }
            return new JsonResult(_mapper.Map<IEnumerable <CourseDto>>(response));

        }
        
        
        [HttpGet("{courseId}",Name="GetCourse")]
        public IActionResult GetCourse(Guid authorid,Guid courseid)
        {
            var response = _courselibrary.GetCourse(authorid, courseid);
            if (response == null)
            {
                return NotFound();
            }
            return new JsonResult(_mapper.Map<CourseDto>(response));
        }

        [HttpPost]
        public ActionResult<CourseDto> CreateCourse(Guid authorID,CourseToCreateDto CoursetoCreateDto)
        {
            if (!_courselibrary.AuthorExists(authorID))
            {
                return NotFound();
            }
            var course = _mapper.Map<Course>(CoursetoCreateDto);
            _courselibrary.AddCourse(authorID, course);
            _courselibrary.Save();

            var courseToReturn = _mapper.Map<CourseDto>(course);

            return CreatedAtRoute("GetCourse", new { authorid = authorID, courseid = courseToReturn.Id }, courseToReturn);
        }

    }
}
