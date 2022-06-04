using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApplication.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string FIO { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public int Experience { get; set; }
        public DateTime HireDate { get; set; }

    }
}
