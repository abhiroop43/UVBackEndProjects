//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackEnd.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class CourseEnrollment
    {
        public string EnrollmentID { get; set; }
        public string CourseID { get; set; }
        public string StudentID { get; set; }
        public System.DateTime EnrolledFrom { get; set; }
        public System.DateTime EnrolledTo { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Cours Cours { get; set; }
    }
}