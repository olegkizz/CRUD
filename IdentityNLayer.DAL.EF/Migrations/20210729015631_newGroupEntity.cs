using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityNLayer.DAL.EF.Migrations
{
    public partial class newGroupEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Status", "TeacherId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2,
                column: "TeacherId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TeacherId",
                table: "Groups",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Teachers_TeacherId",
                table: "Groups",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Teachers_TeacherId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_TeacherId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Groups");

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 0);
        }
    }
}
