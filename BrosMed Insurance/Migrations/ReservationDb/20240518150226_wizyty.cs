using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrosMed_Insurance.Migrations.ReservationDb
{
    /// <inheritdoc />
    public partial class wizyty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Godziny",
                columns: table => new
                {
                    GodzinaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    godzinaVM = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Godziny", x => x.GodzinaId);
                });

            migrationBuilder.CreateTable(
                name: "Usluga",
                columns: table => new
                {
                    UslugaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cena = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usluga", x => x.UslugaId);
                });

            migrationBuilder.CreateTable(
                name: "Terminy",
                columns: table => new
                {
                    TerminyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    UslugaId = table.Column<int>(type: "int", nullable: false),
                    GodzinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminy", x => x.TerminyId);
                    table.ForeignKey(
                        name: "FK_Terminy_Godziny_GodzinaId",
                        column: x => x.GodzinaId,
                        principalTable: "Godziny",
                        principalColumn: "GodzinaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Terminy_Usluga_UslugaId",
                        column: x => x.UslugaId,
                        principalTable: "Usluga",
                        principalColumn: "UslugaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finalizacja",
                columns: table => new
                {
                    FinalizacjaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerminyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finalizacja", x => x.FinalizacjaId);
                    table.ForeignKey(
                        name: "FK_Finalizacja_Terminy_TerminyId",
                        column: x => x.TerminyId,
                        principalTable: "Terminy",
                        principalColumn: "TerminyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVisitHistory",
                columns: table => new
                {
                    UserVisitHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerminyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVisitHistory", x => x.UserVisitHistoryId);
                    table.ForeignKey(
                        name: "FK_UserVisitHistory_Terminy_TerminyId",
                        column: x => x.TerminyId,
                        principalTable: "Terminy",
                        principalColumn: "TerminyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Finalizacja_TerminyId",
                table: "Finalizacja",
                column: "TerminyId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminy_GodzinaId",
                table: "Terminy",
                column: "GodzinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminy_UslugaId",
                table: "Terminy",
                column: "UslugaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVisitHistory_TerminyId",
                table: "UserVisitHistory",
                column: "TerminyId");

            migrationBuilder.Sql("INSERT INTO Godziny (godzinaVM) values ('8.30')");
            migrationBuilder.Sql("INSERT INTO Godziny (godzinaVM) values ('10.00')");
            migrationBuilder.Sql("INSERT INTO Godziny (godzinaVM) values ('11.30')");
            migrationBuilder.Sql("INSERT INTO Godziny (godzinaVM) values ('13.00')");
            migrationBuilder.Sql("INSERT INTO Godziny (godzinaVM) values ('14.30')");

            migrationBuilder.Sql("INSERT INTO Usluga (Nazwa, Cena) values ('Przeswietlenie', '150zł')");
            migrationBuilder.Sql("INSERT INTO Usluga (Nazwa, Cena) values ('Profilaktyka', '100zł')");
            migrationBuilder.Sql("INSERT INTO Usluga (Nazwa, Cena) values ('Diagnoza złamania', '100zł')");
            migrationBuilder.Sql("INSERT INTO Usluga (Nazwa, Cena) values ('Diagnoza złamania + gips', '250zł')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Finalizacja");

            migrationBuilder.DropTable(
                name: "UserVisitHistory");

            migrationBuilder.DropTable(
                name: "Terminy");

            migrationBuilder.DropTable(
                name: "Godziny");

            migrationBuilder.DropTable(
                name: "Usluga");
        }
    }
}
