﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JanusEntities : DbContext
    {
        public JanusEntities()
            : base("name=JanusEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AbsenceClaims> AbsenceClaims { get; set; }
        public virtual DbSet<Availibility> Availibility { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<shiftRequests> shiftRequests { get; set; }
        public virtual DbSet<Shifts> Shifts { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}