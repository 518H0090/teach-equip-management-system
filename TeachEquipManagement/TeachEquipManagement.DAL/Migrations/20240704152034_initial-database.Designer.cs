﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeachEquipManagement.DAL.EFContext;

#nullable disable

namespace TeachEquipManagement.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240704152034_initial-database")]
    partial class initialdatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.ApprovalRequest", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InventoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ApproveDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(8083));

                    b.Property<bool>("IsApproved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("ManagerApprove")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("RequestDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(7474));

                    b.Property<string>("RequestType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "InventoryId");

                    b.HasIndex("InventoryId");

                    b.ToTable("ApprovalRequests");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.Inventory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AmountBorrow")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("ToolId")
                        .HasColumnType("int");

                    b.Property<int>("TotalQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("ToolId")
                        .IsUnique();

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("InvoiceDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(2836));

                    b.Property<double>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

                    b.Property<int>("ToolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ToolId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.Tool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectRelative")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<string>("ToolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Tool");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid?>("RefreshTokenId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("RefreshTokenId")
                        .IsUnique()
                        .HasFilter("[RefreshTokenId] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.UserDetail", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValue("");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValue("");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValue("");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValue("");

                    b.Property<Guid>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.UserPermission", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("UserPermissions");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.ApprovalRequest", b =>
                {
                    b.HasOne("TeachEquipManagement.DAL.Models.Inventory", "Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachEquipManagement.DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.Inventory", b =>
                {
                    b.HasOne("TeachEquipManagement.DAL.Models.Tool", "Tool")
                        .WithOne("Inventory")
                        .HasForeignKey("TeachEquipManagement.DAL.Models.Inventory", "ToolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tool");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.Invoice", b =>
                {
                    b.HasOne("TeachEquipManagement.DAL.Models.Tool", "Tool")
                        .WithMany("Invoices")
                        .HasForeignKey("ToolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tool");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.Tool", b =>
                {
                    b.HasOne("TeachEquipManagement.DAL.Models.Category", "Category")
                        .WithMany("Tools")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachEquipManagement.DAL.Models.Supplier", "Supplier")
                        .WithMany("Tools")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.User", b =>
                {
                    b.HasOne("TeachEquipManagement.DAL.Models.RefreshToken", "RefreshToken")
                        .WithOne("User")
                        .HasForeignKey("TeachEquipManagement.DAL.Models.User", "RefreshTokenId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("RefreshToken");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.UserDetail", b =>
                {
                    b.HasOne("TeachEquipManagement.DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.UserPermission", b =>
                {
                    b.HasOne("TeachEquipManagement.DAL.Models.Permission", "Permission")
                        .WithMany("UserPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachEquipManagement.DAL.Models.User", "User")
                        .WithMany("UserPermissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.Category", b =>
                {
                    b.Navigation("Tools");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.Permission", b =>
                {
                    b.Navigation("UserPermissions");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.RefreshToken", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.Supplier", b =>
                {
                    b.Navigation("Tools");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.Tool", b =>
                {
                    b.Navigation("Inventory")
                        .IsRequired();

                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("TeachEquipManagement.DAL.Models.User", b =>
                {
                    b.Navigation("UserPermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
