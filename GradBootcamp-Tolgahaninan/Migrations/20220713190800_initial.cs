using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GradBootcamp_Tolgahaninan.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.id);
                });

      /*      migrationBuilder.CreateTable(
                name: "mytable",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    adult = table.Column<string>(type: "character varying(126)", maxLength: 126, nullable: false),
                    belongs_to_collection = table.Column<string>(type: "character varying(184)", maxLength: 184, nullable: true),
                    budget = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    genres = table.Column<string>(type: "character varying(264)", maxLength: 264, nullable: false),
                    homepage = table.Column<string>(type: "character varying(242)", maxLength: 242, nullable: true),
                    imdb_id = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: true),
                    original_language = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    original_title = table.Column<string>(type: "character varying(109)", maxLength: 109, nullable: false),
                    overview = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    popularity = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: true),
                    poster_path = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: true),
                    production_companies = table.Column<string>(type: "character varying(1252)", maxLength: 1252, nullable: true),
                    production_countries = table.Column<string>(type: "character varying(1039)", maxLength: 1039, nullable: true),
                    release_date = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    revenue = table.Column<int>(type: "integer", nullable: true),
                    runtime = table.Column<decimal>(type: "numeric(6,1)", precision: 6, scale: 1, nullable: true),
                    spoken_languages = table.Column<string>(type: "character varying(765)", maxLength: 765, nullable: true),
                    status = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    tagline = table.Column<string>(type: "character varying(297)", maxLength: 297, nullable: true),
                    title = table.Column<string>(type: "character varying(105)", maxLength: 105, nullable: true),
                    video = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    vote_average = table.Column<decimal>(type: "numeric(4,1)", precision: 4, scale: 1, nullable: true),
                    vote_count = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mytable", x => x.id);
                });
      */
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "mytable");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
