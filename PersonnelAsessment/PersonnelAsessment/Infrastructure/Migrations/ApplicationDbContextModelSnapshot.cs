﻿// <auto-generated />
using System;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Admins", "private");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin1@ymail.com",
                            Password = "wVHAesBpD3fGGREvMkCqyyHABbgjeU22v2OdcQ8M+Jo=",
                            Salt = new byte[] { 14, 219, 106, 150, 6, 252, 1, 234, 149, 170, 167, 133, 176, 226, 192, 211 }
                        });
                });

            modelBuilder.Entity("Domain.Entities.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassroomTeacherId")
                        .HasColumnType("integer");

                    b.Property<char>("Litera")
                        .HasColumnType("character(1)");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<int>("OrganisationId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomTeacherId");

                    b.HasIndex("OrganisationId");

                    b.ToTable("Forms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClassroomTeacherId = 3,
                            Litera = ' ',
                            Number = 1,
                            OrganisationId = 1
                        });
                });

            modelBuilder.Entity("Domain.Entities.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("AssigmentId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkerId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AssigmentId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Grades");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AssigmentId = 1,
                            WorkerId = 2,
                            Value = "5"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Assigment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("FormId")
                        .HasColumnType("integer");

                    b.Property<string>("Topic")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("Assigment");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateOnly(2024, 4, 10),
                            FormId = 1,
                            Topic = "Устройство сердца"
                        });
                });

            modelBuilder.Entity("Domain.Entities.JobList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Day")
                        .HasColumnType("text");

                    b.Property<int>("FormId")
                        .HasColumnType("integer");

                    b.Property<int>("IndexNum")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("JobList");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Day = "Monday",
                            FormId = 1,
                            IndexNum = 1,
                        });
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.UserImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageName")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserImages");
                });

            modelBuilder.Entity("OrganisationLead", b =>
                {
                    b.Property<int>("OrganisationsId")
                        .HasColumnType("integer");

                    b.Property<int>("LeadId")
                        .HasColumnType("integer");

                    b.HasKey("OrganisationsId", "LeadId");

                    b.HasIndex("LeadsId");

                    b.ToTable("Workers", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Organisation", b =>
                {
                    b.HasBaseType("Domain.Entities.User");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("INN")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.ToTable("Organisations", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "andrej.04@mail.ru",
                            IsActivated = true,
                            Password = "eKPL44bLuWH5mJJbVH2vbTcXdwDjm8Fb4CIHB0NZ8S0=",
                            Role = "organisation",
                            Salt = new byte[] { 58, 94, 103, 112, 230, 149, 61, 51, 48, 95, 116, 77, 14, 106, 8, 70 }
                        });
                });

            modelBuilder.Entity("Domain.Entities.Worker", b =>
                {
                    b.HasBaseType("Domain.Entities.User");

                    b.Property<string>("FIO")
                        .HasColumnType("text");

                    b.Property<int>("FormId")
                        .HasColumnType("integer");

                    b.HasIndex("FormId");

                    b.ToTable("Workers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Email = "andrej@example.ru",
                            IsActivated = true,
                            Password = "ABvKZwAsbcFGPDmhT1elWoFPIQUPp+MZO9Vt1eiukkI=",
                            Role = "student",
                            Salt = new byte[] { 53, 104, 17, 236, 74, 82, 90, 207, 169, 201, 221, 58, 24, 250, 69, 58 },
                            FIO = "Зубенко Михаил Петрович",
                            FormId = 1
                        });
                });

            modelBuilder.Entity("Domain.Entities.Lead", b =>
                {
                    b.HasBaseType("Domain.Entities.User");

                    b.Property<string>("Education")
                        .HasColumnType("text");

                    b.Property<string>("FIO")
                        .HasColumnType("text");

                    b.ToTable("Leads", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Email = "niyaz_teacher@example.ru",
                            IsActivated = true,
                            Password = "mMTQiS0BjDcIwRpa7Z9QxBheAz/KOKG832xMCYqdyt4=",
                            Role = "teacher",
                            Salt = new byte[] { 174, 156, 101, 39, 72, 119, 148, 98, 42, 83, 2, 59, 168, 117, 76, 155 },
                            FIO = "Александр Македонский"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Form", b =>
                {
                    b.HasOne("Domain.Entities.Teacher", "ClassroomTeacher")
                        .WithMany("Forms")
                        .HasForeignKey("ClassroomTeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Organisation", "Organisation")
                        .WithMany("Forms")
                        .HasForeignKey("OrganisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassroomTeacher");

                    b.Navigation("Organisation");
                });

            modelBuilder.Entity("Domain.Entities.Grade", b =>
                {
                    b.HasOne("Domain.Entities.Assigment", "Assigment")
                        .WithMany("Grades")
                        .HasForeignKey("AssigmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Worker", "Worker")
                        .WithMany("Grades")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assigment");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Domain.Entities.Assigment", b =>
                {
                    b.HasOne("Domain.Entities.Form", "Form")
                        .WithMany("Assigmnets")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Form");
                });

            modelBuilder.Entity("Domain.Entities.JobList", b =>
                {
                    b.HasOne("Domain.Entities.Form", "Form")
                        .WithMany("JobLists")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Form");
                });

            modelBuilder.Entity("Domain.Entities.UserImage", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithOne("Image")
                        .HasForeignKey("Domain.Entities.UserImage", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrganisationLead", b =>
                {
                    b.HasOne("Domain.Entities.Organisation", null)
                        .WithMany()
                        .HasForeignKey("OrganisationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Lead", null)
                        .WithMany()
                        .HasForeignKey("LeadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Organisation", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Organisation", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Worker", b =>
                {
                    b.HasOne("Domain.Entities.Form", "Form")
                        .WithMany("Workers")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Worker", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Form");
                });

            modelBuilder.Entity("Domain.Entities.Lead", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Lead", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Form", b =>
                {
                    b.Navigation("Assigments");

                    b.Navigation("JobLists");

                    b.Navigation("Workers");
                });

            modelBuilder.Entity("Domain.Entities.Assigment", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Image");
                });

            modelBuilder.Entity("Domain.Entities.Organisation", b =>
                {
                    b.Navigation("Forms");

                });

            modelBuilder.Entity("Domain.Entities.Workers", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("Domain.Entities.Lead", b =>
                {
                    b.Navigation("Forms");

                });
#pragma warning restore 612, 618
        }
    }
}
