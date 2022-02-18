using Microsoft.EntityFrameworkCore.Migrations;

namespace SupplementStore.Infrastructure.Migrations
{
    public partial class BasketProduct_ProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Products_ProductId",
                table: "BasketProducts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "BasketProducts",
                newName: "Product_Id");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProducts_ProductId",
                table: "BasketProducts",
                newName: "IX_BasketProducts_Product_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Products_Product_Id",
                table: "BasketProducts",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Products_Product_Id",
                table: "BasketProducts");

            migrationBuilder.RenameColumn(
                name: "Product_Id",
                table: "BasketProducts",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProducts_Product_Id",
                table: "BasketProducts",
                newName: "IX_BasketProducts_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Products_ProductId",
                table: "BasketProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
