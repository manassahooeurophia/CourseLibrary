using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PluralsightCourseLib.API.Model
{
    public abstract class CourseManipulationDto : IValidatableObject
    {
        [Required(ErrorMessage = "Title field is required")]
        [MaxLength(100, ErrorMessage = "Maximum 100 char is allowed for Title")]
        public string Title { get; set; }

        public virtual string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == Description)
            {
                yield return new ValidationResult("Ttile cannot be same as Description", new[] { "CourseManipulationDto" });
            }
        }
    }
}
