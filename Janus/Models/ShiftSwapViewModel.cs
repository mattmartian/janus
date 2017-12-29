using System;

namespace Janus.Models
{
    public class ShiftSwapViewModel
    {
        public int shiftRequestID { get; set; }

        public string managerSignOff { get; set; }

        public string requestor { get; set; }

        public int requestorID { get; set; }

        public string requestorShift { get; set; }

        public string requestWith { get; set; }

        public int requestWithID { get; set; }

        public string requestWithShift { get; set; }
        public Nullable<bool> requestConfirmed { get; set; }

        public string requestStatus { get; set; }
    }
}