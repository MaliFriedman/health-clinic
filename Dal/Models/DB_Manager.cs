using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Webapi.models;

public partial class DB_Manager : DbContext
{
    public DB_Manager()
    {
    }

    public DB_Manager(DbContextOptions<DB_Manager> options)
        : base(options)
    {
    }

    public virtual DbSet<AvailableAppointment> AvailableAppointments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<NotAvailableAppointment> NotAvailableAppointments { get; set; }

    public virtual DbSet<PassedAppointment> PassedAppointments { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<WorkHour> WorkHours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\"));
            AppDomain.CurrentDomain.SetData("DataDirectory", projectRoot);

            string connectionString = @"Data Source=.\SQLEXPRESS;
                                    AttachDbFilename=|DataDirectory|\Dal\data\Db.mdf;
                                    Integrated Security=True;
                                    Connect Timeout=30;
                                    TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AvailableAppointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Availabl__3214EC07278DDA20");

            entity.ToTable("Available_Appointments");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DoctorId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Doctor_Id");
            entity.Property(e => e.PatientId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Patient_Id");

            entity.HasOne(d => d.Doctor).WithMany(p => p.AvailableAppointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Available_Appointments_ToTable");

            entity.HasOne(d => d.Patient).WithMany(p => p.AvailableAppointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Available_Appointments_ToTable_1");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctors__3214EC074C8D19D1");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last_Name");
            entity.Property(e => e.LengthOfAppointment).HasColumnName("Length_Of_Appointment");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Phone_Number");
            entity.Property(e => e.SalaryPerHour).HasColumnName("Salary_PerHour");
            entity.Property(e => e.Specialization)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<NotAvailableAppointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07CF83D358");

            entity.ToTable("Not_Available_Appointments");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DoctorId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Doctor_Id");
            entity.Property(e => e.PatientId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Patient_Id");

            entity.HasOne(d => d.Doctor).WithMany(p => p.NotAvailableAppointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Not_Available_Appointments_ToTable");

            entity.HasOne(d => d.Patient).WithMany(p => p.NotAvailableAppointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Not_Available_Appointments_ToTable_1");
        });

        modelBuilder.Entity<PassedAppointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC071DC0E5FF");

            entity.ToTable("Passed_Appointments");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DidThePatientArrive).HasColumnName("Did_the_patient_arrive");
            entity.Property(e => e.DoctorId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Doctor_Id");
            entity.Property(e => e.PatientId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Patient_Id");

            entity.HasOne(d => d.Doctor).WithMany(p => p.PassedAppointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Passed_Appointments_ToTable");

            entity.HasOne(d => d.Patient).WithMany(p => p.PassedAppointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Passed_Appointments_ToTable_1");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patients__3214EC07068B25F6");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last_Name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Phone_Number");
        });

        modelBuilder.Entity<WorkHour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC075F9FA7CB");

            entity.ToTable("Work_Hours");

            entity.Property(e => e.DoctorId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Doctor_Id");
            entity.Property(e => e.EndTime).HasColumnName("End_Time");
            entity.Property(e => e.StartTime).HasColumnName("Start_Time");

            entity.HasOne(d => d.Doctor).WithMany(p => p.WorkHours)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Work_Hours_ToTable");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
