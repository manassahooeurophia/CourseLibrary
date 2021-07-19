using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PluralsightCourseLib.API.Model
{
    public class AuthorToCreateDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public string MainCategory { get; set; }

        public ICollection<CourseToCreateDto> courses { get; set; }
            = new List<CourseToCreateDto>();

    }

}
