using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityNLayer.DAL.EF.Migrations
{
    public partial class addingTeacher_ChangeStudentGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Groups",
                newName: "Number");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareerStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                column: "Number",
                value: "Nemiga-1");

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2,
                column: "Number",
                value: "Nemiga-2");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Type" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Type" },
                values: new object[] { null, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Groups",
                newName: "OwnerId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "OwnerId" },
                values: new object[] { "FirstGroup", "420e09c7-4d2a-4f0e-b780-27055dc90111" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "OwnerId" },
                values: new object[] { "SecondGroup", "8379999c-7a8a-457e-925e-4cde670c251d" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "City", "Email", "State", "Zip" },
                values: new object[] { "First", "Moscow", "olegkizz@mail.com", "Russia", "213" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "City", "Email", "State", "Zip" },
                values: new object[] { "Fist", "Mocow", "olegkzz@mail.com", "Russia", "23" });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Address", "City", "Email", "State", "Zip" },
                values: new object[] { "First", "Moscow", "olegkizz@mail.com", "Belarus", "213" });
        }
    }
}
