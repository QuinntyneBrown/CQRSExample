﻿// <auto-generated />
using System;
using CQRSExample.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CQRSExample.API.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20180228060123_PageSize")]
    partial class PageSize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview1-28290")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CQRSExample.Core.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Firstname");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Lastname");

                    b.Property<Guid?>("TenantId");

                    b.HasKey("CustomerId");

                    b.HasIndex("TenantId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.Dashboard", b =>
                {
                    b.Property<int>("DashboardId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<Guid?>("TenantId");

                    b.HasKey("DashboardId");

                    b.HasIndex("TenantId");

                    b.ToTable("Dashboards");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.DashboardTile", b =>
                {
                    b.Property<int>("DashboardTileId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DashboardId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<Guid?>("TenantId");

                    b.HasKey("DashboardTileId");

                    b.HasIndex("DashboardId");

                    b.HasIndex("TenantId");

                    b.ToTable("DashboardTiles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("DashboardTile");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.DashboardTileSettings", b =>
                {
                    b.Property<int>("DashboardTileSettingsId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DashboardTileId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("Height");

                    b.Property<int>("Left");

                    b.Property<int>("Top");

                    b.Property<int>("Width");

                    b.HasKey("DashboardTileSettingsId");

                    b.HasIndex("DashboardTileId")
                        .IsUnique();

                    b.ToTable("DashboardTileSettings");

                    b.HasDiscriminator<string>("Discriminator").HasValue("DashboardTileSettings");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<Guid?>("TenantId");

                    b.HasKey("RoleId");

                    b.HasIndex("TenantId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.Tenant", b =>
                {
                    b.Property<Guid>("TenantId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("TenantId");

                    b.ToTable("Tenant");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.Tile", b =>
                {
                    b.Property<int>("TileId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<Guid?>("TenantId");

                    b.HasKey("TileId");

                    b.HasIndex("TenantId");

                    b.ToTable("Tiles");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Password");

                    b.Property<Guid?>("TenantId");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.HasIndex("TenantId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.CustomerDashboardTile", b =>
                {
                    b.HasBaseType("CQRSExample.Core.Entities.DashboardTile");


                    b.ToTable("CustomerDashboardTile");

                    b.HasDiscriminator().HasValue("CustomerDashboardTile");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.CustomerDashboardTileSettings", b =>
                {
                    b.HasBaseType("CQRSExample.Core.Entities.DashboardTileSettings");

                    b.Property<int>("PageSize");

                    b.ToTable("CustomerDashboardTileSettings");

                    b.HasDiscriminator().HasValue("CustomerDashboardTileSettings");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.Customer", b =>
                {
                    b.HasOne("CQRSExample.Core.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.Dashboard", b =>
                {
                    b.HasOne("CQRSExample.Core.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.DashboardTile", b =>
                {
                    b.HasOne("CQRSExample.Core.Entities.Dashboard")
                        .WithMany("DashboardTiles")
                        .HasForeignKey("DashboardId");

                    b.HasOne("CQRSExample.Core.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.DashboardTileSettings", b =>
                {
                    b.HasOne("CQRSExample.Core.Entities.DashboardTile", "DashboardTile")
                        .WithOne("Settings")
                        .HasForeignKey("CQRSExample.Core.Entities.DashboardTileSettings", "DashboardTileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.Role", b =>
                {
                    b.HasOne("CQRSExample.Core.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.Tile", b =>
                {
                    b.HasOne("CQRSExample.Core.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("CQRSExample.Core.Entities.User", b =>
                {
                    b.HasOne("CQRSExample.Core.Entities.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });
#pragma warning restore 612, 618
        }
    }
}