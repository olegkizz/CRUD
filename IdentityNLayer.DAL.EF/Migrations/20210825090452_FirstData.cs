using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityNLayer.DAL.EF.Migrations
{
    public partial class FirstData : Migration
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
                name: "Topics",
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
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Topics_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Topics",
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
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
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
                    TeacherId = table.Column<int>(type: "int", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EntityID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_Enrollments_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    { "7b487486-9a02-4c2b-bd75-744bb38d0ea1", "7b2de314-c4e1-4733-95c7-d382c77c5463", "Admin", "ADMIN" },
                    { "b1b124db-2749-4f5b-aff5-7de8d576bab8", "4c6940b3-413b-4235-ac13-47b2338824e4", "Manager", "MANAGER" },
                    { "20787328-044d-495f-9aa4-ea4983bf795f", "95a4af52-262e-4e93-b8a9-e775ddb62083", "Student", "STUDENT" },
                    { "f1c6bcea-bee6-4da5-a011-d48b5a7d17ca", "516ab371-902f-4fb4-9bd4-a479310ea1e9", "Teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "03c25264-ebad-486b-b67b-8ea32106fe1c", 0, "6829fe48-4002-4601-a383-bfc2c88af8e8", "admin@admin.com", true, false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEG6IBFEVjW2yw6FQ0lzo0NREeypiUoTyWdACKL6wRX+C0K6EMSVUcuFAvWqFDq49/Q==", null, false, "303368bb-ada2-44bf-874f-a60b2b0ebb1f", false, "admin@admin.com" },
                    { "ff45eeed-a12f-4611-9459-da96021468ab", 0, "c58283de-a1b4-4740-adac-337a321a53ed", "manager@manager.com", true, false, null, null, "MANAGER@MANAGER.COM", "AQAAAAEAACcQAAAAEADqikUrlzn/YGhQ9PSAp3474woRi6yflu2xlwCnNrlYkDgk9fsyBZM3h0EZjRNbTQ==", null, false, "ba2385af-2b59-4738-b18d-e6136338f3c6", false, "manager@manager.com" },
                    { "c9fcee5b-40fa-4f54-bb53-b71a28477968", 0, "ecc7ba6f-7a65-47d5-9e92-9643c6815edc", "studentfirst@mail.com", true, false, null, null, "STUDENTFIRST@MAIL.COM", "AQAAAAEAACcQAAAAELXuQGCbaO3IHUfmgjMU6r/aDjOU8FdfitGeKbT4ib7sN4t4HQILwb1f/Puec3VqKw==", null, false, "036ae482-1cff-45fd-bfac-633c9bce20b9", false, "studentfirst@mail.com" },
                    { "003048f9-f13d-4b4f-90b0-f5c16af3683d", 0, "6481ed4a-e648-455d-b12b-34ece3a594b3", "studentsecond@mail.com", true, false, null, null, "STUDENTSECOND@MAIL.COM", "AQAAAAEAACcQAAAAEJ5T7dVbjrNoUSGliqmejRv0Ve+91oFydsxom30ttZU13gjFIQL4EoottWMdZWxESg==", null, false, "e967fc90-febb-4063-b669-6f8fe160d932", false, "studentsecond@mail.com" },
                    { "70b88c20-e3fd-40c2-9d78-0c9cb23ef243", 0, "edb5ca87-c810-41c3-9086-e183e192702e", "studentthird@mail.com", true, false, null, null, "STUDENTTHIRD@MAIL.COM", "AQAAAAEAACcQAAAAEIGekxaJ5ofp/St1ZPPDMWzmtFE+xg3RFq1l8wEf2+C7wn3LA9Ng5MGLIFJbipvCpw==", null, false, "260dbbc5-caf7-464c-adbc-ed020bd8bd18", false, "studentthird@mail.com" },
                    { "61f255f2-f23b-4298-8920-6fb8954acd1e", 0, "075bb0dc-9ee4-4f37-b12c-6aadc3f8ecc3", "teacherfirst@mail.com", true, false, null, null, "TEACHERFIRST@MAIL.COM", "AQAAAAEAACcQAAAAEFwkJGgi/WFG5vt1nlqx2/ubWIr2afv0rQliBXtbeNbXq0lnYtWZCapaIsGMk9oh/w==", null, false, "9f31c5f2-fc99-4883-a7f1-77164ea75cfc", false, "teacherfirst@mail.com" },
                    { "9318e043-d2dc-40b3-bb80-2575e00285d1", 0, "8db583f1-9564-4ad2-8c92-99af2ccc05d5", "teachersecond@mail.com", true, false, null, null, "TEACHERSECOND@MAIL.COM", "AQAAAAEAACcQAAAAELlkHb0z/uduEtLwUER5BZupLO5rK8VqcPVICsJTbpntMHS5JBQQZ72m6nOma+BEtw==", null, false, "ef8b5c91-8d06-48fa-9392-ccff2449e9f3", false, "teachersecond@mail.com" },
                    { "0dc8eca7-57b8-4cfe-9678-1d735982c75e", 0, "dd3e93d1-8cc4-49db-b270-8cec245c9bca", "studentfourth@mail.com", true, false, null, null, "STUDENTFOURTH@MAIL.COM", "AQAAAAEAACcQAAAAEEaYTVwvWNbc3iTAqPs+Sn6Ik3RbSzSgUUdL3a7yoPdt1SywOaV/JVKvv3vg2vx4uw==", null, false, "e95588cc-9666-4965-93ca-7b14e44fe272", false, "studentfourth@mail.com" },
                    { "5be4fdab-8b48-448e-a518-d3dd6b83817a", 0, "14733803-2f5d-4e05-bc81-1ffae4e662d5", "studentfifth@mail.com", true, false, null, null, "STUDENTFIFTH@MAIL.COM", "AQAAAAEAACcQAAAAECJYL5ts/PbUTZYzcQzBwabwFZ9QFPDXwyvFmWOEhcwuQuht59tlEhoHQd+1gwk9PA==", null, false, "a97146d8-944b-4d4d-9bb6-cda0f2fcf208", false, "studentfifth@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
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
                    { "20787328-044d-495f-9aa4-ea4983bf795f", "5be4fdab-8b48-448e-a518-d3dd6b83817a" },
                    { "20787328-044d-495f-9aa4-ea4983bf795f", "0dc8eca7-57b8-4cfe-9678-1d735982c75e" },
                    { "7b487486-9a02-4c2b-bd75-744bb38d0ea1", "03c25264-ebad-486b-b67b-8ea32106fe1c" },
                    { "b1b124db-2749-4f5b-aff5-7de8d576bab8", "ff45eeed-a12f-4611-9459-da96021468ab" },
                    { "f1c6bcea-bee6-4da5-a011-d48b5a7d17ca", "9318e043-d2dc-40b3-bb80-2575e00285d1" },
                    { "20787328-044d-495f-9aa4-ea4983bf795f", "c9fcee5b-40fa-4f54-bb53-b71a28477968" },
                    { "f1c6bcea-bee6-4da5-a011-d48b5a7d17ca", "61f255f2-f23b-4298-8920-6fb8954acd1e" },
                    { "20787328-044d-495f-9aa4-ea4983bf795f", "003048f9-f13d-4b4f-90b0-f5c16af3683d" },
                    { "20787328-044d-495f-9aa4-ea4983bf795f", "70b88c20-e3fd-40c2-9d78-0c9cb23ef243" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Program", "StartDate", "Title", "TopicId" },
                values: new object[,]
                {
                    { 1, "Super MVC", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP", 1 },
                    { 2, "Super Spring", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Java", 2 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "FirstName", "LastName", "Type", "UserId" },
                values: new object[,]
                {
                    { 2, new DateTime(2006, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vova", "Braslav", 1, "003048f9-f13d-4b4f-90b0-f5c16af3683d" },
                    { 1, new DateTime(2005, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oleg", "Kizz", 0, "c9fcee5b-40fa-4f54-bb53-b71a28477968" },
                    { 4, new DateTime(2005, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mikola", "Cool", 0, "0dc8eca7-57b8-4cfe-9678-1d735982c75e" },
                    { 5, new DateTime(2005, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vovka", "Sabur", 2, "5be4fdab-8b48-448e-a518-d3dd6b83817a" },
                    { 3, new DateTime(2005, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nikita", "Chebur", 2, "70b88c20-e3fd-40c2-9d78-0c9cb23ef243" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Bio", "BirthDate", "FirstName", "LastName", "LinkToProfile", "UserId" },
                values: new object[,]
                {
                    { 1, "Super Teacher", new DateTime(1985, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teach", "First", null, "61f255f2-f23b-4298-8920-6fb8954acd1e" },
                    { 2, "Super Teacher", new DateTime(1992, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teach", "Second", null, "9318e043-d2dc-40b3-bb80-2575e00285d1" }
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
                name: "IX_Enrollments_GroupId",
                table: "Enrollments",
                column: "GroupId");

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
                name: "IX_Topics_ParentId",
                table: "Topics",
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
                name: "Topics");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
