using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DGM_Checkout_dev.Data;
using DGM_Checkout_dev.Models;

namespace DGM_Checkout_dev.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170622161044_UVIDUpdate")]
    partial class UVIDUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DGM_Checkout_dev.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DGM_Checkout_dev.Models.Inventory", b =>
                {
                    b.Property<int>("InventoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("InventoryCost");

                    b.Property<string>("InventoryMake")
                        .HasMaxLength(30);

                    b.Property<string>("InventoryModel")
                        .HasMaxLength(30);

                    b.Property<string>("InventoryName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("InventoryNotes")
                        .HasMaxLength(200);

                    b.Property<string>("InventorySerialNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("LocationID");

                    b.Property<int?>("RentalID");

                    b.Property<int>("StatusID");

                    b.Property<int>("TypeID");

                    b.HasKey("InventoryID");

                    b.HasIndex("LocationID");

                    b.HasIndex("RentalID");

                    b.HasIndex("StatusID");

                    b.HasIndex("TypeID");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("DGM_Checkout_dev.Models.Location", b =>
                {
                    b.Property<int>("LocationID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LocationEntry")
                        .HasMaxLength(10);

                    b.HasKey("LocationID");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("DGM_Checkout_dev.Models.Rental", b =>
                {
                    b.Property<int>("RentalID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("RentalCheckoutDate");

                    b.Property<DateTime>("RentalDueDate");

                    b.Property<bool>("RentalLateFee");

                    b.Property<bool>("RentalLateFeePaid");

                    b.Property<string>("RentalLocation")
                        .HasMaxLength(25);

                    b.Property<string>("RentalName")
                        .HasMaxLength(20);

                    b.Property<string>("RentalNotes")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("RentalReturnDate");

                    b.Property<int>("UserID");

                    b.HasKey("RentalID");

                    b.HasIndex("UserID");

                    b.ToTable("Rental");
                });

            modelBuilder.Entity("DGM_Checkout_dev.Models.Status", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("StatusEntry");

                    b.HasKey("StatusID");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("DGM_Checkout_dev.Models.Type", b =>
                {
                    b.Property<int>("TypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TypeEntry")
                        .HasMaxLength(25);

                    b.HasKey("TypeID");

                    b.ToTable("Type");
                });

            modelBuilder.Entity("DGM_Checkout_dev.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UVID")
                        .IsRequired();

                    b.Property<string>("UserEmail")
                        .IsRequired();

                    b.Property<string>("UserFirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("UserLastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("UserNotes")
                        .HasMaxLength(200);

                    b.Property<string>("UserPhone")
                        .IsRequired();

                    b.HasKey("UserID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DGM_Checkout_dev.Models.Inventory", b =>
                {
                    b.HasOne("DGM_Checkout_dev.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DGM_Checkout_dev.Models.Rental", "Rental")
                        .WithMany("Inventory")
                        .HasForeignKey("RentalID");

                    b.HasOne("DGM_Checkout_dev.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DGM_Checkout_dev.Models.Type", "Type")
                        .WithMany()
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DGM_Checkout_dev.Models.Rental", b =>
                {
                    b.HasOne("DGM_Checkout_dev.Models.User", "User")
                        .WithMany("Rentals")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DGM_Checkout_dev.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DGM_Checkout_dev.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DGM_Checkout_dev.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
