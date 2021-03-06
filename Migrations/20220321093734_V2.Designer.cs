// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace KoncertneHale.Migrations
{
    [DbContext(typeof(KoncertneHaleContext))]
    [Migration("20220321093734_V2")]
    partial class V2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Hala", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Hala");
                });

            modelBuilder.Entity("Models.Izvodjac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Izvodjac");
                });

            modelBuilder.Entity("Models.Karta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojSedista")
                        .HasColumnType("int");

                    b.Property<int?>("HalaID")
                        .HasColumnType("int");

                    b.Property<int?>("KoncertID")
                        .HasColumnType("int");

                    b.Property<int?>("RezervacijaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("HalaID");

                    b.HasIndex("KoncertID");

                    b.HasIndex("RezervacijaID")
                        .IsUnique()
                        .HasFilter("[RezervacijaID] IS NOT NULL");

                    b.ToTable("Karte");
                });

            modelBuilder.Entity("Models.Koncert", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cena")
                        .HasColumnType("int");

                    b.Property<string>("Datum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HalaID")
                        .HasColumnType("int");

                    b.Property<int>("IzvodjacID")
                        .HasColumnType("int");

                    b.Property<string>("NazivKoncerta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vreme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("HalaID");

                    b.HasIndex("IzvodjacID");

                    b.ToTable("Koncerti");
                });

            modelBuilder.Entity("Models.Rezervacija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("SBR")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Rezervacije");
                });

            modelBuilder.Entity("Models.Karta", b =>
                {
                    b.HasOne("Models.Hala", "Hala")
                        .WithMany("Karte")
                        .HasForeignKey("HalaID");

                    b.HasOne("Models.Koncert", "Koncert")
                        .WithMany()
                        .HasForeignKey("KoncertID");

                    b.HasOne("Models.Rezervacija", "Rezervacija")
                        .WithOne("Karta")
                        .HasForeignKey("Models.Karta", "RezervacijaID");

                    b.Navigation("Hala");

                    b.Navigation("Koncert");

                    b.Navigation("Rezervacija");
                });

            modelBuilder.Entity("Models.Koncert", b =>
                {
                    b.HasOne("Models.Hala", "Hala")
                        .WithMany("Koncerti")
                        .HasForeignKey("HalaID");

                    b.HasOne("Models.Izvodjac", "Izvodjac")
                        .WithMany("Koncerti")
                        .HasForeignKey("IzvodjacID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hala");

                    b.Navigation("Izvodjac");
                });

            modelBuilder.Entity("Models.Hala", b =>
                {
                    b.Navigation("Karte");

                    b.Navigation("Koncerti");
                });

            modelBuilder.Entity("Models.Izvodjac", b =>
                {
                    b.Navigation("Koncerti");
                });

            modelBuilder.Entity("Models.Rezervacija", b =>
                {
                    b.Navigation("Karta");
                });
#pragma warning restore 612, 618
        }
    }
}
