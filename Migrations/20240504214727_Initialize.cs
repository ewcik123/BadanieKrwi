using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BadanieKrwi.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Badania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKliniki = table.Column<int>(type: "int", nullable: false),
                    IdUzytkownika = table.Column<int>(type: "int", nullable: false),
                    DataBadania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StezenieErytrocytowRBC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HemoglobinaHb = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HematokrytHtc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SredniaObjetoscErytrocytuMCV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SredniaMasaHemoglobinyMCH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SrednieSteczenieHemoglobinyMCHC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RozpietoscRozkladuObjetosciErytrocytowRDWCV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RetikulocytyRC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StezenieLeukocytowWBC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Neutrofile = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bazofile = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Eozynofile = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Limfocyty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Monocyty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlytkiKrwiPLT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SredniaObjetoscKrwiMPV = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badania", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kliniki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lokalizacja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumerTelefonu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kliniki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasloHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wiek = table.Column<int>(type: "int", nullable: false),
                    Plec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataRejestracji = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownik", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Badania");

            migrationBuilder.DropTable(
                name: "Kliniki");

            migrationBuilder.DropTable(
                name: "Uzytkownik");
        }
    }
}
