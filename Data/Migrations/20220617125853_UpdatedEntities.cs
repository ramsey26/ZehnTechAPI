using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class UpdatedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInformation_Products_ProductId",
                table: "ProductInformation");

            migrationBuilder.DropIndex(
                name: "IX_ProductInformation_ProductId",
                table: "ProductInformation");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "IsActive");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductInformation",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductInformation_ProductId",
                table: "ProductInformation",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInformation_Products_ProductId",
                table: "ProductInformation",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInformation_Products_ProductId",
                table: "ProductInformation");

            migrationBuilder.DropIndex(
                name: "IX_ProductInformation_ProductId",
                table: "ProductInformation");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductInformation",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInformation_ProductId",
                table: "ProductInformation",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInformation_Products_ProductId",
                table: "ProductInformation",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
