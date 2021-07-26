using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPut("{courseId}")]
        public IActionResult UpdateCourse(Guid authorId,Guid courseId,CourseToUpdateDto coursetoUpdate)
        {
            if (!_courselibrary.AuthorExists(authorId))
            {
                return NotFound();
            }
            var author = _courselibrary.GetAuthor(authorId);
            var courseFromRepo = _courselibrary.GetCourse(author.Id, courseId);
            if (courseFromRepo == null)
            {
                var courseToAdd=_mapper.Map<Course>(coursetoUpdate);
                _courselibrary.AddCourse(authorId, courseToAdd);
                _courselibrary.Save();

                var courseToReturn=_mapper.Map<CourseDto>(courseToAdd);

                return CreatedAtRoute("GetCourse", new { authorId, courseId = courseToReturn.Id },courseToReturn);
            }

            _mapper.Map(coursetoUpdate, courseFromRepo);

            _courselibrary.UpdateCourse(courseFromRepo);
            _courselibrary.Save();

            return NoContent();
        }

        [HttpPatch("{courseId}")]
        public ActionResult PartiallyUpdateCourse(Guid authorId, Guid courseId,
            JsonPatchDocument<CourseToUpdateDto> patchdocument)
        {
            if (!_courselibrary.AuthorExists(authorId))
            {
                return NotFound();
            }
            var author = _courselibrary.GetAuthor(authorId);
            var courseFromRepo = _courselibrary.GetCourse(author.Id, courseId);
            if (courseFromRepo == null)
            {
                // return NotFound();
                var courseToUpdate = new CourseToUpdateDto();
                patchdocument.ApplyTo(courseToUpdate, ModelState);
                if (!TryValidateModel(courseToUpdate))
                {
                    return ValidationProblem(ModelState);
                }

                var courseToAdd = _mapper.Map<Course>(courseToUpdate);
                courseToAdd.Id = courseId;

                _courselibrary.AddCourse(authorId, courseToAdd);
                _courselibrary.Save();

                var courseToReturn = _mapper.Map<CourseDto>(courseToAdd);
                return CreatedAtRoute("GetCourse", new { authorId, courseId }, courseToReturn);

            }
            var courseToPatch = _mapper.Map<CourseToUpdateDto>(courseFromRepo);
            patchdocument.ApplyTo(courseToPatch,ModelState);

            if (!TryValidateModel(courseToPatch))
            {
                return ValidationProblem(ModelState);
            }

            var mapped=_mapper.Map(courseToPatch, courseFromRepo);
            _courselibrary.UpdateCourse(courseFromRepo);
            _courselibrary.Save();

            return NoContent();

        }

        [HttpDelete("{courseId}")]
        public ActionResult DeleteCourse(Guid authorId,Guid courseId)
        {
            var author = _courselibrary.AuthorExists(authorId);
            if (!_courselibrary.AuthorExists(authorId))
            {
                return NotFound();
            }

            var course = _courselibrary.GetCourse(authorId, courseId);
            if(course == null)
            {
                return NotFound();
            }
            _courselibrary.DeleteCourse(course);
            _courselibrary.Save();

            return NoContent();
        }

    }
}
