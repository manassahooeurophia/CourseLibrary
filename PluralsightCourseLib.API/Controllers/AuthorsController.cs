using PluralsightCourseLib.API.Services;
using Microsoft.AspNetCore.Mvc;
using PluralsightCourseLib.API.Helpers;
using PluralsightCourseLib.API.Model;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PluralsightCourseLib.API.Entities;

namespace PluralsightCourseLib.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController: ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository,IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public IActionResult GetAuthors([FromQuery] string mainCategory,string searchQuery)
        {
            var authorFromRepo= _courseLibraryRepository.GetAuthors(mainCategory,searchQuery);

            return new JsonResult(_mapper.Map<IEnumerable<AuthorDto>>(authorFromRepo));
             
        }

        [HttpGet("{authorID}",Name ="GetAuthor")]
        public IActionResult GetAuthor(Guid authorID)
        {
            var response = _courseLibraryRepository.GetAuthor(authorID);

            if (response == null)
            {
                return NotFound();
            }   
            return Ok(_mapper.Map<AuthorDto>(response));
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorToCreateDto authortoCreateDto)
        {
            var author = _mapper.Map<Author>(authortoCreateDto);
            _courseLibraryRepository.AddAuthor(author); 
            _courseLibraryRepository.Save();

            var authorToReturn = _mapper.Map<AuthorDto>(author);
            return CreatedAtRoute("GetAuthor", new { authorID = authorToReturn.Id }, authorToReturn);
            
        }


        [HttpOptions]
        public IActionResult GetAuthorOptions()
        {
            Response.Headers.Add("Allow", "Get,Post,Patch,Delete,Post");
            return Ok();
        }



    }
}
