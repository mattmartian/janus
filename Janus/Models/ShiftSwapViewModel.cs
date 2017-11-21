using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Models
{
    public class ShiftSwapViewModel
    {
        public int shiftRequestID { get; set; }

        public string managerSignOff { get; set; }

        public string requestor { get; set; }

        public int requestorID { get; set; }

        public int requestorShift { get; set; }

        public string requestWith { get; set; }

        public int requestWithID { get; set; }

        public int requestWithShift { get; set; }
        public Nullable<bool> requestConfirmed { get; set; }

        public string requestStatus { get; set; }
    }
}