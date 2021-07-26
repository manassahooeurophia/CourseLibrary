using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PluralsightCourseLib.API.Model
{
    public class CourseToUpdateDto : CourseManipulationDto
    {
        [Required(ErrorMessage ="You should fill out Description")]
        public override string Description { get => base.Description; set => base.Description=value; }
    }
}
