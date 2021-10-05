using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityNLayer.DAL.EF.Migrations
{
    public partial class CourseTopicRelation : Migration
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
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkToContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_AspNetUsers_UserId",
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
                    ManagerId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Groups_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
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
                    { "a54919e9-67d5-4a66-aceb-97981c9661a9", "ac4cc7ce-4186-4baa-b337-f0fb03a6b321", "Teacher", "TEACHER" },
                    { "3672b7f2-195a-4ba6-ad1e-0e1dd169bb8c", "bf6ef346-9f4b-49e3-84a1-f9b0d69b238b", "Manager", "MANAGER" },
                    { "d9b2deec-89dc-4f4d-bd08-cab9a6fc1a25", "5b90b91e-10b8-4f6a-9f0d-11c47e72279b", "Admin", "ADMIN" },
                    { "acdd5a81-58dd-4bcd-a113-226be4cdd1f2", "42bd63cb-baf9-4c9d-98ec-457afe95b87d", "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "d0a8b9d6-d1ea-4c98-b63d-66cb5f71f846", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "5938b488-7af8-4f34-a74f-0071d125c59b", "guest19@mail.com", true, "guest19", "standart", false, null, null, "guest19@MAIL.COM", "AQAAAAEAACcQAAAAEFxIjIxoI3qSxmAJc4ZIbN2Bo4VGM60SBUF2hzUvTWfUqXKHapH4eFHB0yW3KdVFUA==", null, false, "dd9ba037-ea2c-4728-b246-2c96a2642ccf", false, "guest19@mail.com" },
                    { "c38ed63f-ebc1-4c72-9e1b-ff2e9b387433", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "9af8e0cf-3059-44b5-9066-e4d89f31a5d6", "guest18@mail.com", true, "guest18", "standart", false, null, null, "guest18@MAIL.COM", "AQAAAAEAACcQAAAAECuLXzVwuFl9Bx0+262c8CYp37HfHSsmgb0xMXRjKxG+/Da2mGAXqiQiojoib5L2XQ==", null, false, "78031e49-8ebd-4db7-ad01-a5367c057c32", false, "guest18@mail.com" },
                    { "9066ac52-e825-4822-b075-8ef620c7c611", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "83e41360-01c1-40ac-bdb5-3720b4be1d02", "guest17@mail.com", true, "guest17", "standart", false, null, null, "guest17@MAIL.COM", "AQAAAAEAACcQAAAAEFoW0oEucvUgM4otzabKaOO5vwbOgqbxF3uYbRcF+qoXW3wciTIZPbu5MxqY7STA8g==", null, false, "f4b78217-a46b-4044-8a92-b35adf4477c7", false, "guest17@mail.com" },
                    { "089927af-9f98-4ce8-838f-c3f1e7d46b94", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "68330fb6-769c-46d2-a418-74b09463c437", "guest16@mail.com", true, "guest16", "standart", false, null, null, "guest16@MAIL.COM", "AQAAAAEAACcQAAAAEO5aH5ijXNn4HJjiZ4VqmfnhWD8/NIXW/xceyWVfojrh0XeLAT9BcF4zWUIDuhaJkw==", null, false, "ff25d6fe-ea14-46d5-bb3f-7a7e5f74ff44", false, "guest16@mail.com" },
                    { "924198cd-14ad-49fc-8fec-69a299aff272", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "13b2cd56-806e-4981-b1c0-694c33b35f44", "guest15@mail.com", true, "guest15", "standart", false, null, null, "guest15@MAIL.COM", "AQAAAAEAACcQAAAAEEKR98dMsprLotWuXvhnozqb49bGePQoZdTmxDQtXHohvzd1FqJxViHjN3ZwfS/97w==", null, false, "dda873eb-0040-464d-a40a-1aa03e57a91f", false, "guest15@mail.com" },
                    { "534d74ee-fdd9-416e-8ef3-71033c537b6a", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "55776b44-3567-4bce-9bbf-fde109540766", "guest14@mail.com", true, "guest14", "standart", false, null, null, "guest14@MAIL.COM", "AQAAAAEAACcQAAAAENUti8Jnbf0ck+5RLJRDOAcrNmXJ0s06HhAVo2k+qS9xNcOypuZaKifqPZlNyniTPg==", null, false, "cca37989-71af-4601-a719-b5189c971faa", false, "guest14@mail.com" },
                    { "242129a6-69da-49fe-88e5-aec37559b807", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "c2d6dff5-2e5e-425e-afe2-64dd3d4a18e1", "guest12@mail.com", true, "guest12", "standart", false, null, null, "guest12@MAIL.COM", "AQAAAAEAACcQAAAAEIGkr5bs15ycRlUlNHn1YcZ7yQwWOW2wXZHIGrFQ26csspx7caOk8Re1ZCN1jem1lg==", null, false, "52505a40-2a5d-47b1-bcb5-db8718d03e84", false, "guest12@mail.com" },
                    { "9ee86e0f-2703-4300-9c4d-a51692607087", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "dd858f18-3319-4427-9266-6e52ab6f9760", "guest11@mail.com", true, "guest11", "standart", false, null, null, "guest11@MAIL.COM", "AQAAAAEAACcQAAAAEBTk63KacR0D/MnliodI+2gG2PkZRHm4spTIgEOY8Gayn5qEQ3E/CpsFKT6U+qEJGQ==", null, false, "a9087dfe-3668-4864-b6ab-d52cc7d58a8c", false, "guest11@mail.com" },
                    { "946eb14c-5032-4fd1-a92c-889b27fdf22e", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "3242b62e-f10f-41fc-85c6-faf9ca2d712e", "guest13@mail.com", true, "guest13", "standart", false, null, null, "guest13@MAIL.COM", "AQAAAAEAACcQAAAAEOiV/kV5RSv6KKwnMaxI1iuddeFJDEkI6ADd87HYK9mMIC+cG/ptU+ReDQGGqA0kqw==", null, false, "0ea8d5c9-1b1f-42ce-9c68-a80451699322", false, "guest13@mail.com" },
                    { "a2305cf8-b61b-407c-8584-255a46eae3e2", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "03a39a8c-a197-4401-9d81-4a8cbd137f96", "guest9@mail.com", true, "guest9", "standart", false, null, null, "guest9@MAIL.COM", "AQAAAAEAACcQAAAAEC5Foe0yYgUxkGar/IKM1eTGNa2Or3Ga1mkfhkeVRsky/RUtOBRFn2uJAVYNJdCYzQ==", null, false, "b27fdc96-d09c-4050-bef2-4759446144ab", false, "guest9@mail.com" },
                    { "852ca49f-1619-4d99-8b09-b1e91d0d04ff", 0, new DateTime(2000, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "909536c6-0a71-4444-a742-e68f6efb441f", "manager@manager.com", true, "manager", "manager", false, null, null, "MANAGER@MANAGER.COM", "AQAAAAEAACcQAAAAEFDjhim9BDcn1EfDDmQmdBD3DhKlxx/ASduvLW5g7tD9oKYDZcPkOwI+xNE7eRJixQ==", null, false, "fea3357b-ca8b-4a3d-86d6-521a373509cd", false, "manager@manager.com" },
                    { "49676724-9b31-4936-b2d1-0740fd6e4627", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "89ad82c0-4e09-42ce-8ecd-322677856d1b", "guest1@mail.com", true, "guest1", "standart", false, null, null, "guest1@MAIL.COM", "AQAAAAEAACcQAAAAEGi5jsQi1gN/SupTk6RpWXX79OIQ/xPtwYE9W4QxR7msz3wSZttvy83NJfVaJ7Z+dA==", null, false, "e63d8942-650e-4ba0-8426-880f42b0efcc", false, "guest1@mail.com" },
                    { "4babdb25-0cc3-4e16-830f-083c881ad932", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "9c5293e7-8d08-43cc-8dfe-4f7c37a90098", "guest10@mail.com", true, "guest10", "standart", false, null, null, "guest10@MAIL.COM", "AQAAAAEAACcQAAAAEExZY76h8lyKDFjpA5FmZDv6eoriYkFxWT7VpZTjjnLY0KMDLxO5PM2hgd/4IvflLQ==", null, false, "e442ad6c-a121-4f9c-86e8-3f248b2e58a6", false, "guest10@mail.com" },
                    { "6e2f52c6-1bbe-4f23-b36a-b276ed10e54b", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ab3a9fe2-eadf-46a6-811a-b271855f9448", "guest2@mail.com", true, "guest2", "standart", false, null, null, "guest2@MAIL.COM", "AQAAAAEAACcQAAAAELh+ggVEH7pjJjHkw7h5531dRrfoXuoZyM+lazxavteSvo7D9rc1J6gKtWfNssEveA==", null, false, "5311dfcc-819f-4567-9591-e744e8085792", false, "guest2@mail.com" },
                    { "4969de28-5308-411b-af82-eb8a9b6b5091", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "61909bac-8fa6-4d36-a926-391626391fdf", "guest3@mail.com", true, "guest3", "standart", false, null, null, "guest3@MAIL.COM", "AQAAAAEAACcQAAAAEI5E9Kc6lvQqTcGbBkOe414gWwdaXfzpuiRl7SWoL6ftF6I7xSF+Rc39nwfhT7jxJQ==", null, false, "a36dbff0-6aa3-40f3-ab14-e525acfcc696", false, "guest3@mail.com" },
                    { "cb93483c-3391-4a23-a4dd-5e9c0e518602", 0, new DateTime(1998, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "2630d96c-84a3-4314-901f-38bbad2f8a9c", "admin@admin.com", true, "admin", "admin", false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEPTFmKESFMob24bxO4KeFbRMBPVax8ItjVcSfHl8ZFJN8kXKtrh5tktOhyTHmbXWWQ==", null, false, "2dfccf43-6139-4093-a2c0-9c5518cf7a0e", false, "admin@admin.com" },
                    { "1967840a-b38f-41a5-903c-012e3b2d25b6", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "a038b38e-746f-4d4c-a8ea-6e862c481800", "guest5@mail.com", true, "guest5", "standart", false, null, null, "guest5@MAIL.COM", "AQAAAAEAACcQAAAAELNB5sxsGwje7v1b4PXvs7bKK4+mgjwZmbFVjxUs9a4Cb2hI02MXo3wGY5Vq1gdLTQ==", null, false, "534c78d4-3647-4de2-9024-ca52e57c3f92", false, "guest5@mail.com" },
                    { "dd504119-de1e-4d50-a866-2b7a9bbe16a0", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "c5680bd3-f95f-4cab-bccb-5d84d0b0bab6", "guest6@mail.com", true, "guest6", "standart", false, null, null, "guest6@MAIL.COM", "AQAAAAEAACcQAAAAEAnNQ5zDMvVaDi4nDu9WKlJbu7Cra5yDSs8NC3HIxfI517lhut7cjjRajKAOMDIxsw==", null, false, "99c8ad47-ccdc-4c0a-a0a0-308a547d7c36", false, "guest6@mail.com" },
                    { "36cb28b0-9fb1-46f9-99ff-a071ea8bc769", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "9d5a9947-f322-4e1e-9e29-abe25e7483e8", "guest7@mail.com", true, "guest7", "standart", false, null, null, "guest7@MAIL.COM", "AQAAAAEAACcQAAAAEI9STmlvBkjI5mRg7p9GkMa+1HqgGUptYiI5m0jhvDb3Dhw+P8rMwSrTwvP+eduPQw==", null, false, "2e7258ef-26c6-4cfa-82ae-56d3fd41a13d", false, "guest7@mail.com" },
                    { "18f350b7-4dbf-4a05-9a48-3509415890a6", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "5e24ad3f-5808-4d57-9360-d969783e0d31", "guest8@mail.com", true, "guest8", "standart", false, null, null, "guest8@MAIL.COM", "AQAAAAEAACcQAAAAENNB5h3joM+JqPMkR7p1Wwl8v3dTz5V1rQ8H5bphyH5zOhnsqnj6L68rfniT/fEAuw==", null, false, "1500f562-5970-4b0d-9c36-0e15134164ce", false, "guest8@mail.com" },
                    { "283aeb1d-4dba-49f0-8597-f55c05b7a1e0", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "7c2b863c-e092-4700-a785-c508116a1789", "guest4@mail.com", true, "guest4", "standart", false, null, null, "guest4@MAIL.COM", "AQAAAAEAACcQAAAAEGqV0wrdeG6JTswgIPNkrQof/rHqYkJnqYDFpC6KtU5rtUjjQsrY9E0x8cNvzMa4Mw==", null, false, "1e31d731-52cf-410d-add4-d9648d1669ab", false, "guest4@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Description", "ParentId", "Title" },
                values: new object[,]
                {
                    { 2, "Super Spring", null, "Spring" },
                    { 3, "Super ReactJS", null, "ReactJS" },
                    { 4, "Super AngularJS", null, "AngularJS" },
                    { 5, "Super PythonBackend", null, "PythonBackend" },
                    { 1, "Super MVC", null, ".NET" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d9b2deec-89dc-4f4d-bd08-cab9a6fc1a25", "cb93483c-3391-4a23-a4dd-5e9c0e518602" },
                    { "3672b7f2-195a-4ba6-ad1e-0e1dd169bb8c", "852ca49f-1619-4d99-8b09-b1e91d0d04ff" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Title", "TopicId" },
                values: new object[,]
                {
                    { 1, "Super MVC", "ASP", 1 },
                    { 2, "Super Spring", "Java", 2 },
                    { 3, "Super JavaScript", "JavaScript", 3 },
                    { 4, "Super Python", "Python", 5 }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CourseId", "Duration", "FileId", "Name", "Theme" },
                values: new object[] { 1, 1, 5, null, "Lesson 1", "Super Lesson 1" });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CourseId", "Duration", "FileId", "Name", "Theme" },
                values: new object[] { 2, 1, 5, null, "Lesson 2", "Super Lesson 2" });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CourseId", "Duration", "FileId", "Name", "Theme" },
                values: new object[] { 3, 1, 5, null, "Lesson 3", "Super Lesson 3" });

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
                name: "IX_Groups_ManagerId",
                table: "Groups",
                column: "ManagerId");

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
                name: "IX_Managers_UserId",
                table: "Managers",
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
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
