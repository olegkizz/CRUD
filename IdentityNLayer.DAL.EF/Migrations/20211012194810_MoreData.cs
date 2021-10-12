using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityNLayer.DAL.EF.Migrations
{
    public partial class MoreData : Migration
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
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
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
                name: "Methodists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkToContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Methodists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Methodists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicId = table.Column<int>(type: "int", nullable: true),
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
                        onDelete: ReferentialAction.SetNull);
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
                    MethodistId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Groups_Methodists_MethodistId",
                        column: x => x.MethodistId,
                        principalTable: "Methodists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Groups_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lessons_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
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

            migrationBuilder.CreateTable(
                name: "GroupLessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupLessons_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupLessons_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentMarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonId = table.Column<int>(type: "int", nullable: true),
                    Mark = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentMarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentMarks_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentMarks_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentMarks_Students_StudentId",
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
                    { "9101c5bd-3b81-4689-9d33-90b46c11f80d", "b54c42db-3718-4def-902d-83ff11243c55", "Teacher", "TEACHER" },
                    { "15d18b58-a0a4-4f87-bce4-3c692370a9ab", "423e39ee-1cc5-4975-bd1c-e7edc659ec79", "Methodist", "METHODIST" },
                    { "fadb92af-6bec-461f-a769-a70bd30df2da", "648945d9-2387-43a4-a1f1-87e6cc31bf57", "Admin", "ADMIN" },
                    { "64d6ed5b-0d40-46eb-963a-fdda178d0a86", "259d782a-0c12-4e67-a6ef-33847ac386b9", "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a98703e2-1887-4b63-a4d3-042933f7c386", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "59afa278-9dfd-4acf-80d6-211025bcb16a", "guest24@mail.com", true, "guest24", "standart", false, null, null, "guest24@MAIL.COM", "AQAAAAEAACcQAAAAEBQA7tJ06kFbdOnlUXeXv0O4aNuMO6usRFtyx/o5Z74FrK7nW82QvbeSa4AK6CGhpA==", null, false, "b207572b-c423-43ed-b106-0e11133dee95", false, "guest24@mail.com" },
                    { "14e6b0c7-d2d6-4ca5-a5a5-179d18b42a21", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "8045500f-6208-4c44-91e0-756142c71440", "guest23@mail.com", true, "guest23", "standart", false, null, null, "guest23@MAIL.COM", "AQAAAAEAACcQAAAAEBaycZfdYfUlgwkvB6/7/MpfQr9Mio2RmXQjTeMPeNitpGW7OGNbQj4uF0EVzgh3qA==", null, false, "209e68de-f9f9-4aae-96ec-4e9faad3dec6", false, "guest23@mail.com" },
                    { "85a86443-1fcb-41f6-933c-a53139a70e84", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "3096c8af-c3af-4794-a3e8-fc262375bb7e", "guest22@mail.com", true, "guest22", "standart", false, null, null, "guest22@MAIL.COM", "AQAAAAEAACcQAAAAECUYyA86bSAzY4eMgv14Kds2czWPtqQsCfEeqhAWu2q532vY3caaOxdtg+vwVYx7BA==", null, false, "c1169582-fc8c-40de-8858-781f1aec82f5", false, "guest22@mail.com" },
                    { "60334305-6995-4261-b295-e2aa9668557d", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ea54735c-5b54-4636-a931-05fa4bb5907d", "guest21@mail.com", true, "guest21", "standart", false, null, null, "guest21@MAIL.COM", "AQAAAAEAACcQAAAAEAvWmQmhdoAiqblLed/48Fum3PRYyQSmeRPp/zK7DJR99s2A3VaeoLdrqxKtGqL9lg==", null, false, "3b17defb-32f0-4463-9665-973ee1e9b191", false, "guest21@mail.com" },
                    { "2bd2ce37-376f-4762-a78d-b1335ddfac35", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "af22e2a8-109a-4b98-ad70-2cd31db2098c", "guest20@mail.com", true, "guest20", "standart", false, null, null, "guest20@MAIL.COM", "AQAAAAEAACcQAAAAEDV+gdtNwWgLSqYImmBjb/s7C7eq8bbU6x5YMw3NNq+JL2EWUslWv3df6vl3SXjp2Q==", null, false, "1bbf864f-16ac-4a5f-b3bf-812dcde0e2dd", false, "guest20@mail.com" },
                    { "7611c080-1a64-4f9f-8dca-6c66646e08c4", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "2d4e2c56-07fd-43d0-a21c-7bf6d2ce095b", "guest19@mail.com", true, "guest19", "standart", false, null, null, "guest19@MAIL.COM", "AQAAAAEAACcQAAAAEDErfFL1XQdo+0eo9HiAC9kS3nnYo7RoldQEGdRIzxVd12jUlH/DCVo/eZMWJ6u/Fg==", null, false, "e928625d-3fef-4bb0-9ec6-bf64c84fd2b8", false, "guest19@mail.com" },
                    { "ca7db5b9-ef26-4480-9810-02258bda2904", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "f15592b5-0fe7-4f01-95cd-21ef5ef85398", "guest18@mail.com", true, "guest18", "standart", false, null, null, "guest18@MAIL.COM", "AQAAAAEAACcQAAAAEPoEQcl7kEznU7fqGDHTmI169JzfN6txqyfdV9mnsAS3mfiRmeDKm32lFJnN3nwq+w==", null, false, "1237281f-0cbc-4872-a570-d223ae60dcf2", false, "guest18@mail.com" },
                    { "af329e4e-1c95-45eb-af28-d51402030a87", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "8a064b43-0738-4952-9fef-b6ec77a186ef", "guest16@mail.com", true, "guest16", "standart", false, null, null, "guest16@MAIL.COM", "AQAAAAEAACcQAAAAEGsDpQiCuurkL70NTuEKF5BF8fmaMZOIO0C4zgO3W4bd+Jq/nGandFV+UFWc0flckg==", null, false, "87ef1da0-59d8-4cb0-90ff-3660ad774b38", false, "guest16@mail.com" },
                    { "548f64e8-e83a-4e5d-85a7-7c2c16a28274", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "11a3b22b-4e1c-44b0-bb5b-717a15af0603", "guest15@mail.com", true, "guest15", "standart", false, null, null, "guest15@MAIL.COM", "AQAAAAEAACcQAAAAEN1GEsNDHmcPzT12WDL2rJsyhcyAR/bPZIFlCDizE+wzZrzn+Y+RJrW+s4hg0m7ntw==", null, false, "48021715-21ac-4d26-a72b-e5a2257b5915", false, "guest15@mail.com" },
                    { "9bdd5c5f-c359-44cf-9236-87eea9b36913", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "7196520d-cc76-40bf-a437-7ebca82345e6", "guest14@mail.com", true, "guest14", "standart", false, null, null, "guest14@MAIL.COM", "AQAAAAEAACcQAAAAENzSOn9eqAo43Nt4FWT7t1FzYoiprosRZZbJaf6ZCUzkljDNEva/k8uZgjRF749Obw==", null, false, "ec37b6e5-75a9-4590-840d-18ec12affd3a", false, "guest14@mail.com" },
                    { "7bb4ec4f-e6db-4f97-a003-c117ee1b3c3e", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "18934eec-6425-46be-9bae-f4e58d66dcda", "guest17@mail.com", true, "guest17", "standart", false, null, null, "guest17@MAIL.COM", "AQAAAAEAACcQAAAAEIqnAEoBwSU9G3x2iIyLR1vzJ7hR8dXJeMbZsFpE3br4nAa+XaHCAnLsR7BY0eW6GA==", null, false, "6469d14d-707d-4f9a-8f9a-f4d11c6ce705", false, "guest17@mail.com" },
                    { "be35fe96-8b81-43ba-8f1d-d0917ea569a0", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "cc3bc7f0-25b0-464e-92f9-8c4a6b697c31", "guest12@mail.com", true, "guest12", "standart", false, null, null, "guest12@MAIL.COM", "AQAAAAEAACcQAAAAEC4j+f9hWEziBDfeWdBjPj5n6aIypoVzAGOwsEa9cXI3bOvdA5YxVAZHrxKNGR7iAw==", null, false, "87835736-e962-4eb7-b46c-c1682319911c", false, "guest12@mail.com" },
                    { "b303e61e-6b3a-48b4-a4fc-3175840921a7", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "736c05bb-4502-4794-82ff-5f18cd4b5584", "guest0@mail.com", true, "guest0", "standart", false, null, null, "guest0@MAIL.COM", "AQAAAAEAACcQAAAAEGO0eUe0sxTePf5kXLjXaNd1mcOiWg8D60WvbzJ0EVg6fY6HqPXPb4+dZr1A9zOHZQ==", null, false, "350318a5-90f8-4c8a-bd19-343790bf2afc", false, "guest0@mail.com" },
                    { "327ed16e-6857-4fbf-b548-ccb6ae875499", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "a1a5f3ce-dd16-44f2-a0c8-cfd0330ac924", "guest1@mail.com", true, "guest1", "standart", false, null, null, "guest1@MAIL.COM", "AQAAAAEAACcQAAAAEH2gHTnFU2oFXI9ZazmEIQuWP4ngJ9PnxrS4sXFVH8TwiQ3Q2FdVzJoe+zXegMVVEA==", null, false, "37225e0b-6d18-4165-8b6e-5bf188932e9f", false, "guest1@mail.com" },
                    { "2f85eb25-4314-441f-b48a-8252294691bf", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "5dd49f51-a2c9-46d5-884d-9316ad04343f", "guest13@mail.com", true, "guest13", "standart", false, null, null, "guest13@MAIL.COM", "AQAAAAEAACcQAAAAEBTzi5jb+UoCgXi3VmcLzx7V6PO/gJ4jD0zHkGmgItSIgm1QUNgAqj3J0bxfFT1lyg==", null, false, "beca0019-cf91-4b9b-87b0-22d5e81e509d", false, "guest13@mail.com" },
                    { "64530410-94ad-4f35-9c57-369626b3120c", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "41a59a1c-82e1-4975-9115-d84813f3ae4a", "guest3@mail.com", true, "guest3", "standart", false, null, null, "guest3@MAIL.COM", "AQAAAAEAACcQAAAAEDBWTHaFPWf03voJiN33AEhW1P0C7aSJFn7uSoj3OKXGM4AdKV3zhrqSQHGzc73log==", null, false, "e4664e5b-fec3-499d-bf8b-115a1bf7f967", false, "guest3@mail.com" },
                    { "a8568844-3eff-43b1-a056-b6a268d0baff", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "4448814e-c8e0-4857-8fc3-8245eda7d9c0", "guest4@mail.com", true, "guest4", "standart", false, null, null, "guest4@MAIL.COM", "AQAAAAEAACcQAAAAEOGAC6dIIkp4YDEjAvxr2RXcdwV6qDBBLkwqOvQm0bFe3tTOalbJyqNY2Cuc4Hmq7w==", null, false, "00529847-b313-44b6-babb-c54746db05e4", false, "guest4@mail.com" },
                    { "96637916-fc7d-4c1a-9fa6-5c1333f8182b", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "801935e2-67b5-4238-ad56-34351d761f4f", "guest5@mail.com", true, "guest5", "standart", false, null, null, "guest5@MAIL.COM", "AQAAAAEAACcQAAAAEF25RU05JWazU4iIgvAvUEhtDhAEFc0Rsk3/ryVoD5L8Y1tQ4GxyQgpR6Bb9oa2B2g==", null, false, "8adfa8f3-afa0-4d54-a248-e73ac6eba495", false, "guest5@mail.com" },
                    { "8b318b2f-6da1-4fda-8569-5ec2c616dc06", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "c880b085-8f0b-495d-a16e-2c5ad62c5267", "guest2@mail.com", true, "guest2", "standart", false, null, null, "guest2@MAIL.COM", "AQAAAAEAACcQAAAAEBt185DxFMSgIdbLTdtz4DXuaH/n7xT8DNrJzj7V8Znco5QLpdUv8O7yIm8b+ZyDzg==", null, false, "e9318eed-7728-45d2-9fc8-0462ed2c8114", false, "guest2@mail.com" },
                    { "d311541a-1eb5-4ff1-b3f7-4cf9488d66a6", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "e4ae6c9b-e6e7-427f-bee4-8375549d6d6d", "guest7@mail.com", true, "guest7", "standart", false, null, null, "guest7@MAIL.COM", "AQAAAAEAACcQAAAAEHa6FppseixeuGLHIcmjNIG29g60/JNXF3EAltdgV3z0AIAn29W+lGYdTHRTkaRydA==", null, false, "894a6ba2-47b5-424c-b365-917c1208c803", false, "guest7@mail.com" },
                    { "0e57a01c-79f6-4fb3-826e-99e97eef753e", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "23f571ac-27bf-478d-97f8-f7b06e25f4e0", "guest8@mail.com", true, "guest8", "standart", false, null, null, "guest8@MAIL.COM", "AQAAAAEAACcQAAAAEMq5tyF6NAj5TkdyKsWpYsq778OEmODs1SHz1m/d/skEzcAosNPZkumSNTpxy24bPg==", null, false, "0d8f0178-37b0-4ba6-822f-ed56675c8951", false, "guest8@mail.com" },
                    { "04f5c6bc-8c8b-4d51-b5aa-628359015b2c", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "f8f523aa-9dc0-4db7-b453-92854fefeed3", "guest9@mail.com", true, "guest9", "standart", false, null, null, "guest9@MAIL.COM", "AQAAAAEAACcQAAAAEKYNQsdk+9CM0bHcNnZbyogdvDhTc9E5wa+cszFGN9wrhFBU+IhceUXiDKwko7aXwA==", null, false, "86acf8e8-d189-4b07-a27e-434e2e96057e", false, "guest9@mail.com" },
                    { "4bc45931-567a-45c1-9957-30688c16ac96", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "2fd6d1c0-d4fa-4f24-a551-70c82640f080", "guest10@mail.com", true, "guest10", "standart", false, null, null, "guest10@MAIL.COM", "AQAAAAEAACcQAAAAEAu9nUYRSPAZ9f0PFhX9cF6mt/gkEsJUyjJkKDnIohtLLOhCNixGf5RXxvlqNpyzgw==", null, false, "78f729ed-e483-457c-a69c-8c3d768b111d", false, "guest10@mail.com" },
                    { "1f817850-7284-4c93-924f-4b98757d6339", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "950cd829-71b9-44a9-897c-bda4a9ee5f54", "guest11@mail.com", true, "guest11", "standart", false, null, null, "guest11@MAIL.COM", "AQAAAAEAACcQAAAAEE9Dhecvfy2WH1nnj9OB7mzYCklIWYOZX3x9u0wBYJp/p0UielLG8YLOHrwjgq998w==", null, false, "d8362cc4-d6a6-4113-9630-8f5c211c865f", false, "guest11@mail.com" },
                    { "4fe22b44-56fd-4e18-97d5-2bafc06b8985", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "592d49db-15f9-4fb2-8085-4ec1d4029773", "guest6@mail.com", true, "guest6", "standart", false, null, null, "guest6@MAIL.COM", "AQAAAAEAACcQAAAAEAHDbceRo1cX0QphooEV9w7CtEW5sIDHsRj4VJI4Vc0QEXgQKsG3E+nvLfOWHlYzqQ==", null, false, "0b90b6e4-6078-46e8-9f78-7e43227f0112", false, "guest6@mail.com" },
                    { "d3ed8abf-0003-44bd-bd7c-9010005f8855", 0, new DateTime(1998, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "cb41b8e4-9451-441a-9c9a-68965645954c", "admin@admin.com", true, "admin", "admin", false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEE8Jtdqupv1UDWXfnhDIElLk2Wd5pG3iHa5XAvKayPR1/ogRjgB6Mr0SQXr0hQ/xfQ==", null, false, "71041d34-186f-47c4-9004-6eaa05a9fdc0", false, "admin@admin.com" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Description", "ParentId", "Title" },
                values: new object[,]
                {
                    { 1, "Super MVC", null, ".NET" },
                    { 2, "Super Spring", null, "Spring" },
                    { 3, "Super ReactJS", null, "ReactJS" },
                    { 4, "Super AngularJS", null, "AngularJS" },
                    { 5, "Super PythonBackend", null, "PythonBackend" },
                    { 6, "Super Magento", null, "PHP Magento" },
                    { 7, "Super WordPress", null, "PHP WordPress" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9101c5bd-3b81-4689-9d33-90b46c11f80d", "548f64e8-e83a-4e5d-85a7-7c2c16a28274" },
                    { "fadb92af-6bec-461f-a769-a70bd30df2da", "d3ed8abf-0003-44bd-bd7c-9010005f8855" },
                    { "15d18b58-a0a4-4f87-bce4-3c692370a9ab", "af329e4e-1c95-45eb-af28-d51402030a87" },
                    { "15d18b58-a0a4-4f87-bce4-3c692370a9ab", "7bb4ec4f-e6db-4f97-a003-c117ee1b3c3e" },
                    { "15d18b58-a0a4-4f87-bce4-3c692370a9ab", "ca7db5b9-ef26-4480-9810-02258bda2904" },
                    { "15d18b58-a0a4-4f87-bce4-3c692370a9ab", "7611c080-1a64-4f9f-8dca-6c66646e08c4" },
                    { "15d18b58-a0a4-4f87-bce4-3c692370a9ab", "2bd2ce37-376f-4762-a78d-b1335ddfac35" },
                    { "9101c5bd-3b81-4689-9d33-90b46c11f80d", "9bdd5c5f-c359-44cf-9236-87eea9b36913" },
                    { "64d6ed5b-0d40-46eb-963a-fdda178d0a86", "327ed16e-6857-4fbf-b548-ccb6ae875499" },
                    { "64d6ed5b-0d40-46eb-963a-fdda178d0a86", "8b318b2f-6da1-4fda-8569-5ec2c616dc06" },
                    { "64d6ed5b-0d40-46eb-963a-fdda178d0a86", "64530410-94ad-4f35-9c57-369626b3120c" },
                    { "64d6ed5b-0d40-46eb-963a-fdda178d0a86", "b303e61e-6b3a-48b4-a4fc-3175840921a7" },
                    { "64d6ed5b-0d40-46eb-963a-fdda178d0a86", "96637916-fc7d-4c1a-9fa6-5c1333f8182b" },
                    { "64d6ed5b-0d40-46eb-963a-fdda178d0a86", "a8568844-3eff-43b1-a056-b6a268d0baff" },
                    { "9101c5bd-3b81-4689-9d33-90b46c11f80d", "be35fe96-8b81-43ba-8f1d-d0917ea569a0" },
                    { "9101c5bd-3b81-4689-9d33-90b46c11f80d", "1f817850-7284-4c93-924f-4b98757d6339" },
                    { "64d6ed5b-0d40-46eb-963a-fdda178d0a86", "4bc45931-567a-45c1-9957-30688c16ac96" },
                    { "9101c5bd-3b81-4689-9d33-90b46c11f80d", "2f85eb25-4314-441f-b48a-8252294691bf" },
                    { "64d6ed5b-0d40-46eb-963a-fdda178d0a86", "0e57a01c-79f6-4fb3-826e-99e97eef753e" },
                    { "64d6ed5b-0d40-46eb-963a-fdda178d0a86", "d311541a-1eb5-4ff1-b3f7-4cf9488d66a6" },
                    { "64d6ed5b-0d40-46eb-963a-fdda178d0a86", "4fe22b44-56fd-4e18-97d5-2bafc06b8985" },
                    { "64d6ed5b-0d40-46eb-963a-fdda178d0a86", "04f5c6bc-8c8b-4d51-b5aa-628359015b2c" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Title", "TopicId" },
                values: new object[,]
                {
                    { 6, "Super PHP", "PHP", 7 },
                    { 5, "Super PHP", "PHP", 6 },
                    { 4, "Super Python", "Python", 5 },
                    { 3, "Super JavaScript", "JavaScript", 3 },
                    { 2, "Super Spring", "Java", 2 },
                    { 1, "Super MVC", "ASP", 1 }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "EntityID", "GroupId", "Role", "State", "UserID" },
                values: new object[,]
                {
                    { 16, 5, null, 2, 2, "548f64e8-e83a-4e5d-85a7-7c2c16a28274" },
                    { 1, 1, null, 3, 0, "b303e61e-6b3a-48b4-a4fc-3175840921a7" },
                    { 15, 4, null, 2, 2, "9bdd5c5f-c359-44cf-9236-87eea9b36913" },
                    { 2, 2, null, 3, 0, "327ed16e-6857-4fbf-b548-ccb6ae875499" },
                    { 3, 3, null, 3, 0, "8b318b2f-6da1-4fda-8569-5ec2c616dc06" },
                    { 4, 4, null, 3, 0, "64530410-94ad-4f35-9c57-369626b3120c" },
                    { 5, 1, null, 3, 0, "a8568844-3eff-43b1-a056-b6a268d0baff" },
                    { 6, 2, null, 3, 0, "96637916-fc7d-4c1a-9fa6-5c1333f8182b" },
                    { 7, 3, null, 3, 0, "4fe22b44-56fd-4e18-97d5-2bafc06b8985" },
                    { 8, 4, null, 3, 0, "d311541a-1eb5-4ff1-b3f7-4cf9488d66a6" },
                    { 10, 2, null, 3, 0, "04f5c6bc-8c8b-4d51-b5aa-628359015b2c" },
                    { 11, 3, null, 3, 0, "4bc45931-567a-45c1-9957-30688c16ac96" },
                    { 12, 1, null, 2, 2, "1f817850-7284-4c93-924f-4b98757d6339" },
                    { 9, 1, null, 3, 0, "0e57a01c-79f6-4fb3-826e-99e97eef753e" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "EntityID", "GroupId", "Role", "State", "UserID" },
                values: new object[,]
                {
                    { 13, 2, null, 2, 2, "be35fe96-8b81-43ba-8f1d-d0917ea569a0" },
                    { 14, 3, null, 2, 2, "2f85eb25-4314-441f-b48a-8252294691bf" }
                });

            migrationBuilder.InsertData(
                table: "Methodists",
                columns: new[] { "Id", "LinkToContact", "UserId" },
                values: new object[,]
                {
                    { 20, "https://web.telegram.org", "7611c080-1a64-4f9f-8dca-6c66646e08c4" },
                    { 18, "https://web.telegram.org", "7bb4ec4f-e6db-4f97-a003-c117ee1b3c3e" },
                    { 19, "https://web.telegram.org", "ca7db5b9-ef26-4480-9810-02258bda2904" },
                    { 21, "https://web.telegram.org", "2bd2ce37-376f-4762-a78d-b1335ddfac35" },
                    { 17, "https://web.telegram.org", "af329e4e-1c95-45eb-af28-d51402030a87" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Type", "UserId" },
                values: new object[,]
                {
                    { 7, 0, "4fe22b44-56fd-4e18-97d5-2bafc06b8985" },
                    { 9, 0, "0e57a01c-79f6-4fb3-826e-99e97eef753e" },
                    { 6, 0, "96637916-fc7d-4c1a-9fa6-5c1333f8182b" },
                    { 5, 0, "a8568844-3eff-43b1-a056-b6a268d0baff" },
                    { 4, 0, "64530410-94ad-4f35-9c57-369626b3120c" },
                    { 10, 0, "04f5c6bc-8c8b-4d51-b5aa-628359015b2c" },
                    { 3, 0, "8b318b2f-6da1-4fda-8569-5ec2c616dc06" },
                    { 2, 0, "327ed16e-6857-4fbf-b548-ccb6ae875499" },
                    { 1, 0, "b303e61e-6b3a-48b4-a4fc-3175840921a7" },
                    { 8, 0, "d311541a-1eb5-4ff1-b3f7-4cf9488d66a6" },
                    { 11, 0, "4bc45931-567a-45c1-9957-30688c16ac96" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Bio", "LinkToProfile", "UserId" },
                values: new object[,]
                {
                    { 15, "Teacher15. Programmist.", "https://github.com", "9bdd5c5f-c359-44cf-9236-87eea9b36913" },
                    { 12, "Teacher12. Programmist.", "https://github.com", "1f817850-7284-4c93-924f-4b98757d6339" },
                    { 13, "Teacher13. Programmist.", "https://github.com", "be35fe96-8b81-43ba-8f1d-d0917ea569a0" },
                    { 14, "Teacher14. Programmist.", "https://github.com", "2f85eb25-4314-441f-b48a-8252294691bf" },
                    { 16, "Teacher16. Programmist.", "https://github.com", "548f64e8-e83a-4e5d-85a7-7c2c16a28274" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CourseId", "MethodistId", "Number", "StartDate", "Status", "TeacherId" },
                values: new object[,]
                {
                    { 1, 1, 17, "ASP-1", new DateTime(2021, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 12 },
                    { 2, 2, 18, "Java-1", new DateTime(2021, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 13 },
                    { 3, 3, 19, "JavaScript-1", new DateTime(2021, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 14 },
                    { 4, 4, 20, "Python-1", new DateTime(2021, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 15 },
                    { 5, 5, 21, "PHP-1", new DateTime(2021, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 16 }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CourseId", "Duration", "FileId", "Name", "Theme" },
                values: new object[,]
                {
                    { 1, 1, 5, null, "Lesson 1", "Super Lesson 1" },
                    { 2, 1, 5, null, "Lesson 2", "Super Lesson 2" },
                    { 3, 1, 5, null, "Lesson 3", "Super Lesson 3" },
                    { 4, 2, 1, null, "Lesson 1", "Super Lesson 1" },
                    { 5, 2, 1, null, "Lesson 2", "Super Lesson 2" },
                    { 6, 2, 1, null, "Lesson 3", "Super Lesson 3" }
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
                name: "IX_GroupLessons_GroupId",
                table: "GroupLessons",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupLessons_LessonId",
                table: "GroupLessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CourseId",
                table: "Groups",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_MethodistId",
                table: "Groups",
                column: "MethodistId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TeacherId",
                table: "Groups",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_FileId",
                table: "Lessons",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Methodists_UserId",
                table: "Methodists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMarks_CourseId",
                table: "StudentMarks",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMarks_LessonId",
                table: "StudentMarks",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMarks_StudentId",
                table: "StudentMarks",
                column: "StudentId");

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
                name: "GroupLessons");

            migrationBuilder.DropTable(
                name: "StudentMarks");

            migrationBuilder.DropTable(
                name: "StudentToGroupActions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Methodists");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
