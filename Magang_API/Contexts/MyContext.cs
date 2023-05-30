using System;
using System.Collections.Generic;
using Magang_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Magang_API.Contexts;

public partial class MyContext : DbContext
{
    public MyContext()
    {
    }

    public MyContext(DbContextOptions<MyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountRole> AccountRoles { get; set; }

    public virtual DbSet<AccountStudent> AccountStudents { get; set; }

    public virtual DbSet<AccountStudentRole> AccountStudentRoles { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Education> Educations { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Profiling> Profilings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__account__46A222CDE920F637");

            entity.ToTable("account");

            entity.Property(e => e.AccountId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("account_id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");

            entity.HasOne(d => d.AccountNavigation).WithOne(p => p.Account)
                .HasForeignKey<Account>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__account__account__3D5E1FD2");
        });

        modelBuilder.Entity<AccountRole>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__account___46A222CDE87E2DD3");

            entity.ToTable("account_role");

            entity.Property(e => e.AccountId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("account_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Account).WithOne(p => p.AccountRole)
                .HasForeignKey<AccountRole>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__account_r__accou__403A8C7D");

            entity.HasOne(d => d.Role).WithMany(p => p.AccountRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__account_r__role___412EB0B6");
        });

        modelBuilder.Entity<AccountStudent>(entity =>
        {
            entity.HasKey(e => e.AccountStudentId).HasName("PK__account___BEA2C70AB9BFF84C");

            entity.ToTable("account_student");

            entity.Property(e => e.AccountStudentId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("account_student_id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");

            entity.HasOne(d => d.AccountStudentNavigation).WithOne(p => p.AccountStudent)
                .HasForeignKey<AccountStudent>(d => d.AccountStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__account_s__accou__440B1D61");
        });

        modelBuilder.Entity<AccountStudentRole>(entity =>
        {
            entity.HasKey(e => e.AccountStudentId).HasName("PK__account___BEA2C70A8DFBDC0E");

            entity.ToTable("account_student_role");

            entity.Property(e => e.AccountStudentId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("account_student_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.AccountStudent).WithOne(p => p.AccountStudentRole)
                .HasForeignKey<AccountStudentRole>(d => d.AccountStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__account_s__accou__46E78A0C");

            entity.HasOne(d => d.Role).WithMany(p => p.AccountStudentRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__account_s__role___47DBAE45");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__departme__3213E83F7BBCFA0B");

            entity.ToTable("department");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__educatio__3213E83F96BEBC57");

            entity.ToTable("education");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Degree)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("degree");
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("gpa");
            entity.Property(e => e.Major)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("major");
            entity.Property(e => e.UniversityId).HasColumnName("university_id");

            entity.HasOne(d => d.University).WithMany(p => p.Educations)
                .HasForeignKey(d => d.UniversityId)
                .HasConstraintName("FK__education__unive__2A4B4B5E");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Nik).HasName("PK__employee__DF97D0EC5F5F6BEB");

            entity.ToTable("employee");

            entity.HasIndex(e => new { e.Email, e.PhoneNumber }, "UQ__employee__117757C2F0DFE9C9").IsUnique();

            entity.Property(e => e.Nik)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nik");
            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("birth_date");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.HiringDate)
                .HasColumnType("datetime")
                .HasColumnName("hiring_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__employee__depart__2E1BDC42");
        });

        modelBuilder.Entity<Profiling>(entity =>
        {
            entity.HasKey(e => e.ProfilingId).HasName("PK__profilin__BBAD950F562DB922");

            entity.ToTable("profiling");

            entity.Property(e => e.ProfilingId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("profiling_id");
            entity.Property(e => e.EducationId).HasColumnName("education_id");

            entity.HasOne(d => d.Education).WithMany(p => p.Profilings)
                .HasForeignKey(d => d.EducationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__profiling__educa__34C8D9D1");

            entity.HasOne(d => d.ProfilingNavigation).WithOne(p => p.Profiling)
                .HasForeignKey<Profiling>(d => d.ProfilingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__profiling__profi__35BCFE0A");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role__3213E83F97D1B256");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__status__2A33069AA17F2418");

            entity.ToTable("status");

            entity.Property(e => e.StudentId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("student_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.MentorId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("mentor_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status1).HasColumnName("status");

            entity.HasOne(d => d.Department).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__status__departme__398D8EEE");

            entity.HasOne(d => d.Mentor).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("FK__status__mentor_i__3A81B327");

            entity.HasOne(d => d.Student).WithOne(p => p.Status)
                .HasForeignKey<Status>(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__status__student___38996AB5");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Nim).HasName("PK__student__DF97D0EAF77D7C9C");

            entity.ToTable("student");

            entity.HasIndex(e => new { e.Email, e.PhoneNumber }, "UQ__student__117757C2E3A8D73E").IsUnique();

            entity.Property(e => e.Nim)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nim");
            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("birth_date");
            entity.Property(e => e.Degree)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("degree");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("gpa");
            entity.Property(e => e.IsApproval).HasColumnName("is_approval");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Major)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("major");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.UniversitasId).HasColumnName("universitas_id");
            entity.Property(e => e.Document)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("document");

            entity.HasOne(d => d.Universitas).WithMany(p => p.Students)
                .HasForeignKey(d => d.UniversitasId)
                .HasConstraintName("FK__student__univers__31EC6D26");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__universi__3213E83F3B949942");

            entity.ToTable("university");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
