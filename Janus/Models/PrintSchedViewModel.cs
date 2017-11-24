using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Models
{
    public class PrintSchedViewModel
    {
        public int userID { get; set; }

        public int shiftStart { get; set; }

        public int shiftEnd { get; set; }

        public string day { get; set; }

        public string position { get; set; }
    }
}