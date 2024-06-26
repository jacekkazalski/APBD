﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using kol2.Entities;

namespace kol2.Migrations
{
    [DbContext(typeof(FireStationContext))]
    [Migration("20220530181328_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("kol2.Entities.Action", b =>
                {
                    b.Property<int>("IdAction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("NeedSpecialEquipment")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("IdAction")
                        .HasName("Action_pk");

                    b.ToTable("Action");
                });

            modelBuilder.Entity("kol2.Entities.FireTruck", b =>
                {
                    b.Property<int>("IdFiretruck")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OperationNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("SpecialEquipment")
                        .HasColumnType("bit");

                    b.HasKey("IdFiretruck")
                        .HasName("FireTruck_pk");

                    b.ToTable("FireTruck");
                });

            modelBuilder.Entity("kol2.Entities.FireTruckAction", b =>
                {
                    b.Property<int>("IdFiretruck")
                        .HasColumnType("int");

                    b.Property<int>("IdAction")
                        .HasColumnType("int");

                    b.Property<DateTime>("AssignmentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IdFiretruck", "IdAction")
                        .HasName("FireTruckAction_pk");

                    b.HasIndex("IdAction");

                    b.ToTable("FireTruck_Action");
                });

            modelBuilder.Entity("kol2.Entities.FireTruckAction", b =>
                {
                    b.HasOne("kol2.Entities.Action", "IdActionNavigation")
                        .WithMany("FireTruckActions")
                        .HasForeignKey("IdAction")
                        .HasConstraintName("FireTruckAction_Action")
                        .IsRequired();

                    b.HasOne("kol2.Entities.FireTruck", "IdFireTruckNavigation")
                        .WithMany("FireTruckActions")
                        .HasForeignKey("IdFiretruck")
                        .HasConstraintName("FireTruckAction_FireTruck")
                        .IsRequired();

                    b.Navigation("IdActionNavigation");

                    b.Navigation("IdFireTruckNavigation");
                });

            modelBuilder.Entity("kol2.Entities.Action", b =>
                {
                    b.Navigation("FireTruckActions");
                });

            modelBuilder.Entity("kol2.Entities.FireTruck", b =>
                {
                    b.Navigation("FireTruckActions");
                });
#pragma warning restore 612, 618
        }
    }
}
