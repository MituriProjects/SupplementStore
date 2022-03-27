using Microsoft.EntityFrameworkCore.Migrations;

namespace SupplementStore.Infrastructure.Migrations
{
    public partial class RenameGradeToRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Grade_Stars",
                table: "Opinions",
                newName: "Rating_Stars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating_Stars",
                table: "Opinions",
                newName: "Grade_Stars");
        }
    }
}
