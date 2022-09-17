using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisneyApi.Migrations
{
    public partial class addgeneros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_peliculas_generos_GeneroId",
                table: "peliculas");

            migrationBuilder.DropIndex(
                name: "IX_peliculas_GeneroId",
                table: "peliculas");

            migrationBuilder.DropColumn(
                name: "GeneroId",
                table: "peliculas");

            migrationBuilder.CreateTable(
                name: "GeneroPelicula",
                columns: table => new
                {
                    generosId = table.Column<int>(type: "int", nullable: false),
                    peliculasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneroPelicula", x => new { x.generosId, x.peliculasId });
                    table.ForeignKey(
                        name: "FK_GeneroPelicula_generos_generosId",
                        column: x => x.generosId,
                        principalTable: "generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneroPelicula_peliculas_peliculasId",
                        column: x => x.peliculasId,
                        principalTable: "peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GeneroPelicula_peliculasId",
                table: "GeneroPelicula",
                column: "peliculasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneroPelicula");

            migrationBuilder.AddColumn<int>(
                name: "GeneroId",
                table: "peliculas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_peliculas_GeneroId",
                table: "peliculas",
                column: "GeneroId");

            migrationBuilder.AddForeignKey(
                name: "FK_peliculas_generos_GeneroId",
                table: "peliculas",
                column: "GeneroId",
                principalTable: "generos",
                principalColumn: "Id");
        }
    }
}
