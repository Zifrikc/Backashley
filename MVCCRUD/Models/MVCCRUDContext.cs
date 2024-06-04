using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVCCRUD.Models
{
    public partial class MVCCRUDContext : DbContext
    {
        public MVCCRUDContext()
        {
        }

        public MVCCRUDContext(DbContextOptions<MVCCRUDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Worker> Workers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                // optionsBuilder.UseSqlServer("server=localhost; database=MVCCRUD; integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>(entity =>
            {
                entity.Property(e => e.fullName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.lastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.typeDocument)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.numberDocument)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.birthDate).HasColumnType("date");

                entity.Property(e => e.entryDate).HasColumnType("date");

                entity.Property(e => e.numberChildren).HasColumnType("int");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
