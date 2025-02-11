using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class WebApiDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_InvoiceHeaders_InvoiceId",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "InvoiceHeaders");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "InvoiceHeaders",
                newName: "InvoiceHId");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "InvoiceDetails",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetails_InvoiceId",
                table: "InvoiceDetails",
                newName: "IX_InvoiceDetails_ProductID");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateAndTime",
                table: "InvoiceHeaders",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceName",
                table: "InvoiceHeaders",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "InvoiceHeaders",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitPrice",
                table: "InvoiceDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "InvoiceDetails",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceHId",
                table: "InvoiceDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<int>(type: "int", nullable: false),
                    Productquantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSlot = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceHId",
                table: "InvoiceDetails",
                column: "InvoiceHId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_InvoiceHeaders_InvoiceHId",
                table: "InvoiceDetails",
                column: "InvoiceHId",
                principalTable: "InvoiceHeaders",
                principalColumn: "InvoiceHId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Products_ProductID",
                table: "InvoiceDetails",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_InvoiceHeaders_InvoiceHId",
                table: "InvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Products_ProductID",
                table: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetails_InvoiceHId",
                table: "InvoiceDetails");

            migrationBuilder.DropColumn(
                name: "CreateDateAndTime",
                table: "InvoiceHeaders");

            migrationBuilder.DropColumn(
                name: "InvoiceName",
                table: "InvoiceHeaders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InvoiceHeaders");

            migrationBuilder.DropColumn(
                name: "InvoiceHId",
                table: "InvoiceDetails");

            migrationBuilder.RenameColumn(
                name: "InvoiceHId",
                table: "InvoiceHeaders",
                newName: "InvoiceId");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "InvoiceDetails",
                newName: "InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetails_ProductID",
                table: "InvoiceDetails",
                newName: "IX_InvoiceDetails_InvoiceId");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "InvoiceHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "InvoiceDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "InvoiceDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_InvoiceHeaders_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId",
                principalTable: "InvoiceHeaders",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
