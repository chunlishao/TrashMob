﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrashMob.Persistence;

namespace TrashMob.Migrations
{
    [DbContext(typeof(MobDbContext))]
    partial class MobDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
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

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
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

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TrashMob.Models.AttendeeNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("AcknowledgedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("NotificationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("NotificationTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("NotificationTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("AttendeeNotification");
                });

            modelBuilder.Entity("TrashMob.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ActualNumberOfParticipants")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("CreatedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<DateTimeOffset>("EventDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("EventStatusId")
                        .HasColumnType("int");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Gpscoords")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("GPSCoords");

                    b.Property<Guid>("LastUpdatedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("LastUpdatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<int?>("MaxNumberOfParticipants")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("StateProvince")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("EventStatusId");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("LastUpdatedByUserId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TrashMob.Models.EventAttendee", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("ArrivalTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("CanceledDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DepartureTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("SignUpDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("UserId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EventAttendees");
                });

            modelBuilder.Entity("TrashMob.Models.EventHistory", b =>
                {
                    b.Property<int?>("ActualNumberOfParticipants")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("CreatedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<DateTimeOffset>("EventDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("EventStatusId")
                        .HasColumnType("int");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Gpscoords")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("GPSCoords");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LastUpdatedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("LastUpdatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<int?>("MaxNumberOfParticipants")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("StateProvince")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.ToTable("EventHistory");
                });

            modelBuilder.Entity("TrashMob.Models.EventStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("EventStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Event is actively recruiting new members",
                            DisplayOrder = 1,
                            Name = "Active"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Event is full",
                            DisplayOrder = 2,
                            Name = "Full"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Event has been canceled",
                            DisplayOrder = 3,
                            Name = "Canceled"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Event has completed",
                            DisplayOrder = 4,
                            Name = "Completed"
                        });
                });

            modelBuilder.Entity("TrashMob.Models.EventType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("EventTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Park Cleanup",
                            DisplayOrder = 1,
                            IsActive = true,
                            Name = "Park Cleanup"
                        },
                        new
                        {
                            Id = 2,
                            Description = "School Cleanup",
                            DisplayOrder = 2,
                            IsActive = true,
                            Name = "School Cleanup"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Neighborhood Cleanup",
                            DisplayOrder = 3,
                            IsActive = true,
                            Name = "Neighborhood Cleanup"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Beach Cleanup",
                            DisplayOrder = 4,
                            IsActive = true,
                            Name = "Beach Cleanup"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Highway Cleanup",
                            DisplayOrder = 5,
                            IsActive = true,
                            Name = "Highway Cleanup"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Natural Disaster Cleanup",
                            DisplayOrder = 6,
                            IsActive = true,
                            Name = "Natural Disaster Cleanup"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Trail Cleanup",
                            DisplayOrder = 7,
                            IsActive = true,
                            Name = "Trail Cleanup"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Reef Cleanup",
                            DisplayOrder = 8,
                            IsActive = true,
                            Name = "Reef Cleanup"
                        },
                        new
                        {
                            Id = 9,
                            Description = "Private Land Cleanup",
                            DisplayOrder = 9,
                            IsActive = true,
                            Name = "Private Land Cleanup"
                        },
                        new
                        {
                            Id = 10,
                            Description = "Dog Park Cleanup",
                            DisplayOrder = 10,
                            IsActive = true,
                            Name = "Dog Park Cleanup"
                        },
                        new
                        {
                            Id = 11,
                            Description = "Waterway Cleanup",
                            DisplayOrder = 11,
                            IsActive = true,
                            Name = "Waterway Cleanup"
                        },
                        new
                        {
                            Id = 12,
                            Description = "Vandalism Cleanup",
                            DisplayOrder = 12,
                            IsActive = true,
                            Name = "Vandalism Cleanup"
                        },
                        new
                        {
                            Id = 13,
                            Description = "Social Event",
                            DisplayOrder = 13,
                            IsActive = true,
                            Name = "Social Event"
                        },
                        new
                        {
                            Id = 14,
                            Description = "Other",
                            DisplayOrder = 14,
                            IsActive = true,
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("TrashMob.Models.NotificationType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("NotificationTypes");
                });

            modelBuilder.Entity("TrashMob.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("DateAgreedToPrivacyPolicy")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateAgreedToTermsOfService")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("MemberSince")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("PrivacyPolicyVersion")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("RecruitedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TermsOfServiceVersion")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("RecruitedByUserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TrashMob.Models.UserFeedback", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comments")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("EventRating")
                        .HasColumnType("int");

                    b.Property<Guid>("RegardingUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("UserRating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("RegardingUserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFeedback");
                });

            modelBuilder.Entity("TrashMob.Models.UserSubscription", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FollowingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("UserId", "FollowingId");

                    b.HasIndex("FollowingId");

                    b.ToTable("UserSubscription");
                });

