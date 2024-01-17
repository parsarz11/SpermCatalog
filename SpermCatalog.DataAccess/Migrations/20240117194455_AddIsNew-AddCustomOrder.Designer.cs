﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpermCatalog.DataAccess.DatabaseContext;

#nullable disable

namespace SpermCatalog.DataAccess.Migrations
{
    [DbContext(typeof(SpermCatalogDbContext))]
    [Migration("20240117194455_AddIsNew-AddCustomOrder")]
    partial class AddIsNewAddCustomOrder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SpermCatalog.DataAccess.Entities.BeefSperm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BREED")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CONF")
                        .HasColumnType("float");

                    b.Property<double>("COUL")
                        .HasColumnType("float");

                    b.Property<double>("CR")
                        .HasColumnType("float");

                    b.Property<int>("CustomOrder")
                        .HasColumnType("int");

                    b.Property<double>("DM")
                        .HasColumnType("float");

                    b.Property<double>("GRAS")
                        .HasColumnType("float");

                    b.Property<double>("IAB")
                        .HasColumnType("float");

                    b.Property<double>("ICRC")
                        .HasColumnType("float");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<string>("MGS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NAME")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PCAR")
                        .HasColumnType("float");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<double>("RDT")
                        .HasColumnType("float");

                    b.Property<string>("RegNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SCE")
                        .HasColumnType("float");

                    b.Property<string>("SIRE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BeefSperms");
                });

            modelBuilder.Entity("SpermCatalog.DataAccess.Entities.DairySperm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomOrder")
                        .HasColumnType("int");

                    b.Property<double>("DPR")
                        .HasColumnType("float");

                    b.Property<double>("FAT")
                        .HasColumnType("float");

                    b.Property<double>("FI")
                        .HasColumnType("float");

                    b.Property<double>("FLC")
                        .HasColumnType("float");

                    b.Property<double>("FM")
                        .HasColumnType("float");

                    b.Property<double>("FS")
                        .HasColumnType("float");

                    b.Property<double>("ICC")
                        .HasColumnType("float");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<double>("LNM")
                        .HasColumnType("float");

                    b.Property<string>("MGS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MILK")
                        .HasColumnType("float");

                    b.Property<string>("NAAB_CODE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NAME")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PL")
                        .HasColumnType("float");

                    b.Property<double>("PRO")
                        .HasColumnType("float");

                    b.Property<double>("PTAT")
                        .HasColumnType("float");

                    b.Property<string>("RegNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SCE")
                        .HasColumnType("float");

                    b.Property<string>("SIRE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TPI")
                        .HasColumnType("float");

                    b.Property<double>("UDC")
                        .HasColumnType("float");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DairySperms");
                });
#pragma warning restore 612, 618
        }
    }
}
