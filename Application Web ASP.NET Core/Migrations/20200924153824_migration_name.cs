using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Application_Web_ASP.NET_Core.Migrations
{
    public partial class migration_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "compte",
                schema: "public",
                columns: table => new
                {
                    CPT_ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CPT_NOM = table.Column<string>(maxLength: 50, nullable: true),
                    CPT_PRENOM = table.Column<string>(maxLength: 50, nullable: true),
                    CPT_TELPORTABLE = table.Column<string>(type: "char(10)", nullable: true),
                    CPT_MEL = table.Column<string>(maxLength: 100, nullable: true),
                    CPT_PWD = table.Column<string>(maxLength: 64, nullable: true),
                    CPT_RUE = table.Column<string>(maxLength: 200, nullable: true),
                    CPT_CP = table.Column<string>(type: "char(5)", nullable: true),
                    CPT_VILLE = table.Column<string>(type: "char(50)", nullable: true),
                    CPT_PAYS = table.Column<string>(type: "char(50)", nullable: true),
                    CPT_LATITUDE = table.Column<float>(nullable: true),
                    CPT_LONGITUDE = table.Column<float>(nullable: true),
                    CPT_DATECREATION = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compte", x => x.CPT_ID);
                });

            migrationBuilder.CreateTable(
                name: "film",
                schema: "public",
                columns: table => new
                {
                    FLM_ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FLM_TITRE = table.Column<string>(maxLength: 100, nullable: true),
                    FLM_SYNOPSIS = table.Column<string>(maxLength: 500, nullable: true),
                    FLM_DATEPARUTION = table.Column<string>(nullable: true),
                    FLM_DUREE = table.Column<decimal>(type: "numeric(3,0)", nullable: false),
                    FLM_GENRE = table.Column<string>(maxLength: 30, nullable: true),
                    FLM_URLPHOTO = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_film", x => x.FLM_ID);
                });

            migrationBuilder.CreateTable(
                name: "Favori",
                schema: "public",
                columns: table => new
                {
                    CPT_ID = table.Column<int>(nullable: false),
                    FLM_ID = table.Column<int>(nullable: false),
                    Compte = table.Column<int>(nullable: true),
                    Film = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_favori", x => new { x.FLM_ID, x.CPT_ID });
                    table.ForeignKey(
                        name: "fk_fav_com",
                        column: x => x.CPT_ID,
                        principalSchema: "public",
                        principalTable: "compte",
                        principalColumn: "CPT_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_fav_fil",
                        column: x => x.FLM_ID,
                        principalSchema: "public",
                        principalTable: "film",
                        principalColumn: "FLM_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favori_CPT_ID",
                schema: "public",
                table: "Favori",
                column: "CPT_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favori",
                schema: "public");

            migrationBuilder.DropTable(
                name: "compte",
                schema: "public");

            migrationBuilder.DropTable(
                name: "film",
                schema: "public");
        }
    }
}
