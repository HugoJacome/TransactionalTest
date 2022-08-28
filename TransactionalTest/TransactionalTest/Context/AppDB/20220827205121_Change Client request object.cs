using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionalTest.Context.AppDB
{
    public partial class ChangeClientrequestobject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MovementAccount",
                table: "Movements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovementAccount",
                table: "Movements");
        }
    }
}
