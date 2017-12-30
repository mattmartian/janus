using System;

namespace Janus.Models
{
    public class ClaimsVIewModel
    {
        public int claimID { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }

        public string startTime { get; set; }

        public string endTime { get; set; }

        public string description { get; set; }

        public string claimType { get; set; }

        public Nullable<bool> isApproved { get; set; }
    }
}