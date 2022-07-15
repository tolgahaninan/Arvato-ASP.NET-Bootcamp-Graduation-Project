using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GradBootcamp_Tolgahaninan.Migrations
{
    public partial class initialized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "genres",
                table: "mytable",
                type: "character varying(264)",
                maxLength: 264,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(264)",
                oldMaxLength: 264);

            migrationBuilder.CreateTable(
                name: "movieViews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    movieId = table.Column<long>(type: "bigint", nullable: false),
                    ClickCounter = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movieViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_movieViews_mytable_movieId",
                        column: x => x.movieId,
                        principalTable: "mytable",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_movieViews_movieId",
                table: "movieViews",
                column: "movieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movieViews");

            migrationBuilder.AlterColumn<string>(
                name: "genres",
                table: "mytable",
                type: "character varying(264)",
                maxLength: 264,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(264)",
                oldMaxLength: 264,
                oldNullable: true);
        }
    }
}
