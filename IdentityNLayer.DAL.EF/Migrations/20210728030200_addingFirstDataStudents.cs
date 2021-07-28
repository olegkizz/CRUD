using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityNLayer.DAL.EF.Migrations
{
    public partial class addingFirstDataStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name", "OwnerId" },
                values: new object[] { 1, "FirstGroup", "420e09c7-4d2a-4f0e-b780-27055dc90111" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name", "OwnerId" },
                values: new object[] { 2, "SecondGroup", "8379999c-7a8a-457e-925e-4cde670c251d" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "City", "Email", "GroupId", "Name", "State", "Zip" },
                values: new object[] { 1, "First", "Moscow", "olegkizz@mail.com", 1, "Oleg", "Russia", "213" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "City", "Email", "GroupId", "Name", "State", "Zip" },
                values: new object[] { 2, "Fist", "Mocow", "olegkzz@mail.com", 1, "Vova", "Russia", "23" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "City", "Email", "GroupId", "Name", "State", "Zip" },
                values: new object[] { 3, "First", "Moscow", "olegkizz@mail.com", 2, "Nikita", "Belarus", "213" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
