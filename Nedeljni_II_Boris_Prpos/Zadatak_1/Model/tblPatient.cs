//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zadatak_1.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPatient
    {
        public int PatientID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string CardNumber { get; set; }
        public Nullable<System.DateTime> DateExpire { get; set; }
        public Nullable<int> DoctorID { get; set; }
    
        public virtual tblDoctor tblDoctor { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}