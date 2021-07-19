using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PluralsightCourseLib.API.Model
{
    public class CourseToCreateDto
    {
        [Required]
        public string Title { get; set; }

       
        [MaxLength(15000)]
        public string Description { get; set; }
    }
}
