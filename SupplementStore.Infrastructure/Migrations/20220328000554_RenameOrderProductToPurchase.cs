using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SupplementStore.Infrastructure.Migrations
{
    public partial class RenameOrderProductToPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opinions_OrderProducts_OrderProduct_Id",
                table: "Opinions");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "OrderProduct_Id",
                table: "Opinions",
                newName: "Purchase_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Opinions_OrderProduct_Id",
                table: "Opinions",
                newName: "IX_Opinions_Purchase_Id");

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    Order_Id = table.Column<Guid>(nullable: false),
                    Product_Id = table.Column<Guid>(nullable: false),
                    Opinion_Id = table.Column<Guid>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Opinions_Opinion_Id",
                        column: x => x.Opinion_Id,
                        principalTable: "Opinions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchases_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_Opinion_Id",
                table: "Purchases",
                column: "Opinion_Id",
                unique: true,
                filter: "[Opinion_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_Order_Id",
                table: "Purchases",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_Product_Id",
                table: "Purchases",
                column: "Product_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Opinions_Purchases_Purchase_Id",
                table: "Opinions",
                column: "Purchase_Id",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Opinions_Purchases_Purchase_Id",
                table: "Opinions");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.RenameColumn(
                name: "Purchase_Id",
                table: "Opinions",
                newName: "OrderProduct_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Opinions_Purchase_Id",
                table: "Opinions",
                newName: "IX_Opinions_OrderProduct_Id");

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    Opinion_Id = table.Column<Guid>(nullable: true),
                    Order_Id = table.Column<Guid>(nullable: false),
                    Product_Id = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Opinions_Opinion_Id",
                        column: x => x.Opinion_Id,
                        principalTable: "Opinions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_Opinion_Id",
                table: "OrderProducts",
                column: "Opinion_Id",
                unique: true,
                filter: "[Opinion_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_Order_Id",
                table: "OrderProducts",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_Product_Id",
                table: "OrderProducts",
                column: "Product_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Opinions_OrderProducts_OrderProduct_Id",
                table: "Opinions",
                column: "OrderProduct_Id",
                principalTable: "OrderProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
