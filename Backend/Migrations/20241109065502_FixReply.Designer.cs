﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using e_commerce_website.Data;

#nullable disable

namespace e_commerce_website.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20241109065502_FixReply")]
    partial class FixReply
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AppRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AppUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("AppUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("078269d8-1a12-4592-b92e-7ff1a876a5f2"),
                            UserId = new Guid("4557893f-1f56-4b6f-bb3b-caefd62c8c49")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens", (string)null);
                });

            modelBuilder.Entity("e_commerce_website.Models.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("078269d8-1a12-4592-b92e-7ff1a876a5f2"),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Administrator role",
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = new Guid("6d9186ba-2cd6-4b6c-b729-4e605de1019f"),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "User role",
                            Name = "User",
                            NormalizedName = "User"
                        });
                });

            modelBuilder.Entity("e_commerce_website.Models.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("birthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("displayname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("gender")
                        .HasColumnType("bit");

                    b.Property<string>("phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("4557893f-1f56-4b6f-bb3b-caefd62c8c49"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "3f8b8923-cb45-438d-9081-f83d4c21138b",
                            Email = "huynhquoctien01062003@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "HUYNHQUOCTIEN01062003@GMAIL.COM",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAIAAYagAAAAENoDzuNTx+FBAel7PiIrxXGMj47Y4ZbbbCc8m/i/4+mihRkw7ppLmSMGO4tzWhD8iw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "admin",
                            address = "29 Street No. 8, Linh Tay Ward, Thu Duc City",
                            birthDay = new DateTime(1998, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            displayname = "Admin",
                            gender = false,
                            status = 0
                        });
                });

            modelBuilder.Entity("e_commerce_website.Models.Category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("generalityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("categories");

                    b.HasData(
                        new
                        {
                            id = 1,
                            generalityName = "Quần áo",
                            name = "Áo sơ mi",
                            status = 0
                        },
                        new
                        {
                            id = 2,
                            generalityName = "Quần áo",
                            name = "Quần tây",
                            status = 0
                        },
                        new
                        {
                            id = 3,
                            generalityName = "Quần áo",
                            name = "Áo thun",
                            status = 0
                        },
                        new
                        {
                            id = 4,
                            generalityName = "Quần áo",
                            name = "Quần kaki",
                            status = 0
                        });
                });

            modelBuilder.Entity("e_commerce_website.Models.Chat", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("receiverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("senderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("receiverId");

                    b.HasIndex("senderId");

                    b.ToTable("chats");
                });

            modelBuilder.Entity("e_commerce_website.Models.Evaluation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("productId");

                    b.HasIndex("userId");

                    b.ToTable("evaluations");
                });

            modelBuilder.Entity("e_commerce_website.Models.Image", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("productId")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("urlImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("productId");

                    b.ToTable("images");
                });

            modelBuilder.Entity("e_commerce_website.Models.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("deliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("feeShip")
                        .HasColumnType("int");

                    b.Property<string>("guess")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("total")
                        .HasColumnType("int");

                    b.Property<Guid?>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("e_commerce_website.Models.OrderDetail", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("orderId")
                        .HasColumnType("int");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<int>("sale")
                        .HasColumnType("int");

                    b.Property<int>("unitPrice")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("orderId");

                    b.HasIndex("productId");

                    b.ToTable("orderDetails");
                });

            modelBuilder.Entity("e_commerce_website.Models.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<int?>("categoryId")
                        .HasColumnType("int");

                    b.Property<int?>("color")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("importPrice")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<int?>("providerId")
                        .HasColumnType("int");

                    b.Property<int?>("rating")
                        .HasColumnType("int");

                    b.Property<int>("sale")
                        .HasColumnType("int");

                    b.Property<int?>("size")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<int>("viewCount")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("categoryId");

                    b.HasIndex("providerId");

                    b.ToTable("products");

                    b.HasData(
                        new
                        {
                            id = 1,
                            amount = 0,
                            categoryId = 1,
                            color = 6,
                            description = "mô tả sản phẩm 1",
                            importPrice = 100000,
                            name = "Áo sơ mi",
                            price = 150000,
                            providerId = 1,
                            rating = 5,
                            sale = 0,
                            size = 2,
                            status = 0,
                            viewCount = 0
                        },
                        new
                        {
                            id = 2,
                            amount = 0,
                            categoryId = 1,
                            color = 2,
                            description = "mô tả sản phẩm 2",
                            importPrice = 80000,
                            name = "Áo sơ mi tay ngắn",
                            price = 120000,
                            providerId = 2,
                            rating = 5,
                            sale = 0,
                            size = 0,
                            status = 0,
                            viewCount = 0
                        },
                        new
                        {
                            id = 3,
                            amount = 0,
                            categoryId = 2,
                            color = 6,
                            description = "mô tả sản phẩm 3",
                            importPrice = 200000,
                            name = "Quần tây",
                            price = 250000,
                            providerId = 3,
                            rating = 5,
                            sale = 0,
                            size = 2,
                            status = 0,
                            viewCount = 0
                        },
                        new
                        {
                            id = 4,
                            amount = 0,
                            categoryId = 3,
                            color = 1,
                            description = "mô tả sản phẩm 4",
                            importPrice = 50000,
                            name = "Áo thun",
                            price = 75000,
                            providerId = 4,
                            rating = 5,
                            sale = 0,
                            size = 2,
                            status = 0,
                            viewCount = 0
                        },
                        new
                        {
                            id = 5,
                            amount = 0,
                            categoryId = 4,
                            color = 7,
                            description = "mô tả sản phẩm 5",
                            importPrice = 180000,
                            name = "Quần kaki",
                            price = 220000,
                            providerId = 5,
                            rating = 5,
                            sale = 0,
                            size = 2,
                            status = 0,
                            viewCount = 0
                        });
                });

            modelBuilder.Entity("e_commerce_website.Models.Provider", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("providers");

                    b.HasData(
                        new
                        {
                            id = 1,
                            name = "Việt Tiến",
                            status = 0
                        },
                        new
                        {
                            id = 2,
                            name = "Cty May Sông Hồng",
                            status = 0
                        },
                        new
                        {
                            id = 3,
                            name = "Cty May Nhà Bè",
                            status = 0
                        },
                        new
                        {
                            id = 4,
                            name = "Cty Giditex",
                            status = 0
                        },
                        new
                        {
                            id = 5,
                            name = "Cty Vinatex",
                            status = 0
                        });
                });

            modelBuilder.Entity("e_commerce_website.Models.Reply", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("evaluationId")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("evaluationId");

                    b.HasIndex("userId");

                    b.ToTable("replies");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("e_commerce_website.Models.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("e_commerce_website.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("e_commerce_website.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("e_commerce_website.Models.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_commerce_website.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("e_commerce_website.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("e_commerce_website.Models.Chat", b =>
                {
                    b.HasOne("e_commerce_website.Models.AppUser", "receiver")
                        .WithMany()
                        .HasForeignKey("receiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_commerce_website.Models.AppUser", "sender")
                        .WithMany()
                        .HasForeignKey("senderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("receiver");

                    b.Navigation("sender");
                });

            modelBuilder.Entity("e_commerce_website.Models.Evaluation", b =>
                {
                    b.HasOne("e_commerce_website.Models.Product", "product")
                        .WithMany("Evaluations")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_commerce_website.Models.AppUser", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");

                    b.Navigation("user");
                });

            modelBuilder.Entity("e_commerce_website.Models.Image", b =>
                {
                    b.HasOne("e_commerce_website.Models.Product", "product")
                        .WithMany("Images")
                        .HasForeignKey("productId");

                    b.Navigation("product");
                });

            modelBuilder.Entity("e_commerce_website.Models.Order", b =>
                {
                    b.HasOne("e_commerce_website.Models.AppUser", "user")
                        .WithMany("Orders")
                        .HasForeignKey("userId");

                    b.Navigation("user");
                });

            modelBuilder.Entity("e_commerce_website.Models.OrderDetail", b =>
                {
                    b.HasOne("e_commerce_website.Models.Order", "order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_commerce_website.Models.Product", "product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("order");

                    b.Navigation("product");
                });

            modelBuilder.Entity("e_commerce_website.Models.Product", b =>
                {
                    b.HasOne("e_commerce_website.Models.Category", "category")
                        .WithMany("Products")
                        .HasForeignKey("categoryId");

                    b.HasOne("e_commerce_website.Models.Provider", "provider")
                        .WithMany()
                        .HasForeignKey("providerId");

                    b.Navigation("category");

                    b.Navigation("provider");
                });

            modelBuilder.Entity("e_commerce_website.Models.Reply", b =>
                {
                    b.HasOne("e_commerce_website.Models.Evaluation", "evaluation")
                        .WithMany("Replies")
                        .HasForeignKey("evaluationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("e_commerce_website.Models.AppUser", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("evaluation");

                    b.Navigation("user");
                });

            modelBuilder.Entity("e_commerce_website.Models.AppUser", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("e_commerce_website.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("e_commerce_website.Models.Evaluation", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("e_commerce_website.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("e_commerce_website.Models.Product", b =>
                {
                    b.Navigation("Evaluations");

                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
