﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using wiseguy;

namespace api.Migrations
{
    [DbContext(typeof(WiseGuyContext))]
    [Migration("20210211121813_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("MaillistParticipant", b =>
                {
                    b.Property<int>("MaillistsId")
                        .HasColumnType("int");

                    b.Property<string>("ParticipantsEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MaillistsId", "ParticipantsEmail");

                    b.HasIndex("ParticipantsEmail");

                    b.ToTable("MaillistParticipant");
                });

            modelBuilder.Entity("wiseguy.Answer", b =>
                {
                    b.Property<int>("PhraseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AnswerType")
                        .HasColumnType("int");

                    b.Property<int?>("SheetId")
                        .HasColumnType("int");

                    b.HasKey("PhraseId");

                    b.HasIndex("SheetId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("wiseguy.Maillist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Maillists");
                });

            modelBuilder.Entity("wiseguy.Participant", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("wiseguy.Phrase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("SheetTemplateId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SheetTemplateId");

                    b.ToTable("Phrases");
                });

            modelBuilder.Entity("wiseguy.SheetCopy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Completed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IssueId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SheetTemplateId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.HasIndex("SheetTemplateId");

                    b.ToTable("Copies");
                });

            modelBuilder.Entity("wiseguy.SheetIssue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Issued")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MaillistId")
                        .HasColumnType("int");

                    b.Property<int?>("SheetTemplateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaillistId");

                    b.HasIndex("SheetTemplateId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("wiseguy.SheetTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Course")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("MaillistParticipant", b =>
                {
                    b.HasOne("wiseguy.Maillist", null)
                        .WithMany()
                        .HasForeignKey("MaillistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("wiseguy.Participant", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("wiseguy.Answer", b =>
                {
                    b.HasOne("wiseguy.SheetCopy", "Sheet")
                        .WithMany()
                        .HasForeignKey("SheetId");

                    b.Navigation("Sheet");
                });

            modelBuilder.Entity("wiseguy.Phrase", b =>
                {
                    b.HasOne("wiseguy.SheetTemplate", null)
                        .WithMany("Phrases")
                        .HasForeignKey("SheetTemplateId");
                });

            modelBuilder.Entity("wiseguy.SheetCopy", b =>
                {
                    b.HasOne("wiseguy.SheetIssue", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueId");

                    b.HasOne("wiseguy.SheetTemplate", "SheetTemplate")
                        .WithMany()
                        .HasForeignKey("SheetTemplateId");

                    b.Navigation("Issue");

                    b.Navigation("SheetTemplate");
                });

            modelBuilder.Entity("wiseguy.SheetIssue", b =>
                {
                    b.HasOne("wiseguy.Maillist", "Maillist")
                        .WithMany()
                        .HasForeignKey("MaillistId");

                    b.HasOne("wiseguy.SheetTemplate", "SheetTemplate")
                        .WithMany()
                        .HasForeignKey("SheetTemplateId");

                    b.Navigation("Maillist");

                    b.Navigation("SheetTemplate");
                });

            modelBuilder.Entity("wiseguy.SheetTemplate", b =>
                {
                    b.Navigation("Phrases");
                });
#pragma warning restore 612, 618
        }
    }
}
