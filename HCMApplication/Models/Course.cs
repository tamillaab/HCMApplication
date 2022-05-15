using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApplication.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Format { get; set; }
        public string Validity { get; set; }
        public int Period { get; set; }
        public DateTime AddDate { get; set; }




        //public int CourseCalendarId { get; set; }

    }
}
