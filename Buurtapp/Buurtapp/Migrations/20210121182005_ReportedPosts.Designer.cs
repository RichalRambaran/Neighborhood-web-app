﻿// <auto-generated />
using System;
using Buurtapp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Buurtapp.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20210121182005_ReportedPosts")]
    partial class ReportedPosts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Buurtapp.Areas.Identity.Data.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("UserId");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CreateDate")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("CreateDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("FirstName");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int")
                        .HasColumnName("HouseNumber");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("LastName");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("PostalCode");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("PostalCode", "HouseNumber");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Buurtapp.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryId");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("Name");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Test"
                        });
                });

            modelBuilder.Entity("Buurtapp.Models.Chat", b =>
                {
                    b.Property<int>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ChatId");

                    b.Property<string>("CreateDate")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("CreateDate");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("Title");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("Owner");

                    b.HasKey("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("Buurtapp.Models.ChatParticipant", b =>
                {
                    b.Property<int>("ChatId")
                        .HasColumnType("int")
                        .HasColumnName("Chat");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("Participant");

                    b.HasKey("ChatId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatParticipant");
                });

            modelBuilder.Entity("Buurtapp.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CommentId");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("Content");

                    b.Property<string>("PlaceDate")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("PlaceDate");

                    b.Property<int>("PostId")
                        .HasColumnType("int")
                        .HasColumnName("Post");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("Commenter");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Buurtapp.Models.Location", b =>
                {
                    b.Property<string>("PostalCode")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("PostalCode");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int")
                        .HasColumnName("HouseNumber");

                    b.Property<string>("Neighborhood")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("Neighbourhood");

                    b.HasKey("PostalCode", "HouseNumber");

                    b.ToTable("Location");

                    b.HasData(
                        new
                        {
                            PostalCode = "2526LX",
                            HouseNumber = 917,
                            Neighborhood = "Schilderswijk"
                        });
                });

            modelBuilder.Entity("Buurtapp.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MessageId");

                    b.Property<int>("ChatId")
                        .HasColumnType("int")
                        .HasColumnName("Chat");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("Content");

                    b.Property<string>("SendDate")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("SendDate");

                    b.Property<string>("SendTime")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("SendTime");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("Sender");

                    b.HasKey("MessageId");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Buurtapp.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PostId");

                    b.Property<bool>("Anonymous")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("Anonymous");

                    b.Property<bool>("Archived")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("Archived");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("Category");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("Content");

                    b.Property<string>("Image")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("Image");

                    b.Property<string>("PlaceDate")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("PlaceDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasColumnName("Title");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("Poster");

                    b.Property<int>("Views")
                        .HasColumnType("int")
                        .HasColumnName("Views");

                    b.HasKey("PostId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("Buurtapp.Models.Report", b =>
                {
                    b.Property<int>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ReportId");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("Description");

                    b.Property<int>("PostId")
                        .HasColumnType("int")
                        .HasColumnName("Post");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("Reporter");

                    b.HasKey("ReportId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Buurtapp.Models.Solution", b =>
                {
                    b.Property<int>("SolutionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SolutionId");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("Description");

                    b.Property<string>("PlaceDate")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("PlaceDate");

                    b.Property<int>("PostId")
                        .HasColumnType("int")
                        .HasColumnName("Post");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("Title");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("Proposer");

                    b.Property<int>("Votes")
                        .HasColumnType("int")
                        .HasColumnName("Votes");

                    b.HasKey("SolutionId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Solution");
                });

            modelBuilder.Entity("Buurtapp.Models.UserLikesPost", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("User");

                    b.Property<int>("PostId")
                        .HasColumnType("int")
                        .HasColumnName("Post");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("UserLikesPost");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("RoleId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("Buurtapp.Areas.Identity.Data.AppUser", b =>
                {
                    b.HasOne("Buurtapp.Models.Location", "Location")
                        .WithMany("AppUsers")
                        .HasForeignKey("PostalCode", "HouseNumber");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Buurtapp.Models.Chat", b =>
                {
                    b.HasOne("Buurtapp.Areas.Identity.Data.AppUser", "AppUser")
                        .WithMany("Chats")
                        .HasForeignKey("UserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Buurtapp.Models.ChatParticipant", b =>
                {
                    b.HasOne("Buurtapp.Models.Chat", "Chat")
                        .WithMany("ChatParticipants")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Buurtapp.Areas.Identity.Data.AppUser", "AppUser")
                        .WithMany("ChatParticipants")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("Buurtapp.Models.Comment", b =>
                {
                    b.HasOne("Buurtapp.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Buurtapp.Areas.Identity.Data.AppUser", "AppUser")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");

                    b.Navigation("AppUser");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Buurtapp.Models.Message", b =>
                {
                    b.HasOne("Buurtapp.Models.Chat", "Chat")
                        .WithMany()
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Buurtapp.Areas.Identity.Data.AppUser", "AppUser")
                        .WithMany("Messages")
                        .HasForeignKey("UserId");

                    b.Navigation("AppUser");

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("Buurtapp.Models.Post", b =>
                {
                    b.HasOne("Buurtapp.Models.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Buurtapp.Areas.Identity.Data.AppUser", "AppUser")
                        .WithMany("Posts")
                        .HasForeignKey("UserId");

                    b.Navigation("AppUser");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Buurtapp.Models.Report", b =>
                {
                    b.HasOne("Buurtapp.Models.Post", "Post")
                        .WithMany("Reports")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Buurtapp.Areas.Identity.Data.AppUser", "AppUser")
                        .WithMany("Reports")
                        .HasForeignKey("UserId");

                    b.Navigation("AppUser");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Buurtapp.Models.Solution", b =>
                {
                    b.HasOne("Buurtapp.Models.Post", "Post")
                        .WithMany("Solutions")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Buurtapp.Areas.Identity.Data.AppUser", "AppUser")
                        .WithMany("Solutions")
                        .HasForeignKey("UserId");

                    b.Navigation("AppUser");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Buurtapp.Models.UserLikesPost", b =>
                {
                    b.HasOne("Buurtapp.Models.Post", "Post")
                        .WithMany("UserLikesPosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Buurtapp.Areas.Identity.Data.AppUser", "AppUser")
                        .WithMany("UserLikesPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Buurtapp.Areas.Identity.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Buurtapp.Areas.Identity.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Buurtapp.Areas.Identity.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Buurtapp.Areas.Identity.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Buurtapp.Areas.Identity.Data.AppUser", b =>
                {
                    b.Navigation("ChatParticipants");

                    b.Navigation("Chats");

                    b.Navigation("Comments");

                    b.Navigation("Messages");

                    b.Navigation("Posts");

                    b.Navigation("Reports");

                    b.Navigation("Solutions");

                    b.Navigation("UserLikesPosts");
                });

            modelBuilder.Entity("Buurtapp.Models.Category", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Buurtapp.Models.Chat", b =>
                {
                    b.Navigation("ChatParticipants");
                });

            modelBuilder.Entity("Buurtapp.Models.Location", b =>
                {
                    b.Navigation("AppUsers");
                });

            modelBuilder.Entity("Buurtapp.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Reports");

                    b.Navigation("Solutions");

                    b.Navigation("UserLikesPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
