﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RDManipulacao.Infrastructure.Data;

#nullable disable

namespace RDManipulacao.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("RDManipulacao.Domain.Entities.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ChannelName")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "autor");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "descricao");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "duracao");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "excluido");

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "dataPublicacao");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "titulo");

                    b.HasKey("Id");

                    b.ToTable("Videos");
                });
#pragma warning restore 612, 618
        }
    }
}
