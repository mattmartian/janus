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
    
    public partial class Availibility
    {
        public int availibilityID { get; set; }
        public int employeeID { get; set; }
        public System.DateTime startTime { get; set; }
        public System.DateTime endTime { get; set; }
        public string day { get; set; }
    
        public virtual Employees Employee { get; set; }
    }
}
