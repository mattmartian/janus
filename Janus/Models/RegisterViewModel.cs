using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Models
{
    public class RegisterViewModel
    {
        public string companyID { get; set; }

        public string departmentID { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string birthDate { get; set; }

        public string password { get; set; }

        public string phone { get; set; }

        public string email { get; set; }

        public string streetAddress { get; set; }

        public string postalCode { get; set; }

        public DateTime hireDate { get; set; }

        public DateTime fireDate { get; set; }

        public string employmentStatus { get; set; }
    }
}