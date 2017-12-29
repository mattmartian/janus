namespace Janus.Models
{
    public class MailViewModel
    {
        public int messageID { get; set; }

        public int mailFromUserID { get; set; }
        public string mailFromUsername { get; set; }

        public int mailToUserID { get; set; }
        public string mailToUsername { get; set; }

        public string subject { get; set; }

        public string body { get; set; }

        public int? shiftRequestID { get; set; }

        public bool isRead { get; set; }
    }
}