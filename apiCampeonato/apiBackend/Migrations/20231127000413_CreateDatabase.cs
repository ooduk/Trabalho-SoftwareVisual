using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiBackend.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campeonatos",
                columns: table => new
                {
                    CampeonatoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Premiacao = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campeonatos", x => x.CampeonatoId);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    TimeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.TimeId);
                });

            migrationBuilder.CreateTable(
                name: "Confrontos",
                columns: table => new
                {
                    ConfrontoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimeCasaId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeForaId = table.Column<int>(type: "INTEGER", nullable: false),
                    CampeonatoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Gols_time_casa = table.Column<int>(type: "INTEGER", nullable: false),
                    Gols_time_fora = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Confrontos", x => x.ConfrontoId);
                    table.ForeignKey(
                        name: "FK_Confrontos_Campeonatos_CampeonatoId",
                        column: x => x.CampeonatoId,
                        principalTable: "Campeonatos",
                        principalColumn: "CampeonatoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Confrontos_Times_TimeCasaId",
                        column: x => x.TimeCasaId,
                        principalTable: "Times",
                        principalColumn: "TimeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Confrontos_Times_TimeForaId",
                        column: x => x.TimeForaId,
                        principalTable: "Times",
                        principalColumn: "TimeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tabelas",
                columns: table => new
                {
                    TabelaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CampeonatoId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Pontos = table.Column<int>(type: "INTEGER", nullable: false),
                    Gols_marcados = table.Column<int>(type: "INTEGER", nullable: false),
                    Gols_contra = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitorias = table.Column<int>(type: "INTEGER", nullable: false),
                    Empates = table.Column<int>(type: "INTEGER", nullable: false),
                    Derrotas = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabelas", x => x.TabelaId);
                    table.ForeignKey(
                        name: "FK_Tabelas_Campeonatos_CampeonatoId",
                        column: x => x.CampeonatoId,
                        principalTable: "Campeonatos",
                        principalColumn: "CampeonatoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tabelas_Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "TimeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Confrontos_CampeonatoId",
                table: "Confrontos",
                column: "CampeonatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Confrontos_TimeCasaId",
                table: "Confrontos",
                column: "TimeCasaId");

            migrationBuilder.CreateIndex(
                name: "IX_Confrontos_TimeForaId",
                table: "Confrontos",
                column: "TimeForaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tabelas_CampeonatoId",
                table: "Tabelas",
                column: "CampeonatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tabelas_TimeId",
                table: "Tabelas",
                column: "TimeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Confrontos");

            migrationBuilder.DropTable(
                name: "Tabelas");

            migrationBuilder.DropTable(
                name: "Campeonatos");

            migrationBuilder.DropTable(
                name: "Times");
        }
    }
}
