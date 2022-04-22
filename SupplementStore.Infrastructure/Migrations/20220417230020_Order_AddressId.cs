using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SupplementStore.Infrastructure.Migrations
{
    public partial class Order_AddressId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Address_PostalCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "Address_Id",
                table: "Orders",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Address_Id",
                table: "Orders",
                column: "Address_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_Address_Id",
                table: "Orders",
                column: "Address_Id",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_Address_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Address_Id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Address_Id",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Orders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_PostalCode",
                table: "Orders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Orders",
                nullable: false,
                defaultValue: "");
        }
    }
}
