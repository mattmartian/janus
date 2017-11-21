using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Models
{
    public class ClaimsVIewModel
    {
        public int claimID { get; set; }

        public int userID { get; set; }

        public string startTime { get; set; }

        public string endTime { get; set; }

        public string description { get; set; }

        public string claimType { get; set; }

        public Nullable<bool> isApproved { get; set; }
    }
}