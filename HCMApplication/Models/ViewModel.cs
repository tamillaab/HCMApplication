﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApplication.Models
{
    public class ViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Qualification> Qualifications{ get; set; }

    }
}
