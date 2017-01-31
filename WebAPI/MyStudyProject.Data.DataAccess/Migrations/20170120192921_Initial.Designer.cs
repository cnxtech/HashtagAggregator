﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MyStudyProject.Data.DataAccess.Context;
using MyStudyProject.Shared.Contracts.Enums;

namespace MyStudyProject.Data.DataAccess.Migrations
{
    [DbContext(typeof(SqlApplicationDbContext))]
    [Migration("20170120192921_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyStudyProject.Data.Entities.Entities.MessageEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<string>("HashTag");

                    b.Property<int>("MediaType");

                    b.Property<string>("NetworkId");

                    b.Property<DateTime?>("PostDate");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("NetworkId", "UserId")
                        .IsUnique();

                    b.ToTable("Messages");
                });
        }
    }
}