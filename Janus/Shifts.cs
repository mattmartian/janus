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
    
    public partial class Shifts
    {
        public int shiftID { get; set; }
        public int userID { get; set; }
        public string shiftDate { get; set; }
        public int shiftStart { get; set; }
        public int shiftEnd { get; set; }
        public string position { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
