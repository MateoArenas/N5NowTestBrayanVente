﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using N5NowTestBrayanVente.Infrastructure.Contexts;

#nullable disable

namespace N5NowTestBrayanVente.Infrastructure.Migrations
{
    [DbContext(typeof(N5NowTestDBContext))]
    [Migration("20231119211034_IniTbls")]
    partial class IniTbls
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models.Permissions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ApellidoEmpleado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaPermiso")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreEmpleado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoPermiso")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoPermiso");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models.PermissionTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PermissionTypes");
                });

            modelBuilder.Entity("N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models.Permissions", b =>
                {
                    b.HasOne("N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models.PermissionTypes", "PermissionType")
                        .WithMany("Permissions")
                        .HasForeignKey("TipoPermiso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PermissionType");
                });

            modelBuilder.Entity("N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models.PermissionTypes", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
