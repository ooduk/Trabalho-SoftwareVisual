﻿// <auto-generated />
using ApiBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace apiBackend.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20231124022659_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("ApiBackend.Models.Confronto", b =>
                {
                    b.Property<int>("ConfrontoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CampeonatoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gols_time_casa")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gols_time_fora")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TimeCasaId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TimeForaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ConfrontoId");

                    b.HasIndex("CampeonatoId");

                    b.HasIndex("TimeCasaId");

                    b.HasIndex("TimeForaId");

                    b.ToTable("Confrontos");
                });

            modelBuilder.Entity("ApiBackend.Models.Tabela", b =>
                {
                    b.Property<int>("TabelaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CampeonatoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Derrotas")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Empates")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gols_contra")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gols_marcados")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Pontos")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TimeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Vitorias")
                        .HasColumnType("INTEGER");

                    b.HasKey("TabelaId");

                    b.HasIndex("CampeonatoId");

                    b.HasIndex("TimeId");

                    b.ToTable("Tabelas");
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

            modelBuilder.Entity("ApiBackend.Models.Confronto", b =>
                {
                    b.HasOne("ApiBackend.Models.Campeonato", "Campeonato")
                        .WithMany()
                        .HasForeignKey("CampeonatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiBackend.Models.Time", "TimeCasa")
                        .WithMany()
                        .HasForeignKey("TimeCasaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiBackend.Models.Time", "TimeFora")
                        .WithMany()
                        .HasForeignKey("TimeForaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campeonato");

                    b.Navigation("TimeCasa");

                    b.Navigation("TimeFora");
                });

            modelBuilder.Entity("ApiBackend.Models.Tabela", b =>
                {
                    b.HasOne("ApiBackend.Models.Campeonato", "Campeonato")
                        .WithMany()
                        .HasForeignKey("CampeonatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiBackend.Models.Time", "Time")
                        .WithMany()
                        .HasForeignKey("TimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campeonato");

                    b.Navigation("Time");
                });
#pragma warning restore 612, 618
        }
    }
}