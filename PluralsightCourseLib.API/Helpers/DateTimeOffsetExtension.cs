using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PluralsightCourseLib.API.Helpers
{
    public static class DateTimeOffsetExtension
    {
        public static int GetCurrentAge(this DateTimeOffset dob)
        {
            var currentYear = DateTime.UtcNow.Year;
            var dobyear = dob.Year;

            return currentYear-dobyear;
        }
    }
}
