using Microsoft.EntityFrameworkCore.Migrations;

namespace Application_Web_ASP.NET_Core.Migrations
{
    public partial class CreationBDFilmRating2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Compte",
                schema: "public",
                table: "T_J_FAVORI_FAV");

            migrationBuilder.DropColumn(
                name: "Film",
                schema: "public",
                table: "T_J_FAVORI_FAV");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Compte",
                schema: "public",
                table: "T_J_FAVORI_FAV",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Film",
                schema: "public",
                table: "T_J_FAVORI_FAV",
                type: "integer",
                nullable: true);
        }
    }
}
