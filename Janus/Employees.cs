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
    
    public partial class Employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employees()
        {
            this.shiftRequests = new HashSet<shiftRequests>();
            this.Shifts = new HashSet<Shifts>();
            this.AbsenceClaims = new HashSet<AbsenceClaims>();
        }
    
        public int employeeID { get; set; }
        public int userID { get; set; }
        public int availibilityID { get; set; }
        public System.DateTime hireDate { get; set; }
        public System.DateTime fireDate { get; set; }
        public string position { get; set; }
        public string section { get; set; }
        public string manager { get; set; }
        public string employmentStatus { get; set; }
    
        public virtual Availibility Availibility { get; set; }
        public virtual Users User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shiftRequests> shiftRequests { get; set; }
        public virtual Company Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shifts> Shifts { get; set; }
        public virtual Manager Manager { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AbsenceClaims> AbsenceClaims { get; set; }
        public virtual Addresses Address { get; set; }
    }
}
