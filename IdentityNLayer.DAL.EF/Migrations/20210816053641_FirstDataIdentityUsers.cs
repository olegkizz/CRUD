using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityNLayer.DAL.EF.Migrations
{
    public partial class FirstDataIdentityUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topic_Topic_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkToProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Program = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groups_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentToGroupActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentToGroupActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentToGroupActions_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentToGroupActions_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f2b1c587-d7bd-47e5-85a9-f2d175ac6051", "ba87353f-f873-451f-acc8-ea00352e1943", "Admin", "ADMIN" },
                    { "b38d5417-0aa2-413a-8604-49ac98c3ab37", "2c67aa4e-1eb9-4464-a818-a072057082b8", "Manager", "MANAGER" },
                    { "0f7353b9-c3fd-45a6-b129-549c2415c361", "555bb501-7ba0-425c-84a5-ded7905c6574", "Student", "STUDENT" },
                    { "00559986-a345-4592-95f2-0e23f87b6346", "fdb9a83c-2fb4-410c-a287-7fbf683bb4ce", "Teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "d5a30392-28d4-479d-ae75-a69b8da97a40", 0, "ca7639d3-b284-4bae-a386-3b70bc7c744a", "admin@admin.com", true, false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEJsN6sWDpTHsPmbX3oqg0BTgsa6wIbaoGXcYyKTGU/85CQ9lY/gZUZoceTXRjRaNFA==", null, false, "cd9d8280-7457-4ac6-8fc6-3f5b94a0cb84", false, "admin@admin.com" },
                    { "3e495ce3-b6c3-42df-b17d-9d664fd4a948", 0, "f8a15de2-deac-4382-a4f4-30ece9e65b2d", "manager@manager.com", true, false, null, null, "MANAGER@MANAGER.COM", "AQAAAAEAACcQAAAAEMqpmoFs5MlJsE8I33efXQecqBPxiMC9xM30w51DnszxhEgry8C+tPQpx6rc1CXkog==", null, false, "641dc9c3-d4a7-4a78-b9cc-5adec9081b25", false, "manager@manager.com" },
                    { "92659287-e6cd-47b7-ab8e-f6461e2a4824", 0, "163f989e-68e7-47ea-93d5-d0c440615fa7", "studentfirst@mail.com", true, false, null, null, "STUDENTFIRST@MAIL.COM", "AQAAAAEAACcQAAAAEEK08/oJUgIgGbiqqHBqa1NdRq+mq4HPT/68YpD3KCzDXTg71KYrFCCvxRGOOd0yCg==", null, false, "4447dd63-7721-4f9b-b5aa-8f65b1bf6da6", false, "studentfirst@mail.com" },
                    { "f50662f8-e597-4da1-91e5-147401352687", 0, "716e2015-44ed-4816-b248-58fb44cbcaee", "studentsecond@mail.com", true, false, null, null, "STUDENTSECOND@MAIL.COM", "AQAAAAEAACcQAAAAEEz+LLj4Vr21ZfLdZgArQIIp6iVh0K2LcJ+fDgP1rRBEbemev3DXZZAEnj7/dOwYww==", null, false, "064a041e-a808-489f-9fc5-da15d71e4080", false, "studentsecond@mail.com" },
                    { "f75d5d50-eb31-41db-a856-9eb9b3ce04df", 0, "5b3f3987-accd-4c6f-920a-f98828428f48", "studentthird@mail.com", true, false, null, null, "STUDENTTHIRD@MAIL.COM", "AQAAAAEAACcQAAAAEBUPWUd8bvPvV9fnHaOBM6JlMd4YBHgA3C1PGZ+/Kxe2t3Ar47dffEDFNVFbRT1nNQ==", null, false, "8791a778-ddaa-4945-8eed-18298d97cca5", false, "studentthird@mail.com" },
                    { "dfe55b49-e24f-4eca-af56-7fba99e6ff7c", 0, "6d911354-41c1-490e-a767-738498eb6502", "teacherfirst@mail.com", true, false, null, null, "TEACHERFIRST@MAIL.COM", "AQAAAAEAACcQAAAAEEfly1OvDf3lSpBYC0PxyKxcDYoLcLkNl6WecCg6vqRASJgbepAHIaPkTlS3d4tAOg==", null, false, "5687c673-6982-43e9-9e56-f21d20192326", false, "teacherfirst@mail.com" },
                    { "a703ccc6-6d55-4842-abef-e4346f03b64e", 0, "9705292a-b1db-469f-af5e-2348eb4e138b", "teachersecond@mail.com", true, false, null, null, "TEACHERSECOND@MAIL.COM", "AQAAAAEAACcQAAAAEHiGJ/ZEWsS4l+3hvMRS3qyzvy+iGpS3ia3k2q/JlSy/0qKOengtVmRao2cCWx1g1A==", null, false, "5c5a5cdb-bc68-472a-9e4a-8fdc0ac67944", false, "teachersecond@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Topic",
                columns: new[] { "Id", "Description", "ParentId", "Title" },
                values: new object[,]
                {
                    { 1, "Super MVC", null, ".NET" },
                    { 2, "Super Spring", null, "Spring" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "f2b1c587-d7bd-47e5-85a9-f2d175ac6051", "d5a30392-28d4-479d-ae75-a69b8da97a40" },
                    { "b38d5417-0aa2-413a-8604-49ac98c3ab37", "3e495ce3-b6c3-42df-b17d-9d664fd4a948" },
                    { "0f7353b9-c3fd-45a6-b129-549c2415c361", "92659287-e6cd-47b7-ab8e-f6461e2a4824" },
                    { "0f7353b9-c3fd-45a6-b129-549c2415c361", "f50662f8-e597-4da1-91e5-147401352687" },
                    { "0f7353b9-c3fd-45a6-b129-549c2415c361", "f75d5d50-eb31-41db-a856-9eb9b3ce04df" },
                    { "00559986-a345-4592-95f2-0e23f87b6346", "dfe55b49-e24f-4eca-af56-7fba99e6ff7c" },
                    { "00559986-a345-4592-95f2-0e23f87b6346", "a703ccc6-6d55-4842-abef-e4346f03b64e" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Created", "Description", "Program", "StartDate", "Title", "TopicId" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 8, 16, 8, 36, 39, 286, DateTimeKind.Local).AddTicks(2830), "Super MVC", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP", 1 },
                    { 2, new DateTime(2021, 8, 16, 8, 36, 39, 290, DateTimeKind.Local).AddTicks(787), "Super Spring", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Java", 2 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "FirstName", "LastName", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2005, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oleg", "Kizz", 0, "92659287-e6cd-47b7-ab8e-f6461e2a4824" },
                    { 2, new DateTime(2006, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vova", "Braslav", 1, "f50662f8-e597-4da1-91e5-147401352687" },
                    { 3, new DateTime(2005, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nikita", "Chebur", 2, "f75d5d50-eb31-41db-a856-9eb9b3ce04df" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Bio", "BirthDate", "FirstName", "LastName", "LinkToProfile", "UserId" },
                values: new object[,]
                {
                    { 1, "Super Teacher", new DateTime(1985, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teach", "First", null, "dfe55b49-e24f-4eca-af56-7fba99e6ff7c" },
                    { 2, "Super Teacher", new DateTime(1992, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teach", "Second", null, "a703ccc6-6d55-4842-abef-e4346f03b64e" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CourseId", "Number", "StartDate", "Status", "TeacherId" },
                values: new object[] { 1, 1, "Nemiga-1", new DateTime(2021, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CourseId", "Number", "StartDate", "Status", "TeacherId" },
                values: new object[] { 2, 2, "Nemiga-2", new DateTime(2021, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 2 });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "GroupID", "Role", "State", "Updated", "UserID" },
                values: new object[,]
                {
                    { 1, 1, 0, 0, new DateTime(2021, 8, 16, 8, 36, 41, 166, DateTimeKind.Local).AddTicks(8683), "92659287-e6cd-47b7-ab8e-f6461e2a4824" },
                    { 4, 1, 0, 0, new DateTime(2021, 8, 16, 8, 36, 41, 166, DateTimeKind.Local).AddTicks(9340), "f50662f8-e597-4da1-91e5-147401352687" },
                    { 2, 2, 0, 0, new DateTime(2021, 8, 16, 8, 36, 41, 166, DateTimeKind.Local).AddTicks(8962), "92659287-e6cd-47b7-ab8e-f6461e2a4824" },
                    { 3, 2, 0, 0, new DateTime(2021, 8, 16, 8, 36, 41, 166, DateTimeKind.Local).AddTicks(9155), "f50662f8-e597-4da1-91e5-147401352687" },
                    { 5, 2, 0, 0, new DateTime(2021, 8, 16, 8, 36, 41, 166, DateTimeKind.Local).AddTicks(9516), "f75d5d50-eb31-41db-a856-9eb9b3ce04df" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TopicId",
                table: "Courses",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_GroupID",
                table: "Enrollments",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserID",
                table: "Enrollments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CourseId",
                table: "Groups",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TeacherId",
                table: "Groups",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentToGroupActions_GroupId",
                table: "StudentToGroupActions",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentToGroupActions_StudentId",
                table: "StudentToGroupActions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserId",
                table: "Teachers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_ParentId",
                table: "Topic",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "StudentToGroupActions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
