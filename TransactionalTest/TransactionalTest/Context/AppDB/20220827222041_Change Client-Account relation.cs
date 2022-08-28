using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionalTest.Context.AppDB
{
    public partial class ChangeClientAccountrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Person_clientId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_clientId",
                table: "Account");

            migrationBuilder.AddColumn<string>(
                name: "ClientIdentification",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientIdentification",
                table: "Account");

            migrationBuilder.CreateIndex(
                name: "IX_Account_clientId",
                table: "Account",
                column: "clientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Person_clientId",
                table: "Account",
                column: "clientId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
