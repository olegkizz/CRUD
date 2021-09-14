using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityNLayer.DAL.EF.Migrations
{
    public partial class FirstDAta : Migration
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
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Topics_Topics_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    { "6af2d20b-b113-45d0-9e82-66648006fb5c", "c767bc93-a07c-475f-9fd0-649b0897029e", "Teacher", "TEACHER" },
                    { "62df2641-3ce0-4b61-b5eb-5a64c0bc465e", "0df4f9b8-021a-4966-8321-96931c7d2472", "Manager", "MANAGER" },
                    { "1a850231-a336-4407-b177-ca71d4267e33", "460b7740-f546-40f7-823f-6a23670fc331", "Admin", "ADMIN" },
                    { "b3cdb649-ed9c-40f0-b96a-810a034b1d1f", "8cbdea32-6d11-4a7a-afb3-1758b0a1c3d1", "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0b90a86b-949f-4856-8891-9b45b0bc52e9", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "318c075b-98d8-497b-b3f1-c669ffa35315", "guest19@mail.com", true, "guest19", "standart", false, null, null, "guest19@MAIL.COM", "AQAAAAEAACcQAAAAEGFaY5xB0zvSKHE9kMy8OeebDeIbwKllD7h0oQ3LBeMk7gsSAbxOnkLZgYSwwXqHdg==", null, false, "5f39b29d-3fe7-47e8-b6e5-a3365d23717c", false, "guest19@mail.com" },
                    { "a54b8ee7-72ff-402e-a3ba-86a2cead1c19", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "e75ab222-e813-4d8b-83b9-d5973b9a73c0", "guest18@mail.com", true, "guest18", "standart", false, null, null, "guest18@MAIL.COM", "AQAAAAEAACcQAAAAEMKsG5WHBup5iFNiGpe5cwvmiVPNSyj7yWgVTaq6yhXeQ2Qk2AKJBpLazVCxPpQ0Cw==", null, false, "97f4ea42-b174-4ade-9f33-7e6ac2f458d4", false, "guest18@mail.com" },
                    { "cc7ff695-c0b4-4872-af30-b996c95d4211", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ea0680f3-d8b9-4df8-b48f-af1b8d4bce1f", "guest17@mail.com", true, "guest17", "standart", false, null, null, "guest17@MAIL.COM", "AQAAAAEAACcQAAAAEJZu39DUnDE/49m5R0vcJp2NduobHhDVw8OJXmkrasdXARYVinKI5cE+/uPu9RlPUA==", null, false, "7e50e501-ddbc-4057-a207-90bb4e689ee8", false, "guest17@mail.com" },
                    { "e2f3f11d-530f-4048-8b02-9863739f2026", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "40f073fd-b461-4679-96b4-39af2fcf1560", "guest16@mail.com", true, "guest16", "standart", false, null, null, "guest16@MAIL.COM", "AQAAAAEAACcQAAAAEHnGKagbH6zhLuSBqC2VdpSLUfTBEjlX8cljPeerALny9kVGmspNiucfhVgGPG2C1Q==", null, false, "5d314997-aad2-447c-845c-331734cb27e4", false, "guest16@mail.com" },
                    { "02625fde-e353-435a-981f-72ddeffd9fb0", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "3b84e622-1ad1-41f9-b0da-4f2d2651d9af", "guest15@mail.com", true, "guest15", "standart", false, null, null, "guest15@MAIL.COM", "AQAAAAEAACcQAAAAEGS6M63KBjIyLxn05QXTjOY/avLqNf30Z8S7XhBlN2570lafDW5TZQdmZnaUqyLosQ==", null, false, "df58a24f-b2c5-484f-b759-a45524e8d5ee", false, "guest15@mail.com" },
                    { "7c5bdad2-446e-4687-8a18-f343300357b4", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "b36bb167-cf0f-457b-8e86-fa30a3582585", "guest14@mail.com", true, "guest14", "standart", false, null, null, "guest14@MAIL.COM", "AQAAAAEAACcQAAAAEMknqWG0FXZVU2eLDKmfXMHaPeT9IExxFugd1br1JWdRtKDkzBH/YV7QNW3sfb5Qmw==", null, false, "c36190db-c783-4ccc-b9af-ce9108643798", false, "guest14@mail.com" },
                    { "31ef740c-707c-4535-b4cd-17a86339b286", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "469299c0-83e6-4dc6-bbee-2f0e35b8f3a6", "guest13@mail.com", true, "guest13", "standart", false, null, null, "guest13@MAIL.COM", "AQAAAAEAACcQAAAAECTIfiIKuAQ9fyQuU7qbQN3ttaHdSulUzRbuV0ApugL6opUkRchBPvaaZmzpILKhPg==", null, false, "83895968-a495-4d5c-93c3-04fe0d7bfd61", false, "guest13@mail.com" },
                    { "207284bc-8116-44ef-8936-332dd0809840", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "2e05f87a-a493-49e1-bdf9-08254595c8ce", "guest12@mail.com", true, "guest12", "standart", false, null, null, "guest12@MAIL.COM", "AQAAAAEAACcQAAAAEOqzJHJpYVvZ/DyI8yFgr1/wAjx1I5yhM9vrBPSsWRk7ZnG2AU7l5JjdoPIUW+qlfQ==", null, false, "f8687968-06b1-4969-859a-b43496a66758", false, "guest12@mail.com" },
                    { "86980d47-6227-4445-8f59-660b397b0137", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "909efd25-2a95-479a-9ef8-8fade5596432", "guest11@mail.com", true, "guest11", "standart", false, null, null, "guest11@MAIL.COM", "AQAAAAEAACcQAAAAEKWF1JQJZws8RJw9DslFMvrMOJVBYNc8JsFbQ5x+PqqgsaPdxQdsuLI/CvCNZtZzlg==", null, false, "769c1b09-d79b-4895-88e7-576a9b41dc08", false, "guest11@mail.com" },
                    { "199c09ef-9668-4015-83b5-3d44d4de8c1a", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "f692a009-ed00-41b9-b005-3d3816505387", "guest10@mail.com", true, "guest10", "standart", false, null, null, "guest10@MAIL.COM", "AQAAAAEAACcQAAAAEMu2OaFyaophzjqEhxFlWRsgDqBNUZgOX0FWGE2VQxlTDGkoWLgcgMsnmnwMeCWQfg==", null, false, "0fa875a4-93fc-4ba9-a129-59fad3b3830a", false, "guest10@mail.com" },
                    { "5614966d-8a8e-4224-9893-5e9c81c07bda", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "83c2cbdd-cff8-4b6f-8a46-07ac0117ae04", "guest9@mail.com", true, "guest9", "standart", false, null, null, "guest9@MAIL.COM", "AQAAAAEAACcQAAAAEB8jY6IuaWVYP06I3SpB3s/s4Db5pTniQIUanIgdLhvWsUzKLMO7ineGntisgv1dvA==", null, false, "cdb9a2e1-871e-47a1-a4ea-672a193711b8", false, "guest9@mail.com" },
                    { "de75d06d-ec68-4ea9-979a-decac518fcf6", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "50bcc35e-ea9c-4cb8-bf6a-f5b691abad14", "guest7@mail.com", true, "guest7", "standart", false, null, null, "guest7@MAIL.COM", "AQAAAAEAACcQAAAAEFn28mCRYs/reXL882t1Nu7gZaOMtX5FWoSvCrDoRHBqurQklZm07EzhNkMs6Z1tdw==", null, false, "6a942cda-f6b7-4754-91ca-2b313b6ff3bc", false, "guest7@mail.com" },
                    { "c68bca45-322a-4695-b2b9-88440973b06b", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "e53e528d-8545-450c-964e-b754e808e868", "guest6@mail.com", true, "guest6", "standart", false, null, null, "guest6@MAIL.COM", "AQAAAAEAACcQAAAAEISak01JKom2hBYDNJK6g51Etc+Am6PatSUaJLoiazCY9XH3rrcrzTPyV8RA9YaIXg==", null, false, "f4883e03-1bef-430b-aab9-06cc73d8d806", false, "guest6@mail.com" },
                    { "2d4c882a-0c18-46ef-a96e-0e0cb3746f97", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "73320288-4018-4322-b0d6-e2e89ad3a2bb", "guest5@mail.com", true, "guest5", "standart", false, null, null, "guest5@MAIL.COM", "AQAAAAEAACcQAAAAELquhm158/ctbwb7aZQo4QiP+TMRmGp2E8LS4jYwd9Y3iYfu73B8twq+mRZ5FivD1Q==", null, false, "39174241-9205-4ca2-b18b-22dd063ae40b", false, "guest5@mail.com" },
                    { "fe808efa-47ba-4806-83bf-f3d985fea494", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ef3e9481-2b79-4093-852e-166856677e8d", "guest4@mail.com", true, "guest4", "standart", false, null, null, "guest4@MAIL.COM", "AQAAAAEAACcQAAAAEAxOJc75lArnZvY2lLHrD3WCoBxu5mWuLat+1Wu7UCTF+axPa8fCzLh+LYhGVP0Ttw==", null, false, "86ec7e53-842f-4cd7-a17e-6eef33d2a632", false, "guest4@mail.com" },
                    { "c67e1c37-6ce2-4d48-a148-f74cd81608cf", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "05105bc1-b448-439d-b728-1bb7348bc51d", "guest3@mail.com", true, "guest3", "standart", false, null, null, "guest3@MAIL.COM", "AQAAAAEAACcQAAAAEBFyotxUx42XrDPQxIVxP89ykaU1PQK7DhjUJI7zwU2TOlxdRSXlxvYOGtzRqPZSOQ==", null, false, "16fc26f5-cbe9-47a8-a6b8-c9483fa8541d", false, "guest3@mail.com" },
                    { "66d2a5e5-7fab-48a3-a2e4-dd7792d5e343", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "d28fcb3b-8e94-4f52-82e2-05882aee10e2", "guest2@mail.com", true, "guest2", "standart", false, null, null, "guest2@MAIL.COM", "AQAAAAEAACcQAAAAEChFwme4p40/DBMK9ZBvSmGm4/VHIIOeUYpUTGc1TiuhFF5eHIpR7N0nKBfEdvP+1w==", null, false, "822da6e7-51e0-4fbb-9a58-1de96b89bfe4", false, "guest2@mail.com" },
                    { "b7895ba8-0f90-4b2e-a3de-e17a6f854e0c", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "64e6ca33-42fc-4829-bcf2-d02923a4ae3f", "guest1@mail.com", true, "guest1", "standart", false, null, null, "guest1@MAIL.COM", "AQAAAAEAACcQAAAAENus2AxYlPmlg0XdRnPCocROUcgzmyMsOA7qIFhHK/dkHPVHBR+dHO1CMvRdc9aylw==", null, false, "6c5cec04-28fd-4e5b-82de-c0328125545c", false, "guest1@mail.com" },
                    { "70809927-0934-4046-9227-8fe693cf581d", 0, new DateTime(2000, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "2cb84fa6-1a15-469b-8298-c698367afaa9", "manager@manager.com", true, "manager", "manager", false, null, null, "MANAGER@MANAGER.COM", "AQAAAAEAACcQAAAAEIfL5ygFB52YYIiicK8yq4/hk0KPmVeRWYGI5j+cQnKfM3jwvmEwVsF9h3LMPLMdeg==", null, false, "96c1de80-5b9f-4738-987d-05d9aa70bb89", false, "manager@manager.com" },
                    { "77dbb15b-cb84-4105-bd60-1412e6105e9b", 0, new DateTime(1998, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "2286069a-52d5-4dbf-962b-5e0f33485e08", "admin@admin.com", true, "admin", "admin", false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEJVQz++pv6e8HT81az6+HLMr7NuyGywFwczW1DmQEoYYE3ywRmZIz/w/MYfqWmx2gQ==", null, false, "8207f357-3df7-4b3b-8ca7-815e574cd555", false, "admin@admin.com" },
                    { "058956ce-4182-4568-927b-f02dd0d52b76", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "8d99ac6b-dd4e-4b61-942d-b6789e68f668", "guest8@mail.com", true, "guest8", "standart", false, null, null, "guest8@MAIL.COM", "AQAAAAEAACcQAAAAEGXD//VzYDIaaZjD9S7PeVJWpbpNI094HA7b4Edey27AqeCBA6XXq0xy99uKqWOH1A==", null, false, "81e87c7f-6eee-4421-8fa6-3cc222b150e1", false, "guest8@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Program", "StartDate", "Title", "TopicId" },
                values: new object[,]
                {
                    { 3, "Super JavaScript", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JavaScript", 0 },
                    { 2, "Super Spring", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Java", 0 },
                    { 1, "Super MVC", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP", 0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1a850231-a336-4407-b177-ca71d4267e33", "77dbb15b-cb84-4105-bd60-1412e6105e9b" },
                    { "62df2641-3ce0-4b61-b5eb-5a64c0bc465e", "70809927-0934-4046-9227-8fe693cf581d" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "CourseId", "Description", "ParentId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Super MVC", null, ".NET" },
                    { 2, 2, "Super Spring", null, "Spring" },
                    { 3, 3, "Super ReactJS", null, "ReactJS" },
                    { 4, 3, "Super AngularJS", null, "AngularJS" },
                    { 5, 3, "Super NodeJS", null, "NodeJS" }
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
                name: "IX_Topics_CourseId",
                table: "Topics",
                column: "CourseId");

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
                name: "Topics");

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
                name: "AspNetUsers");
        }
    }
}
