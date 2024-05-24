using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento_Conferencias.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbConferencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbConferencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbTrilha",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConferenciaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbTrilha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbTrilha_TbConferencia_ConferenciaId",
                        column: x => x.ConferenciaId,
                        principalTable: "TbConferencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbNetworkingEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrilhaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbNetworkingEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbNetworkingEvent_TbTrilha_TrilhaId",
                        column: x => x.TrilhaId,
                        principalTable: "TbTrilha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbPalestra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duracao = table.Column<int>(type: "int", nullable: false),
                    TrilhaId = table.Column<int>(type: "int", nullable: false),
                    Sessao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbPalestra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbPalestra_TbTrilha_TrilhaId",
                        column: x => x.TrilhaId,
                        principalTable: "TbTrilha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbNetworkingEvent_TrilhaId",
                table: "TbNetworkingEvent",
                column: "TrilhaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbPalestra_TrilhaId",
                table: "TbPalestra",
                column: "TrilhaId");

            migrationBuilder.CreateIndex(
                name: "IX_TbTrilha_ConferenciaId",
                table: "TbTrilha",
                column: "ConferenciaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbNetworkingEvent");

            migrationBuilder.DropTable(
                name: "TbPalestra");

            migrationBuilder.DropTable(
                name: "TbTrilha");

            migrationBuilder.DropTable(
                name: "TbConferencia");
        }
    }
}
