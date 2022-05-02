using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApplication.Models
{
    public class Qualification
    {
        public int QualificationId { get; set; }
        public string FIO { get; set; }
        public string CourseName { get; set; }
        public DateTime DateOfStart { get; set; }
        public int Grade { get; set; }        

        //public int CourseId { get; set; }
        //public int JobId { get; set; }

    }
}
