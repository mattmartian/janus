//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Janus
{
    using System;
    using System.Collections.Generic;
    
    public partial class Messages
    {
        public int messageID { get; set; }
        public int mailFromUserID { get; set; }
        public string mailFromUsername { get; set; }
        public int mailToUserID { get; set; }
        public string mailToUsername { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public bool isRead { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual Users Users1 { get; set; }
    }
}
