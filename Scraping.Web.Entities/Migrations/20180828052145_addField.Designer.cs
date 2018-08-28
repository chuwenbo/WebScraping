﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scraping.Web.Entities;

namespace Scraping.Web.Entities.Migrations
{
    [DbContext(typeof(ScrapingWebContext))]
    [Migration("20180828052145_addField")]
    partial class addField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Scraping.Web.Entities.FOREX.EconomicCalender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Actual")
                        .HasMaxLength(50);

                    b.Property<string>("Consensus")
                        .HasMaxLength(50);

                    b.Property<string>("Country")
                        .HasMaxLength(50);

                    b.Property<string>("Date")
                        .HasMaxLength(50);

                    b.Property<string>("Description");

                    b.Property<string>("Event")
                        .HasMaxLength(200);

                    b.Property<string>("Previous")
                        .HasMaxLength(50);

                    b.Property<string>("Time")
                        .HasMaxLength(50);

                    b.Property<string>("TimeZone")
                        .HasMaxLength(50);

                    b.Property<string>("Vol")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("EconomicCalenders");
                });
#pragma warning restore 612, 618
        }
    }
}