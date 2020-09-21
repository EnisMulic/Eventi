using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Event_Attender.Data.Migrations
{
    public partial class Prva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzava",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Izvodjac",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    TipIzvodjaca = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izvodjac", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogPodaci",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogPodaci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sponzor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponzor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    DrzavaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grad_Drzava_DrzavaId",
                        column: x => x.DrzavaId,
                        principalTable: "Drzava",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizator",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    GradId = table.Column<int>(nullable: true),
                    LogPodaciId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizator_Grad_GradId",
                        column: x => x.GradId,
                        principalTable: "Grad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organizator_LogPodaci_LogPodaciId",
                        column: x => x.LogPodaciId,
                        principalTable: "LogPodaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Osoba",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    GradId = table.Column<int>(nullable: true),
                    LogPodaciId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osoba", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Osoba_Grad_GradId",
                        column: x => x.GradId,
                        principalTable: "Grad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Osoba_LogPodaci_LogPodaciId",
                        column: x => x.LogPodaciId,
                        principalTable: "LogPodaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProstorOdrzavanja",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    TipProstoraOdrzavanja = table.Column<int>(nullable: false),
                    GradId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProstorOdrzavanja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProstorOdrzavanja_Grad_GradId",
                        column: x => x.GradId,
                        principalTable: "Grad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OsobaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrator_Osoba_OsobaId",
                        column: x => x.OsobaId,
                        principalTable: "Osoba",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(nullable: true),
                    PostanskiBroj = table.Column<string>(nullable: true),
                    OsobaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Korisnik_Osoba_OsobaId",
                        column: x => x.OsobaId,
                        principalTable: "Osoba",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Radnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OsobaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Radnik_Osoba_OsobaId",
                        column: x => x.OsobaId,
                        principalTable: "Osoba",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    DatumOdrzavanja = table.Column<DateTime>(nullable: false),
                    VrijemeOdrzavanja = table.Column<string>(nullable: true),
                    Kategorija = table.Column<int>(nullable: false),
                    IsOdobren = table.Column<bool>(nullable: false),
                    IsOtkazan = table.Column<bool>(nullable: false),
                    Slika = table.Column<byte[]>(nullable: true),
                    OrganizatorId = table.Column<int>(nullable: false),
                    AdministratorId = table.Column<int>(nullable: true),
                    ProstorOdrzavanjaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Administrator_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "Administrator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_Organizator_OrganizatorId",
                        column: x => x.OrganizatorId,
                        principalTable: "Organizator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_ProstorOdrzavanja_ProstorOdrzavanjaId",
                        column: x => x.ProstorOdrzavanjaId,
                        principalTable: "ProstorOdrzavanja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IzvodjacEvent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IzvodjacId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzvodjacEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IzvodjacEvent_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IzvodjacEvent_Izvodjac_IzvodjacId",
                        column: x => x.IzvodjacId,
                        principalTable: "Izvodjac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kupovina",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupovina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kupovina_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kupovina_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    DatumLajka = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Like_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdajaTip",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipKarte = table.Column<int>(nullable: false),
                    UkupnoKarataTip = table.Column<int>(nullable: false),
                    BrojProdatihKarataTip = table.Column<int>(nullable: false),
                    CijenaTip = table.Column<float>(nullable: false),
                    PostojeSjedista = table.Column<bool>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdajaTip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdajaTip_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RadnikEvent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RadnikId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadnikEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RadnikEvent_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RadnikEvent_Radnik_RadnikId",
                        column: x => x.RadnikId,
                        principalTable: "Radnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SponzorEvent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(nullable: false),
                    SponzorId = table.Column<int>(nullable: false),
                    Prioritet = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SponzorEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SponzorEvent_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SponzorEvent_Sponzor_SponzorId",
                        column: x => x.SponzorId,
                        principalTable: "Sponzor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recenzija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ocjena = table.Column<int>(nullable: false),
                    Komentar = table.Column<string>(nullable: true),
                    KupovinaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recenzija_Kupovina_KupovinaId",
                        column: x => x.KupovinaId,
                        principalTable: "Kupovina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KupovinaTip",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KupovinaId = table.Column<int>(nullable: false),
                    TipKarte = table.Column<int>(nullable: false),
                    BrojKarata = table.Column<int>(nullable: false),
                    Cijena = table.Column<float>(nullable: false),
                    ProdajaTipId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KupovinaTip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KupovinaTip_Kupovina_KupovinaId",
                        column: x => x.KupovinaId,
                        principalTable: "Kupovina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KupovinaTip_ProdajaTip_ProdajaTipId",
                        column: x => x.ProdajaTipId,
                        principalTable: "ProdajaTip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Karta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<int>(nullable: false),
                    Cijena = table.Column<float>(nullable: false),
                    KupovinaTipId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Karta_KupovinaTip_KupovinaTipId",
                        column: x => x.KupovinaTipId,
                        principalTable: "KupovinaTip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sjediste",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojSjedista = table.Column<int>(nullable: false),
                    KartaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sjediste", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sjediste_Karta_KartaId",
                        column: x => x.KartaId,
                        principalTable: "Karta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrator_OsobaId",
                table: "Administrator",
                column: "OsobaId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_AdministratorId",
                table: "Event",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_OrganizatorId",
                table: "Event",
                column: "OrganizatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_ProstorOdrzavanjaId",
                table: "Event",
                column: "ProstorOdrzavanjaId");

            migrationBuilder.CreateIndex(
                name: "IX_Grad_DrzavaId",
                table: "Grad",
                column: "DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_IzvodjacEvent_EventId",
                table: "IzvodjacEvent",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_IzvodjacEvent_IzvodjacId",
                table: "IzvodjacEvent",
                column: "IzvodjacId");

            migrationBuilder.CreateIndex(
                name: "IX_Karta_KupovinaTipId",
                table: "Karta",
                column: "KupovinaTipId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_OsobaId",
                table: "Korisnik",
                column: "OsobaId");

            migrationBuilder.CreateIndex(
                name: "IX_Kupovina_EventId",
                table: "Kupovina",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Kupovina_KorisnikId",
                table: "Kupovina",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_KupovinaTip_KupovinaId",
                table: "KupovinaTip",
                column: "KupovinaId");

            migrationBuilder.CreateIndex(
                name: "IX_KupovinaTip_ProdajaTipId",
                table: "KupovinaTip",
                column: "ProdajaTipId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_EventId",
                table: "Like",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_KorisnikId",
                table: "Like",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizator_GradId",
                table: "Organizator",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizator_LogPodaciId",
                table: "Organizator",
                column: "LogPodaciId");

            migrationBuilder.CreateIndex(
                name: "IX_Osoba_GradId",
                table: "Osoba",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Osoba_LogPodaciId",
                table: "Osoba",
                column: "LogPodaciId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdajaTip_EventId",
                table: "ProdajaTip",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ProstorOdrzavanja_GradId",
                table: "ProstorOdrzavanja",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Radnik_OsobaId",
                table: "Radnik",
                column: "OsobaId");

            migrationBuilder.CreateIndex(
                name: "IX_RadnikEvent_EventId",
                table: "RadnikEvent",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_RadnikEvent_RadnikId",
                table: "RadnikEvent",
                column: "RadnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_KupovinaId",
                table: "Recenzija",
                column: "KupovinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sjediste_KartaId",
                table: "Sjediste",
                column: "KartaId");

            migrationBuilder.CreateIndex(
                name: "IX_SponzorEvent_EventId",
                table: "SponzorEvent",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_SponzorEvent_SponzorId",
                table: "SponzorEvent",
                column: "SponzorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IzvodjacEvent");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropTable(
                name: "RadnikEvent");

            migrationBuilder.DropTable(
                name: "Recenzija");

            migrationBuilder.DropTable(
                name: "Sjediste");

            migrationBuilder.DropTable(
                name: "SponzorEvent");

            migrationBuilder.DropTable(
                name: "Izvodjac");

            migrationBuilder.DropTable(
                name: "Radnik");

            migrationBuilder.DropTable(
                name: "Karta");

            migrationBuilder.DropTable(
                name: "Sponzor");

            migrationBuilder.DropTable(
                name: "KupovinaTip");

            migrationBuilder.DropTable(
                name: "Kupovina");

            migrationBuilder.DropTable(
                name: "ProdajaTip");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "Organizator");

            migrationBuilder.DropTable(
                name: "ProstorOdrzavanja");

            migrationBuilder.DropTable(
                name: "Osoba");

            migrationBuilder.DropTable(
                name: "Grad");

            migrationBuilder.DropTable(
                name: "LogPodaci");

            migrationBuilder.DropTable(
                name: "Drzava");
        }
    }
}
