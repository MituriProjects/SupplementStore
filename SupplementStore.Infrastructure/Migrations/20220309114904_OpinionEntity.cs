using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SupplementStore.Infrastructure.Migrations
{
    public partial class OpinionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Opinion_Id",
                table: "OrderProducts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Opinions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    OrderProduct_Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Grade_Stars = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opinions_OrderProducts_OrderProduct_Id",
                        column: x => x.OrderProduct_Id,
                        principalTable: "OrderProducts",
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
                name: "IX_Opinions_OrderProduct_Id",
                table: "Opinions",
                column: "OrderProduct_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Opinions_Opinion_Id",
                table: "OrderProducts",
                column: "Opinion_Id",
                principalTable: "Opinions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Opinions_Opinion_Id",
                table: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Opinions");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_Opinion_Id",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Opinion_Id",
                table: "OrderProducts");
        }
    }
}
