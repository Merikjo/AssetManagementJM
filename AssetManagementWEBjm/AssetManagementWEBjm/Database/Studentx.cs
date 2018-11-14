//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssetManagementWEBjm.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Studentx
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Studentx()
        {
            this.Phone = new HashSet<Phone>();
            this.PostOffices = new HashSet<PostOffices>();
            this.Reservation = new HashSet<Reservation>();
            this.TreatmentReport = new HashSet<TreatmentReport>();
            this.User = new HashSet<User>();
        }
    
        public int Student_id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identity { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> EnrollmentDateIN { get; set; }
        public Nullable<System.DateTime> EnrollmentDateOUT { get; set; }
        public Nullable<System.DateTime> EnrollmentDateOFF { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> LastModifiedAt { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
        public Nullable<bool> Active { get; set; }
        public string Information { get; set; }
        public Nullable<int> StudentGroup_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phone> Phone { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostOffices> PostOffices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservation { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TreatmentReport> TreatmentReport { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> User { get; set; }
    }
}
