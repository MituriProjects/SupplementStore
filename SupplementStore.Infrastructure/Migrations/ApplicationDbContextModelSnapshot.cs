﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SupplementStore.Infrastructure;

namespace SupplementStore.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
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
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
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
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SupplementStore.Domain.Addresses.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<bool>("IsHidden");

                    b.Property<string>("Street")
                        .IsRequired();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("SupplementStore.Domain.Baskets.BasketProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<Guid>("Product_Id");

                    b.Property<int>("Quantity");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Product_Id");

                    b.HasIndex("UserId");

                    b.ToTable("BasketProducts");
                });

            modelBuilder.Entity("SupplementStore.Domain.Messages.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("SenderEmail");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("SupplementStore.Domain.Opinions.Opinion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<bool>("IsHidden");

                    b.Property<Guid>("Purchase_Id");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("Purchase_Id")
                        .IsUnique();

                    b.ToTable("Opinions");
                });

            modelBuilder.Entity("SupplementStore.Domain.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<Guid>("Address_Id");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Address_Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SupplementStore.Domain.Orders.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<Guid?>("Opinion_Id");

                    b.Property<Guid>("Order_Id");

                    b.Property<Guid>("Product_Id");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("Opinion_Id")
                        .IsUnique()
                        .HasFilter("[Opinion_Id] IS NOT NULL");

                    b.HasIndex("Order_Id");

                    b.HasIndex("Product_Id");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("SupplementStore.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SupplementStore.Domain.Products.ProductImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<bool>("IsMain");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid>("Product_Id");

                    b.HasKey("Id");

                    b.HasIndex("Product_Id");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("SupplementStore.Domain.Wishes.Wish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<Guid>("Product_Id");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Product_Id");

                    b.HasIndex("UserId");

                    b.ToTable("Wishes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SupplementStore.Domain.Addresses.Address", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("SupplementStore.Domain.Addresses.PostalCode", "PostalCode", b1 =>
                        {
                            b1.Property<Guid?>("AddressId")
                                .ValueGeneratedOnAdd();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnName("PostalCode");

                            b1.ToTable("Addresses");

                            b1.HasOne("SupplementStore.Domain.Addresses.Address")
                                .WithOne("PostalCode")
                                .HasForeignKey("SupplementStore.Domain.Addresses.PostalCode", "AddressId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("SupplementStore.Domain.Baskets.BasketProduct", b =>
                {
                    b.HasOne("SupplementStore.Domain.Products.Product")
                        .WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SupplementStore.Domain.Messages.Message", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SupplementStore.Domain.Opinions.Opinion", b =>
                {
                    b.HasOne("SupplementStore.Domain.Orders.Purchase")
                        .WithOne()
                        .HasForeignKey("SupplementStore.Domain.Opinions.Opinion", "Purchase_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("SupplementStore.Domain.Opinions.Rating", "Rating", b1 =>
                        {
                            b1.Property<Guid?>("OpinionId")
                                .ValueGeneratedOnAdd();

                            b1.Property<int>("Stars");

                            b1.ToTable("Opinions");

                            b1.HasOne("SupplementStore.Domain.Opinions.Opinion")
                                .WithOne("Rating")
                                .HasForeignKey("SupplementStore.Domain.Opinions.Rating", "OpinionId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("SupplementStore.Domain.Orders.Order", b =>
                {
                    b.HasOne("SupplementStore.Domain.Addresses.Address")
                        .WithMany()
                        .HasForeignKey("Address_Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SupplementStore.Domain.Orders.Purchase", b =>
                {
                    b.HasOne("SupplementStore.Domain.Opinions.Opinion")
                        .WithOne()
                        .HasForeignKey("SupplementStore.Domain.Orders.Purchase", "Opinion_Id");

                    b.HasOne("SupplementStore.Domain.Orders.Order")
                        .WithMany()
                        .HasForeignKey("Order_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SupplementStore.Domain.Products.Product")
                        .WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SupplementStore.Domain.Products.ProductImage", b =>
                {
                    b.HasOne("SupplementStore.Domain.Products.Product")
                        .WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SupplementStore.Domain.Wishes.Wish", b =>
                {
                    b.HasOne("SupplementStore.Domain.Products.Product")
                        .WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
