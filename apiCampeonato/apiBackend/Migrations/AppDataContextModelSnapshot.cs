﻿// <auto-generated />
using ApiBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace apiBackend.Migrations
{
    [DbContext(typeof(AppDataContext))]
    partial class AppDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.22");

            modelBuilder.Entity("ApiBackend.Models.Campeonato", b =>
                {
                    b.Property<int>("CampeonatoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<double>("Premiacao")
                        .HasColumnType("REAL");

                    b.HasKey("CampeonatoId");

                    b.ToTable("Campeonatos");
                });

            modelBuilder.Entity("ApiBackend.Models.Time", b =>
                {
                    b.Property<int>("TimeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("TimeId");

                    b.ToTable("Times");
                });
#pragma warning restore 612, 618
        }
    }
}
