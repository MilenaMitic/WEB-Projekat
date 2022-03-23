using Microsoft.EntityFrameworkCore.Migrations;

namespace KoncertneHale.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hala",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hala", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Izvodjac",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izvodjac", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SBR = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Koncerti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivKoncerta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vreme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    IzvodjacID = table.Column<int>(type: "int", nullable: false),
                    HalaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Koncerti", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Koncerti_Hala_HalaID",
                        column: x => x.HalaID,
                        principalTable: "Hala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Koncerti_Izvodjac_IzvodjacID",
                        column: x => x.IzvodjacID,
                        principalTable: "Izvodjac",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Karte",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojSedista = table.Column<int>(type: "int", nullable: false),
                    RezervacijaID = table.Column<int>(type: "int", nullable: true),
                    KoncertID = table.Column<int>(type: "int", nullable: true),
                    HalaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karte", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Karte_Hala_HalaID",
                        column: x => x.HalaID,
                        principalTable: "Hala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Karte_Koncerti_KoncertID",
                        column: x => x.KoncertID,
                        principalTable: "Koncerti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Karte_Rezervacije_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacije",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Karte_HalaID",
                table: "Karte",
                column: "HalaID");

            migrationBuilder.CreateIndex(
                name: "IX_Karte_KoncertID",
                table: "Karte",
                column: "KoncertID");

            migrationBuilder.CreateIndex(
                name: "IX_Karte_RezervacijaID",
                table: "Karte",
                column: "RezervacijaID",
                unique: true,
                filter: "[RezervacijaID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Koncerti_HalaID",
                table: "Koncerti",
                column: "HalaID");

            migrationBuilder.CreateIndex(
                name: "IX_Koncerti_IzvodjacID",
                table: "Koncerti",
                column: "IzvodjacID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Karte");

            migrationBuilder.DropTable(
                name: "Koncerti");

            migrationBuilder.DropTable(
                name: "Rezervacije");

            migrationBuilder.DropTable(
                name: "Hala");

            migrationBuilder.DropTable(
                name: "Izvodjac");
        }
    }
}