            modelBuilder.Entity("TrashMob.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.HasDiscriminator().HasValue("ApplicationUser");
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrashMob.Models.AttendeeNotification", b =>
                {
                    b.HasOne("TrashMob.Models.Event", "Event")
                        .WithMany("AttendeeNotifications")
                        .HasForeignKey("EventId")
                        .HasConstraintName("FK_AttendeeNotification_Event")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrashMob.Models.NotificationType", "NotificationType")
                        .WithMany("AttendeeNotifications")
                        .HasForeignKey("NotificationTypeId")
                        .HasConstraintName("FK_AttendeeNotification_NotificationType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrashMob.Models.User", "User")
                        .WithMany("AttendeeNotifications")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_AttendeeNotification_ApplicationUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("NotificationType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TrashMob.Models.Event", b =>
                {
                    b.HasOne("TrashMob.Models.User", "CreatedByUser")
                        .WithMany("EventsCreated")
                        .HasForeignKey("CreatedByUserId")
                        .HasConstraintName("FK_Events_ApplicationUser_CreatedBy")
                        .IsRequired();

                    b.HasOne("TrashMob.Models.EventStatus", "EventStatus")
                        .WithMany("Events")
                        .HasForeignKey("EventStatusId")
                        .HasConstraintName("FK_Events_EventStatuses")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrashMob.Models.EventType", "EventType")
                        .WithMany("Events")
                        .HasForeignKey("EventTypeId")
                        .HasConstraintName("FK_Events_EventTypes")
                        .IsRequired();

                    b.HasOne("TrashMob.Models.User", "LastUpdatedByUser")
                        .WithMany("EventsUpdated")
                        .HasForeignKey("LastUpdatedByUserId")
                        .HasConstraintName("FK_Events_ApplicationUser_LastUpdatedBy")
                        .IsRequired();

                    b.Navigation("CreatedByUser");

                    b.Navigation("EventStatus");

                    b.Navigation("EventType");

                    b.Navigation("LastUpdatedByUser");
                });

            modelBuilder.Entity("TrashMob.Models.EventAttendee", b =>
                {
                    b.HasOne("TrashMob.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .HasConstraintName("FK_EventAttendees_Events")
                        .IsRequired();

                    b.HasOne("TrashMob.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_EventAttendees_ApplicationUser")
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TrashMob.Models.User", b =>
                {
                    b.HasOne("TrashMob.Models.User", "RecruitedByUser")
                        .WithMany("UsersRecruited")
                        .HasForeignKey("RecruitedByUserId")
                        .HasConstraintName("FK_ApplicationUser_RecruitedBy")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("RecruitedByUser");
                });

            modelBuilder.Entity("TrashMob.Models.UserFeedback", b =>
                {
                    b.HasOne("TrashMob.Models.Event", "Event")
                        .WithMany("UserFeedback")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrashMob.Models.User", "RegardingUser")
                        .WithMany("UserFeedbackRegardingUsers")
                        .HasForeignKey("RegardingUserId")
                        .HasConstraintName("FK_UserFeedback_ApplicationUserRegarding")
                        .IsRequired();

                    b.HasOne("TrashMob.Models.User", "User")
                        .WithMany("UserFeedbackUsers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserFeedback_ApplicationUser")
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("RegardingUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TrashMob.Models.UserSubscription", b =>
                {
                    b.HasOne("TrashMob.Models.User", "FollowingUser")
                        .WithMany("UsersFollowed")
                        .HasForeignKey("FollowingId")
                        .HasConstraintName("FK_UserSubscriptions_ApplicationUsersFollowing")
                        .IsRequired();

                    b.HasOne("TrashMob.Models.User", "User")
                        .WithMany("UsersFollowing")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserSubscriptions_ApplicationUserFollowed")
                        .IsRequired();

                    b.Navigation("FollowingUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TrashMob.Models.Event", b =>
                {
                    b.Navigation("AttendeeNotifications");

                    b.Navigation("UserFeedback");
                });

            modelBuilder.Entity("TrashMob.Models.EventStatus", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("TrashMob.Models.EventType", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("TrashMob.Models.NotificationType", b =>
                {
                    b.Navigation("AttendeeNotifications");
                });

            modelBuilder.Entity("TrashMob.Models.User", b =>
                {
                    b.Navigation("AttendeeNotifications");

                    b.Navigation("EventsCreated");

                    b.Navigation("EventsUpdated");

                    b.Navigation("UserFeedbackRegardingUsers");

                    b.Navigation("UserFeedbackUsers");

                    b.Navigation("UsersFollowed");

                    b.Navigation("UsersFollowing");

                    b.Navigation("UsersRecruited");
                });
#pragma warning restore 612, 618
        }
    }
}
