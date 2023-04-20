namespace ex.database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonalCard")]
    public partial class PersonalCard
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public int PostId { get; set; }

        public int SubjectId { get; set; }

        public int Rate { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public virtual Post Post { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
