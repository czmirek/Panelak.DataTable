﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Panelak.DataTable.Mvc;

namespace Panelak.DataTable.Migrations
{
    [DbContext(typeof(DataTableSqliteContext))]
    partial class DataTableSqliteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Panelak.DataTable.DataTableConfigEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("DefaultTabId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ViewIdentifier")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("dtconfig");
                });

            modelBuilder.Entity("Panelak.DataTable.DataTableTabEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Caption")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ConfigId")
                        .HasColumnType("TEXT");

                    b.Property<int>("RowsPerPage")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ConfigId");

                    b.ToTable("dttab");
                });

            modelBuilder.Entity("Panelak.DataTable.DataTableTabEntity", b =>
                {
                    b.HasOne("Panelak.DataTable.DataTableConfigEntity", "Config")
                        .WithMany()
                        .HasForeignKey("ConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Config");
                });
#pragma warning restore 612, 618
        }
    }
}
