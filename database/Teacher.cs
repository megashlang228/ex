namespace ex.database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Teacher")]
    public partial class Teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Teacher()
        {
            PersonalCard = new HashSet<PersonalCard>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string FIO { get; set; }

        public int PostId { get; set; }

        public int SubjectId { get; set; }

        public int Rate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonalCard> PersonalCard { get; set; }

        public virtual Post Post { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
