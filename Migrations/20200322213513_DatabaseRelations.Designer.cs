﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SeniorWepApiProject.Data;

namespace SeniorWepApiProject.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200322213513_DatabaseRelations")]
    partial class DatabaseRelations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.LocationModels.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AppUserId")
                        .HasColumnType("text");

                    b.Property<int>("CityId")
                        .HasColumnType("integer");

                    b.Property<int>("DistrictId")
                        .HasColumnType("integer");

                    b.Property<int>("NeighborhoodId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.LocationModels.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.LocationModels.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<int?>("CityId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("CityId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.LocationModels.Neighborhood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<int?>("DistrictId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("DistrictId");

                    b.ToTable("Neighborhoods");
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.UserModels.Ability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Abilities");
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.UserModels.Fancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Fancies");
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.UserModels.Message", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ReadTime")
                        .HasColumnType("text");

                    b.Property<string>("RecieverUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SendTime")
                        .HasColumnType("text");

                    b.Property<string>("SenderUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RecieverUserId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.UserModels.UserAbility", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<int>("AbilityId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "AbilityId");

                    b.HasIndex("AbilityId");

                    b.ToTable("UserAbilities");
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.UserModels.UserFancy", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<int>("FancyId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "FancyId");

                    b.HasIndex("FancyId");

                    b.ToTable("UserFancies");
                });

            modelBuilder.Entity("SeniorWepApiProject.Domain.AppUserModels.AppRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("SeniorWepApiProject.Domain.AppUserModels.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("BirthDate")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("DeletionDate")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("LastLoginDate")
                        .HasColumnType("text");

                    b.Property<string>("LastLogoutDate")
                        .HasColumnType("text");

                    b.Property<string>("LastUpdateDate")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RegistrationDate")
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("UserPhotoUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SeniorWepApiProject.Domain.Swap.Swap", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AcceptedDate")
                        .HasColumnType("text");

                    b.Property<int?>("AddressId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.Property<int>("Rate")
                        .HasColumnType("integer");

                    b.Property<string>("RecieverUserId")
                        .HasColumnType("text");

                    b.Property<string>("SendedDate")
                        .HasColumnType("text");

                    b.Property<string>("SenderUserId")
                        .HasColumnType("text");

                    b.Property<string>("SwapDate")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("RecieverUserId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("Swaps");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("SeniorWepApiProject.Domain.AppUserModels.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SeniorWepApiProject.Domain.AppUserModels.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SeniorWepApiProject.Domain.AppUserModels.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("SeniorWepApiProject.Domain.AppUserModels.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeniorWepApiProject.Domain.AppUserModels.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SeniorWepApiProject.Domain.AppUserModels.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.LocationModels.Address", b =>
                {
                    b.HasOne("SeniorWepApiProject.Domain.AppUserModels.AppUser", "AppUser")
                        .WithMany("Addresses")
                        .HasForeignKey("AppUserId");
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.LocationModels.City", b =>
                {
                    b.HasOne("SeniorWebApiProject.Domain.LocationModels.Address", "Address")
                        .WithOne("City")
                        .HasForeignKey("SeniorWebApiProject.Domain.LocationModels.City", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.LocationModels.District", b =>
                {
                    b.HasOne("SeniorWebApiProject.Domain.LocationModels.Address", "Address")
                        .WithOne("District")
                        .HasForeignKey("SeniorWebApiProject.Domain.LocationModels.District", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeniorWebApiProject.Domain.LocationModels.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId");
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.LocationModels.Neighborhood", b =>
                {
                    b.HasOne("SeniorWebApiProject.Domain.LocationModels.Address", "Address")
                        .WithOne("Neighborhood")
                        .HasForeignKey("SeniorWebApiProject.Domain.LocationModels.Neighborhood", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeniorWebApiProject.Domain.LocationModels.District", "District")
                        .WithMany("Neighborhoods")
                        .HasForeignKey("DistrictId");
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.UserModels.Message", b =>
                {
                    b.HasOne("SeniorWepApiProject.Domain.AppUserModels.AppUser", "RecieverUser")
                        .WithMany("InComingMessages")
                        .HasForeignKey("RecieverUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeniorWepApiProject.Domain.AppUserModels.AppUser", "SenderUser")
                        .WithMany("OutgoingMessages")
                        .HasForeignKey("SenderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.UserModels.UserAbility", b =>
                {
                    b.HasOne("SeniorWebApiProject.Domain.UserModels.Ability", "Ability")
                        .WithMany("UserAbilities")
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeniorWepApiProject.Domain.AppUserModels.AppUser", "User")
                        .WithMany("UserAbilities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SeniorWebApiProject.Domain.UserModels.UserFancy", b =>
                {
                    b.HasOne("SeniorWebApiProject.Domain.UserModels.Fancy", "Fancy")
                        .WithMany("UserFancies")
                        .HasForeignKey("FancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SeniorWepApiProject.Domain.AppUserModels.AppUser", "User")
                        .WithMany("UserFancies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SeniorWepApiProject.Domain.Swap.Swap", b =>
                {
                    b.HasOne("SeniorWebApiProject.Domain.LocationModels.Address", "Address")
                        .WithMany("Swaps")
                        .HasForeignKey("AddressId");

                    b.HasOne("SeniorWepApiProject.Domain.AppUserModels.AppUser", "RecieverUser")
                        .WithMany("InComingSwaps")
                        .HasForeignKey("RecieverUserId");

                    b.HasOne("SeniorWepApiProject.Domain.AppUserModels.AppUser", "SenderUser")
                        .WithMany("OutgoingSwaps")
                        .HasForeignKey("SenderUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
