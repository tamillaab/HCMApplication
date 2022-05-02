using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApplication.Models
{
    public class CourseCalendar
    {
        public int CourseCalendarId { get; set; }
        public string Name { get; set; }
        public int Period { get; set; }
        public string Mentor { get; set; }
        public int Participants { get; set; }
        public DateTime DateOfStart { get; set; }
    }
}
