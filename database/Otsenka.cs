namespace ex.database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Otsenka")]
    public partial class Otsenka
    {
        public int Id { get; set; }

        public int JournalId { get; set; }

        public int SubjectId { get; set; }

        public int StudentId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Column("Otsenka")]
        [Required]
        [StringLength(1)]
        public string Otsenka1 { get; set; }

        public virtual Journal Journal { get; set; }

        public virtual Student Student { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
