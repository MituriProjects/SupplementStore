using Microsoft.EntityFrameworkCore.Migrations;

namespace SupplementStore.Infrastructure.Migrations
{
    public partial class Opinion_IsHidden : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Opinions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Opinions");
        }
    }
}
