﻿// <auto-generated />
using System;
using ClaimApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClaimApplication.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ClaimApplication.Domain.Entities.AppealPredmet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifyBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("AppealPredmets");
                });

            modelBuilder.Entity("ClaimApplication.Domain.Entities.AppealType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifyBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("AppealTypes");
                });

            modelBuilder.Entity("ClaimApplication.Domain.Entities.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal?>("AmountOfFine")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("AppealDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("AppealNumber")
                        .HasColumnType("integer");

                    b.Property<Guid>("AppealPredmetId")
                        .HasColumnType("uuid");

                    b.Property<string>("AppealText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("AppealTypeId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("CalculatedLateCharges")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CertificateGivenDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CertificateNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<decimal?>("MainDebt")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("MembershipAgreementDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MembershipAgreementNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ModifyBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NameOfBussiness")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("Percentage")
                        .HasColumnType("numeric");

                    b.Property<string>("PreviousAppeal")
                        .HasColumnType("text");

                    b.Property<decimal?>("TotalClaimAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("AppealPredmetId");

                    b.HasIndex("AppealTypeId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("ClaimApplication.Domain.Entities.ResponsiblePerson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid>("ApplicationId")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ModifyBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OrdinalNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TypeOfResponsiblePersonId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("TypeOfResponsiblePersonId");

                    b.ToTable("ResponsiblePeople");
                });

            modelBuilder.Entity("ClaimApplication.Domain.Entities.TypeOfResponsiblePerson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifyBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("TypeOfResponsiblePeople");
                });

            modelBuilder.Entity("ClaimApplication.Domain.Entities.Application", b =>
                {
                    b.HasOne("ClaimApplication.Domain.Entities.AppealPredmet", "AppealPredmet")
                        .WithMany("Aplications")
                        .HasForeignKey("AppealPredmetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClaimApplication.Domain.Entities.AppealType", "AppealType")
                        .WithMany("Aplications")
                        .HasForeignKey("AppealTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppealPredmet");

                    b.Navigation("AppealType");
                });

            modelBuilder.Entity("ClaimApplication.Domain.Entities.ResponsiblePerson", b =>
                {
                    b.HasOne("ClaimApplication.Domain.Entities.Application", "Application")
                        .WithMany("ResponsiblePeople")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClaimApplication.Domain.Entities.TypeOfResponsiblePerson", "TypeOfResponsiblePerson")
                        .WithMany("ResponsiblePerson")
                        .HasForeignKey("TypeOfResponsiblePersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");

                    b.Navigation("TypeOfResponsiblePerson");
                });

            modelBuilder.Entity("ClaimApplication.Domain.Entities.AppealPredmet", b =>
                {
                    b.Navigation("Aplications");
                });

            modelBuilder.Entity("ClaimApplication.Domain.Entities.AppealType", b =>
                {
                    b.Navigation("Aplications");
                });

            modelBuilder.Entity("ClaimApplication.Domain.Entities.Application", b =>
                {
                    b.Navigation("ResponsiblePeople");
                });

            modelBuilder.Entity("ClaimApplication.Domain.Entities.TypeOfResponsiblePerson", b =>
                {
                    b.Navigation("ResponsiblePerson");
                });
#pragma warning restore 612, 618
        }
    }
}
