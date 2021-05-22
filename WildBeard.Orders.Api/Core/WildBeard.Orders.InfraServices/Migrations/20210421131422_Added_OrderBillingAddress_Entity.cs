using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WildBeard.Orders.InfraServices.Migrations
{
    public partial class Added_OrderBillingAddress_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderBillingAddressId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "OrderBillingAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBillingAddresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderBillingAddressId",
                table: "Orders",
                column: "OrderBillingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderBillingAddresses_OrderBillingAddressId",
                table: "Orders",
                column: "OrderBillingAddressId",
                principalTable: "OrderBillingAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderBillingAddresses_OrderBillingAddressId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderBillingAddresses");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderBillingAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderBillingAddressId",
                table: "Orders");
        }
    }
}
