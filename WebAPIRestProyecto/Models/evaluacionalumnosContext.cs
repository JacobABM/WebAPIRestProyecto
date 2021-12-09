using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAPIRestProyecto.Models
{
    public partial class evaluacionalumnosContext : DbContext
    {
        public evaluacionalumnosContext()
        {
        }

        public evaluacionalumnosContext(DbContextOptions<evaluacionalumnosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Evaluacion> Evaluacion { get; set; }
        public virtual DbSet<Materia> Materia { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("latin1");

            modelBuilder.Entity<Evaluacion>(entity =>
            {
                entity.ToTable("evaluacion");

                entity.HasIndex(e => e.IdMateria, "fkMateria");

                entity.HasIndex(e => e.IdAlumno, "fkUsuarioAlumno");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.IdAlumno).HasColumnType("int(11)");

                entity.Property(e => e.IdMateria).HasColumnType("int(11)");

                entity.Property(e => e.PrimeraEvaluacion).HasPrecision(3, 1);

                entity.Property(e => e.SegundaEvaluacion).HasPrecision(3, 1);

                entity.Property(e => e.TerceraEvaluacion).HasPrecision(3, 1);

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.Evaluacion)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkUsuarioAlumno");

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.Evaluacion)
                    .HasForeignKey(d => d.IdMateria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkMateria");
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.ToTable("materia");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.NombreMateria)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(1);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
