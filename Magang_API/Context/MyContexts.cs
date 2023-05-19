using System;
using System.Collections.Generic;
using Magang_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Magang_API.Context;

public partial class MyContexts : DbContext
{
    public MyContexts()
    {
    }

    public MyContexts(DbContextOptions<MyContexts> options)
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



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__account__46A222CDF0F68CC0");

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
                .HasConstraintName("FK__account__account__44FF419A");
        });

        modelBuilder.Entity<AccountRole>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__account___46A222CDA9A2F46F");

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
                .HasConstraintName("FK__account_r__accou__47DBAE45");

            entity.HasOne(d => d.Role).WithMany(p => p.AccountRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__account_r__role___48CFD27E");
        });

        modelBuilder.Entity<AccountStudent>(entity =>
        {
            entity.HasKey(e => e.AccountStudentId).HasName("PK__account___BEA2C70A15D602FE");

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
                .HasConstraintName("FK__account_s__accou__5629CD9C");
        });

        modelBuilder.Entity<AccountStudentRole>(entity =>
        {
            entity.HasKey(e => e.AccountStudentId).HasName("PK__account___BEA2C70A51784B9B");

            entity.ToTable("account_student_role");

            entity.Property(e => e.AccountStudentId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("account_student_id");
            entity.Property(e => e.RoleStudentId).HasColumnName("role_student_id");

            entity.HasOne(d => d.AccountStudent).WithOne(p => p.AccountStudentRole)
                .HasForeignKey<AccountStudentRole>(d => d.AccountStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__account_s__accou__59063A47");

            entity.HasOne(d => d.RoleStudent).WithMany(p => p.AccountStudentRoles)
                .HasForeignKey(d => d.RoleStudentId)
                .HasConstraintName("FK__account_s__role___59FA5E80");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__departme__3213E83F63602BC1");

            entity.ToTable("department");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__educatio__3213E83F35850944");

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
                .HasConstraintName("FK__education__unive__2E1BDC42");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Nik).HasName("PK__employee__DF97D0EC640BC48D");

            entity.ToTable("employee");

            entity.HasIndex(e => new { e.Email, e.PhoneNumber }, "UQ__employee__117757C2379B0846").IsUnique();

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
                .HasConstraintName("FK__employee__depart__31EC6D26");
        });

        modelBuilder.Entity<Profiling>(entity =>
        {
            entity.HasKey(e => e.ProfilingId).HasName("PK__profilin__BBAD950F9B434C22");

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
                .HasConstraintName("FK__profiling__educa__37A5467C");

            entity.HasOne(d => d.ProfilingNavigation).WithOne(p => p.Profiling)
                .HasForeignKey<Profiling>(d => d.ProfilingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__profiling__profi__38996AB5");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__role__3213E83F4B88BF44");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__status__2A33069A68CE13BF");

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
                .HasConstraintName("FK__status__departme__412EB0B6");

            entity.HasOne(d => d.Mentor).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("FK__status__mentor_i__4222D4EF");

            entity.HasOne(d => d.Student).WithOne(p => p.Status)
                .HasForeignKey<Status>(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__status__student___403A8C7D");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Nim).HasName("PK__student__DF97D0EA722A4F32");

            entity.ToTable("student");

            entity.HasIndex(e => new { e.Email, e.PhoneNumber }, "UQ__student__117757C2D843BA92").IsUnique();

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

            entity.HasOne(d => d.Universitas).WithMany(p => p.Students)
                .HasForeignKey(d => d.UniversitasId)
                .HasConstraintName("FK__student__univers__3C69FB99");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__universi__3213E83F055B60B2");

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
