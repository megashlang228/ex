namespace ex.database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Otsenka = new HashSet<Otsenka>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string FIO { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirthday { get; set; }

        [Required]
        [StringLength(12)]
        public string ParentNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public int ClassId { get; set; }

        public virtual Class Class { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Otsenka> Otsenka { get; set; }
    }
}
