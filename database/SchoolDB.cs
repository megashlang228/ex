using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ex.database
{
    public partial class SchoolDB : DbContext
    {
        public SchoolDB()
            : base("name=SchoolDB")
        {
        }

        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Journal> Journal { get; set; }
        public virtual DbSet<Otsenka> Otsenka { get; set; }
        public virtual DbSet<PersonalCard> PersonalCard { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<ClassStudent> ClassStudent { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Journal)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Student)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Journal>()
                .HasMany(e => e.Otsenka)
                .WithRequired(e => e.Journal)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Otsenka>()
                .Property(e => e.Otsenka1)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.PersonalCard)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Teacher)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<School>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<School>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<School>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<School>()
                .Property(e => e.Number)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.FIO)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.ParentNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Otsenka)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Otsenka)
                .WithRequired(e => e.Subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.PersonalCard)
                .WithRequired(e => e.Subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Teacher)
                .WithRequired(e => e.Subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.FIO)
                .IsUnicode(false);

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.PersonalCard)
                .WithRequired(e => e.Teacher)
                .WillCascadeOnDelete(false);
        }
    }
}
