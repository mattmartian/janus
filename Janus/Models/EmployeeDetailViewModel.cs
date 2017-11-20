using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Models
{
    public class EmployeeDetailViewModel
    {
        public int userID { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string role { get; set; }

        public string departmentName { get; set; }
    }
}