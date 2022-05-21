﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApplication.Models
{
    public class EmployeeTest
    {
        [Key]
        public int EmpTestId { get; set; }
        public String EmployeeName { get; set; }
        public String Author { get; set; }
        public String AuthorId { get; set; }
        public int Questiont1 { get; set; }
        public int Questiont2 { get; set; }
        public int Questiont3 { get; set; }
        public int Questiont4 { get; set; }
        public int Questiont5 { get; set; }
        public int Questiont6 { get; set; }
        public int Questiont7 { get; set; }
        public int Questiont8 { get; set; }
        public int Questiont9 { get; set; }
        public int Questiont10 { get; set; }
        public int Total { get; set; }



    }
}
