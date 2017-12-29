namespace Janus.Models
{
    public class ShiftViewModel
    {
        public int shiftID { get; set; }
        public int userID { get; set; }

        public string shiftDate { get; set; }
        public int shiftStart { get; set; }

        public int shiftEnd { get; set; }

        public string position { get; set; }

        public string description { get; set; }

        public string status { get; set; }
    }
}