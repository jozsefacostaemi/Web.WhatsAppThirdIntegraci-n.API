using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web.WhatsAppThirdIntegración.API.Infraestructure.Persistence.Entities;

public partial class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Processor> Processors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Leer la cadena de conexión desde el archivo appsettings.json
            string connectionString = _configuration.GetConnectionString("SqlServer");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_Appointments");

            entity.HasOne(d => d.Process).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Process_Appointments");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Identification).HasMaxLength(15);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Processor>(entity =>
        {
            entity.ToTable("Processor");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
