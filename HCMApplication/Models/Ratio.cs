using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApplication.Models
{
    public class Ratio
    {
        public int number9 { get; set; }
        public int number8 { get; set; }
        public int number7 { get; set; }
        public int number6 { get; set; }
        public int number5 { get; set; }

        public Ratio(int number9, int number8, int number7, int number6, int number5)
        {
            this.number9 = number9;
            this.number8 = number8;
            this.number7 = number7;
            this.number6 = number6;
            this.number5 = number5;
        }
    }
}
