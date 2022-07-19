﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using project_mvc.Database.Contexts;

#nullable disable

namespace project_mvc.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220718122009_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("project_mvc.Database.Entities.Access.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.Access.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.Location.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("MainAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.Location.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Population")
                        .HasColumnType("int");

                    b.Property<string>("Region")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("City");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.People.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.People.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("FacultyNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MajorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("FacultyId");

                    b.HasIndex("MajorId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.People.StudentsSubjects", b =>
                {
                    b.Property<int>("StudentsId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("SubjectsId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int?>("GradeId")
                        .HasColumnType("int");

                    b.HasKey("StudentsId", "SubjectsId");

                    b.HasIndex("GradeId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("StudentsSubjects");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.People.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.University.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Faculty");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.University.FacultyNum", b =>
                {
                    b.Property<int>("MajorId")
                        .HasColumnType("int");

                    b.Property<int>("NextFreeId")
                        .HasColumnType("int");

                    b.HasKey("MajorId");

                    b.ToTable("FacultyNum");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.University.Major", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Enrolment")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<int>("NameId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("NameId");

                    b.ToTable("Major");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.University.MajorName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("MajorName");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.University.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MajorId")
                        .HasColumnType("int");

                    b.Property<int?>("Year")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MajorId");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.University.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("ScheduleSubject", b =>
                {
                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.HasKey("ScheduleId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("ScheduleSubject");
                });

            modelBuilder.Entity("SubjectTeacher", b =>
                {
                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.Property<int>("TeachersId")
                        .HasColumnType("int");

                    b.HasKey("SubjectsId", "TeachersId");

                    b.HasIndex("TeachersId");

                    b.ToTable("SubjectTeacher");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.Access.Account", b =>
                {
                    b.HasOne("project_mvc.Database.Entities.Access.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_mvc.Database.Entities.People.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.HasOne("project_mvc.Database.Entities.People.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");

                    b.Navigation("Role");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.Location.Address", b =>
                {
                    b.HasOne("project_mvc.Database.Entities.Location.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.People.Student", b =>
                {
                    b.HasOne("project_mvc.Database.Entities.Location.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_mvc.Database.Entities.University.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("project_mvc.Database.Entities.University.Major", "Major")
                        .WithMany()
                        .HasForeignKey("MajorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Faculty");

                    b.Navigation("Major");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.People.StudentsSubjects", b =>
                {
                    b.HasOne("project_mvc.Database.Entities.People.Grade", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeId");

                    b.HasOne("project_mvc.Database.Entities.People.Student", "Student")
                        .WithMany("StudentsSubjects")
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_mvc.Database.Entities.University.Subject", "Subject")
                        .WithMany("StudentsSubjects")
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grade");

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.People.Teacher", b =>
                {
                    b.HasOne("project_mvc.Database.Entities.Location.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_mvc.Database.Entities.University.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.University.Faculty", b =>
                {
                    b.HasOne("project_mvc.Database.Entities.Location.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.University.FacultyNum", b =>
                {
                    b.HasOne("project_mvc.Database.Entities.University.Major", "Major")
                        .WithOne()
                        .HasForeignKey("project_mvc.Database.Entities.University.FacultyNum", "MajorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Major");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.University.Major", b =>
                {
                    b.HasOne("project_mvc.Database.Entities.University.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_mvc.Database.Entities.University.MajorName", "Name")
                        .WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");

                    b.Navigation("Name");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.University.Schedule", b =>
                {
                    b.HasOne("project_mvc.Database.Entities.University.Major", "Major")
                        .WithMany()
                        .HasForeignKey("MajorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Major");
                });

            modelBuilder.Entity("ScheduleSubject", b =>
                {
                    b.HasOne("project_mvc.Database.Entities.University.Schedule", null)
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_mvc.Database.Entities.University.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SubjectTeacher", b =>
                {
                    b.HasOne("project_mvc.Database.Entities.University.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_mvc.Database.Entities.People.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("project_mvc.Database.Entities.People.Student", b =>
                {
                    b.Navigation("StudentsSubjects");
                });

            modelBuilder.Entity("project_mvc.Database.Entities.University.Subject", b =>
                {
                    b.Navigation("StudentsSubjects");
                });
#pragma warning restore 612, 618
        }
    }
}
