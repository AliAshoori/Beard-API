using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WildBeard.Orders.InfraServices.Migrations
{
    public partial class Configured_CascadeDelete_OrderDeliveryAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderBillingAddresses_OrderBillingAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderDeliveryAddresses_OrderDeliveryAddressId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderBillingAddressId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderDeliveryAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderBillingAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderDeliveryAddressId",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "OrderDeliveryAddresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "OrderBillingAddresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveryAddresses_OrderId",
                table: "OrderDeliveryAddresses",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderBillingAddresses_OrderId",
                table: "OrderBillingAddresses",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBillingAddresses_Orders_OrderId",
                table: "OrderBillingAddresses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDeliveryAddresses_Orders_OrderId",
                table: "OrderDeliveryAddresses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderBillingAddresses_Orders_OrderId",
                table: "OrderBillingAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDeliveryAddresses_Orders_OrderId",
                table: "OrderDeliveryAddresses");

            migrationBuilder.DropIndex(
                name: "IX_OrderDeliveryAddresses_OrderId",
                table: "OrderDeliveryAddresses");

            migrationBuilder.DropIndex(
                name: "IX_OrderBillingAddresses_OrderId",
                table: "OrderBillingAddresses");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderDeliveryAddresses");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderBillingAddresses");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderBillingAddressId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderDeliveryAddressId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderBillingAddressId",
                table: "Orders",
                column: "OrderBillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderDeliveryAddressId",
                table: "Orders",
                column: "OrderDeliveryAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderBillingAddresses_OrderBillingAddressId",
                table: "Orders",
                column: "OrderBillingAddressId",
                principalTable: "OrderBillingAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderDeliveryAddresses_OrderDeliveryAddressId",
                table: "Orders",
                column: "OrderDeliveryAddressId",
                principalTable: "OrderDeliveryAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
