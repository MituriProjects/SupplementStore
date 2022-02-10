using Microsoft.EntityFrameworkCore.Migrations;

namespace SupplementStore.Infrastructure.Migrations
{
    public partial class OrderAddressValueObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Orders",
                newName: "Address_PostalCode");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Orders",
                newName: "Address_City");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Orders",
                newName: "Address_Street");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_PostalCode",
                table: "Orders",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Orders",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Orders",
                newName: "Address");
        }
    }
}
