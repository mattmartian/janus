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
    
    public partial class shiftRequests
    {
        public int shiftRequestID { get; set; }
        public int managerID { get; set; }
        public string requestor { get; set; }
        public string requestWith { get; set; }
        public bool requestConfirmed { get; set; }
        public string requestStatus { get; set; }
    
        public virtual Employees Employee { get; set; }
    }
}
