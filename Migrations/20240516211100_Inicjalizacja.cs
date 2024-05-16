using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BadanieKrwi.Migrations
{
    /// <inheritdoc />
    public partial class Inicjalizacja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Badania",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypBadania = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataBadania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NazwaKliniki = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StezenieErytrocytowRbc = table.Column<int>(type: "int", nullable: false),
                    HemoglobinaHb = table.Column<int>(type: "int", nullable: false),
                    HematokrytHtc = table.Column<int>(type: "int", nullable: false),
                    SredniaObjetoscErytrocytuMcv = table.Column<double>(type: "float", nullable: false),
                    SredniaMasaHemoglobinyWErytrocycieMch = table.Column<double>(type: "float", nullable: false),
                    SrednieStezenieHemoglobinyWErytrocytachMchc = table.Column<double>(type: "float", nullable: false),
                    RozpietoscRozkladuObjetosciErytrocytowRdwCw = table.Column<double>(type: "float", nullable: false),
                    RetikulocytyRc = table.Column<double>(type: "float", nullable: false),
                    StezenieLeukocytowWbc = table.Column<int>(type: "int", nullable: false),
                    Neutrofile = table.Column<int>(type: "int", nullable: false),
                    Bazofile = table.Column<int>(type: "int", nullable: false),
                    Eozynofile = table.Column<int>(type: "int", nullable: false),
                    Limfocyty = table.Column<int>(type: "int", nullable: false),
                    Monocyty = table.Column<int>(type: "int", nullable: false),
                    PlytkiKrwiPlt = table.Column<int>(type: "int", nullable: false),
                    SredniaObjetoscKrwiMpv = table.Column<double>(type: "float", nullable: false),
                    Zelazo = table.Column<double>(type: "float", nullable: false),
                    Magnez = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badania", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kalendarz",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUzytkownika = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataBadania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdKliniki = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypBadania = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tresc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CzyUstawionoPrzypomnienie = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kalendarz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kliniki",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Informacja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NazwaITelefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kliniki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownik",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "Kalendarz");

            migrationBuilder.DropTable(
                name: "Kliniki");

            migrationBuilder.DropTable(
                name: "Uzytkownik");
        }
    }
}
