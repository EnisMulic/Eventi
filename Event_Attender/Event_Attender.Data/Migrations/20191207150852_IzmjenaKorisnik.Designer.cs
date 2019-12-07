﻿// <auto-generated />
using System;
using EventAttender.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Event_Attender.Data.Migrations
{
    [DbContext(typeof(MojContext))]
    [Migration("20191207150852_IzmjenaKorisnik")]
    partial class IzmjenaKorisnik
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EventAttender.Data.Models.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OsobaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OsobaId");

                    b.ToTable("Administrator");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Drzava", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Drzava");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdministratorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatumOdrzavanja")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsOdobren")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOtkazan")
                        .HasColumnType("bit");

                    b.Property<int>("Kategorija")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizatorId")
                        .HasColumnType("int");

                    b.Property<int>("ProstorOdrzavanjaId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Slika")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("VrijemeOdrzavanja")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdministratorId");

                    b.HasIndex("OrganizatorId");

                    b.HasIndex("ProstorOdrzavanjaId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Grad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DrzavaId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DrzavaId");

                    b.ToTable("Grad");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Izvodjac", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipIzvodjaca")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Izvodjac");
                });

            modelBuilder.Entity("EventAttender.Data.Models.IzvodjacEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("IzvodjacId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("IzvodjacId");

                    b.ToTable("IzvodjacEvent");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Karta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Cijena")
                        .HasColumnType("real");

                    b.Property<int>("KupovinaTipId")
                        .HasColumnType("int");

                    b.Property<int>("Tip")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KupovinaTipId");

                    b.ToTable("Karta");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Korisnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojKreditneKartice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OsobaId")
                        .HasColumnType("int");

                    b.Property<string>("PostanskiBroj")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OsobaId");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Kupovina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Kupovina");
                });

            modelBuilder.Entity("EventAttender.Data.Models.KupovinaTip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojKarata")
                        .HasColumnType("int");

                    b.Property<float>("Cijena")
                        .HasColumnType("real");

                    b.Property<int>("KupovinaId")
                        .HasColumnType("int");

                    b.Property<int?>("ProdajaTipId")
                        .HasColumnType("int");

                    b.Property<int>("TipKarte")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KupovinaId");

                    b.HasIndex("ProdajaTipId");

                    b.ToTable("KupovinaTip");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumLajka")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Like");
                });

            modelBuilder.Entity("EventAttender.Data.Models.LogPodaci", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LogPodaci");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Organizator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GradId")
                        .HasColumnType("int");

                    b.Property<int?>("LogPodaciId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GradId");

                    b.HasIndex("LogPodaciId");

                    b.ToTable("Organizator");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Osoba", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GradId")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LogPodaciId")
                        .HasColumnType("int");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GradId");

                    b.HasIndex("LogPodaciId");

                    b.ToTable("Osoba");
                });

            modelBuilder.Entity("EventAttender.Data.Models.ProdajaTip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojProdatihKarataTip")
                        .HasColumnType("int");

                    b.Property<float>("CijenaTip")
                        .HasColumnType("real");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<bool>("PostojeSjedista")
                        .HasColumnType("bit");

                    b.Property<int>("TipKarte")
                        .HasColumnType("int");

                    b.Property<int>("UkupnoKarataTip")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("ProdajaTip");
                });

            modelBuilder.Entity("EventAttender.Data.Models.ProstorOdrzavanja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GradId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipProstoraOdrzavanja")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GradId");

                    b.ToTable("ProstorOdrzavanja");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Radnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OsobaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OsobaId");

                    b.ToTable("Radnik");
                });

            modelBuilder.Entity("EventAttender.Data.Models.RadnikEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("RadnikId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("RadnikId");

                    b.ToTable("RadnikEvent");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Recenzija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Komentar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KupovinaId")
                        .HasColumnType("int");

                    b.Property<int>("Ocjena")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KupovinaId");

                    b.ToTable("Recenzija");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Sjediste", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojSjedista")
                        .HasColumnType("int");

                    b.Property<int>("KartaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KartaId");

                    b.ToTable("Sjediste");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Sponzor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sponzor");
                });

            modelBuilder.Entity("EventAttender.Data.Models.SponzorEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("Prioritet")
                        .HasColumnType("int");

                    b.Property<int>("SponzorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("SponzorId");

                    b.ToTable("SponzorEvent");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Administrator", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Osoba", "Osoba")
                        .WithMany()
                        .HasForeignKey("OsobaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.Event", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Administrator", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorId");

                    b.HasOne("EventAttender.Data.Models.Organizator", "Organizator")
                        .WithMany()
                        .HasForeignKey("OrganizatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventAttender.Data.Models.ProstorOdrzavanja", "ProstorOdrzavanja")
                        .WithMany()
                        .HasForeignKey("ProstorOdrzavanjaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.Grad", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Drzava", "Drzava")
                        .WithMany()
                        .HasForeignKey("DrzavaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.IzvodjacEvent", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventAttender.Data.Models.Izvodjac", "Izvodjac")
                        .WithMany()
                        .HasForeignKey("IzvodjacId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.Karta", b =>
                {
                    b.HasOne("EventAttender.Data.Models.KupovinaTip", "KupovinaTip")
                        .WithMany()
                        .HasForeignKey("KupovinaTipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.Korisnik", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Osoba", "Osoba")
                        .WithMany()
                        .HasForeignKey("OsobaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.Kupovina", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventAttender.Data.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.KupovinaTip", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Kupovina", "Kupovina")
                        .WithMany()
                        .HasForeignKey("KupovinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventAttender.Data.Models.ProdajaTip", "ProdajaTip")
                        .WithMany()
                        .HasForeignKey("ProdajaTipId");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Like", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventAttender.Data.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.Organizator", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradId");

                    b.HasOne("EventAttender.Data.Models.LogPodaci", "LogPodaci")
                        .WithMany()
                        .HasForeignKey("LogPodaciId");
                });

            modelBuilder.Entity("EventAttender.Data.Models.Osoba", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradId");

                    b.HasOne("EventAttender.Data.Models.LogPodaci", "LogPodaci")
                        .WithMany()
                        .HasForeignKey("LogPodaciId");
                });

            modelBuilder.Entity("EventAttender.Data.Models.ProdajaTip", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.ProstorOdrzavanja", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.Radnik", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Osoba", "Osoba")
                        .WithMany()
                        .HasForeignKey("OsobaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.RadnikEvent", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventAttender.Data.Models.Radnik", "Radnik")
                        .WithMany()
                        .HasForeignKey("RadnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.Recenzija", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Kupovina", "Kupovina")
                        .WithMany()
                        .HasForeignKey("KupovinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.Sjediste", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Karta", "Karta")
                        .WithMany()
                        .HasForeignKey("KartaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventAttender.Data.Models.SponzorEvent", b =>
                {
                    b.HasOne("EventAttender.Data.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EventAttender.Data.Models.Sponzor", "Sponzor")
                        .WithMany()
                        .HasForeignKey("SponzorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
