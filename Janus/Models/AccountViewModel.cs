using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Models
{
    public class AccountViewModel
    {
        public int userID { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string birthDate { get; set; }

        public string phone { get; set; }

        public string email { get; set; }

        public string streetAddress { get; set; }

        public string postalCode { get; set; }

        public string role { get; set; }

        public string departmentName { get; set; }

        public DateTime hireDate { get; set; }
    }
}