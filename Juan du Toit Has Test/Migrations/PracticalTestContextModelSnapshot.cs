﻿// <auto-generated />
using System;
using Juan_du_Toit_Has_Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Juan_du_Toit_Has_Test.Migrations
{
    [DbContext(typeof(PracticalTestContext))]
    partial class PracticalTestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Juan_du_Toit_Has_Test.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmployeeNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Juan_du_Toit_Has_Test.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TitleId")
                        .HasColumnType("int");

                    b.HasKey("PersonID");

                    b.HasIndex("TitleId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Juan_du_Toit_Has_Test.TitleLookup", b =>
                {
                    b.Property<int>("TitleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TitleId");

                    b.ToTable("TitleLookups");
                });

            modelBuilder.Entity("Juan_du_Toit_Has_Test.Employee", b =>
                {
                    b.HasOne("Juan_du_Toit_Has_Test.Person", "Person")
                        .WithOne("Employee")
                        .HasForeignKey("Juan_du_Toit_Has_Test.Employee", "PersonId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();
                });

            modelBuilder.Entity("Juan_du_Toit_Has_Test.Person", b =>
                {
                    b.HasOne("Juan_du_Toit_Has_Test.TitleLookup", "TitleLookup")
                        .WithMany("Persons")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
