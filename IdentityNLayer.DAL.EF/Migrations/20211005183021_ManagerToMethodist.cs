using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityNLayer.DAL.EF.Migrations
{
    public partial class ManagerToMethodist : Migration
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
                    { "6497be4a-3bb6-4284-be8c-1a27a507c000", "8dc28c10-ef6a-4658-b429-e2c425fa59c5", "Teacher", "TEACHER" },
                    { "62fd29f0-8e12-450f-8407-8aba848f8b60", "dcbbfd35-2d45-4ded-9ce1-0502b9718863", "Methodist", "METHODIST" },
                    { "519c1fa8-725c-4a21-99e4-5cea7da2b861", "2621e69d-3ea4-4f80-bdd4-32b0c423ca92", "Admin", "ADMIN" },
                    { "cd9d903a-5452-4861-a470-005cac6aa06d", "d5724a29-2eb2-45c6-9473-19bdeb8ce021", "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8755e974-167a-411b-8f54-cb209194260b", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ebdee0a9-13e3-44ae-9b3e-4178bab626be", "guest19@mail.com", true, "guest19", "standart", false, null, null, "guest19@MAIL.COM", "AQAAAAEAACcQAAAAEKItvtClUPyWJCqduRNj5IJZtFGRKo5xo75eALUwq8r36XzrWZcahX+Fb3ysKhh3tw==", null, false, "d48fd935-4be8-4385-a447-af41a2c24bc8", false, "guest19@mail.com" },
                    { "e25c565d-4bf6-4671-9ecf-b24c283d78dc", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "93b704d3-1de7-484b-ac0b-4331eace5805", "guest18@mail.com", true, "guest18", "standart", false, null, null, "guest18@MAIL.COM", "AQAAAAEAACcQAAAAEPI1fwRkpNNagHdOa7RcnOrD7QNGm6s5YFgWvvWhfSu/0ZYHH5S1MCGgS2uaBtCnUg==", null, false, "ee1c01a9-facd-4f01-9692-de7671a0444b", false, "guest18@mail.com" },
                    { "2977a779-46d5-4ac3-9eef-f377237a2b40", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "6b9f2466-6d07-4a1f-8c58-a378f2586fb7", "guest17@mail.com", true, "guest17", "standart", false, null, null, "guest17@MAIL.COM", "AQAAAAEAACcQAAAAEMi0Zi3dCh3oFJHH3ocPoosdg4WjATBPd9R7LnTgPl5xNEZ2i1fBclKgGqh1KcOtNw==", null, false, "b9706914-dc4a-44b7-9cda-767305b93df0", false, "guest17@mail.com" },
                    { "a73476e3-a381-494e-bd7e-49836220a2d3", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "8140cf11-adc0-44dc-9018-30cd16fbd81e", "guest16@mail.com", true, "guest16", "standart", false, null, null, "guest16@MAIL.COM", "AQAAAAEAACcQAAAAEPcrW9po1hHX6RuVesvETR4vYvxvxrAMd7Kzj8YIFmQPKtmeeiskeXlyEtwT9UalwA==", null, false, "8a26faf6-58de-4a22-ac3e-e040c1276183", false, "guest16@mail.com" },
                    { "e0c0a58b-3054-4c58-b570-4787afb43745", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "dca606c5-37e6-4129-99ce-fe776c7ef2f7", "guest15@mail.com", true, "guest15", "standart", false, null, null, "guest15@MAIL.COM", "AQAAAAEAACcQAAAAEFz6W+olONeeg5dwP0OlZjfQeBQfENjfD7Qwf1qBWAoHtFqH2MZGv3nhydJZHHtExQ==", null, false, "0d369d29-c4ad-43f8-831e-d87d6ba9a423", false, "guest15@mail.com" },
                    { "25ff1f64-38fc-4117-b6cd-d703d905e164", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "3941f0e5-b8c0-4f4c-a40f-e51595d17a28", "guest14@mail.com", true, "guest14", "standart", false, null, null, "guest14@MAIL.COM", "AQAAAAEAACcQAAAAEMC2Gh7OVQZocDpujkqjl+7lrU5ZU5ZiGtpS7gsYJ8SmQu0uKSbScXfI/NP1xn5ebQ==", null, false, "ba264cd0-ec1e-4e2e-967b-56b84ca0cc95", false, "guest14@mail.com" },
                    { "ffbe500e-7aa6-45f6-8ffe-795486799270", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "47f9189a-9378-4da2-9520-f9698800047b", "guest12@mail.com", true, "guest12", "standart", false, null, null, "guest12@MAIL.COM", "AQAAAAEAACcQAAAAECKb+ZPpxUDzd9GtL5Y7B3Sc87olB8IGkNMmgT1B7le2xh1uDZ3f8VfI72hPmiJkNg==", null, false, "e9eca0ef-7f90-4e99-bad5-6e48ed76fef8", false, "guest12@mail.com" },
                    { "8d710781-8172-411d-ae67-355192870b93", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "33716264-f4a9-4b30-8730-68baf7e0c46e", "guest11@mail.com", true, "guest11", "standart", false, null, null, "guest11@MAIL.COM", "AQAAAAEAACcQAAAAELT28vdJRRPSOjRAvfpy/CktXfmKeOuA/RtyG1fz6ruqHkqCwZQJSzXpmeFSkBwxyg==", null, false, "447e1675-3edd-4dbb-b671-2e22e1f0addb", false, "guest11@mail.com" },
                    { "9f981c51-1b80-49e7-8031-9815138cab47", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "7391446d-9fe2-47bb-b893-7a67691eb71c", "guest13@mail.com", true, "guest13", "standart", false, null, null, "guest13@MAIL.COM", "AQAAAAEAACcQAAAAEPqd+7O7R/nhqT3krVAP/oyVBI/TjwEw3MXjGP1BLPdJVOe4YQ1Oxwtc4nwByaRQIg==", null, false, "1a650ab5-fac6-4a2a-b81b-a0e360598dc0", false, "guest13@mail.com" },
                    { "0738e39c-c751-4f85-b744-21e03846682e", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "69c1071f-854f-43dc-8eaa-4913f45dfedb", "guest9@mail.com", true, "guest9", "standart", false, null, null, "guest9@MAIL.COM", "AQAAAAEAACcQAAAAEKvtyj4tG88krYdODQYIf2UVHwlz+foToMiR6wD4aEfpByO6DkqaMNncophSZRmMYQ==", null, false, "469d926a-f8ce-4b81-b96d-cfe205d9be10", false, "guest9@mail.com" },
                    { "5d9d0627-70d1-4a69-89f9-fe07fdeb93db", 0, new DateTime(2000, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "7e7a677f-78b2-45a2-8dd2-39b090e13ac2", "methodist@methodist.com", true, "methodist", "methodist", false, null, null, "METHODIST@METHODIST.COM", "AQAAAAEAACcQAAAAEKlzUoLctxobxx6uSzbMJq0CJW13D/lN8JxwO3+Z5zGqaSjMSOuGUKuFupWNKHoNpQ==", null, false, "b6e6d465-165e-4519-954c-e45061f56f2f", false, "methodist@methodist.com" },
                    { "6d4d1c97-ca8f-421d-b4e3-07558e0557f9", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "f87fdd6a-262f-4cc5-a960-38911e9c43d2", "guest1@mail.com", true, "guest1", "standart", false, null, null, "guest1@MAIL.COM", "AQAAAAEAACcQAAAAED1+QzptGLbIh4q04TuA8T8gMvllotTbJD5pFGpue+d0ffNdXY/ZMxb+dZqSi9LquQ==", null, false, "6103c668-8e52-4589-823f-8edd1857d479", false, "guest1@mail.com" },
                    { "1a34d9f7-8256-4c36-85d7-850ae44eb95b", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "0f0e3c5a-42ba-436d-b49a-2a03b76c7be8", "guest10@mail.com", true, "guest10", "standart", false, null, null, "guest10@MAIL.COM", "AQAAAAEAACcQAAAAEBtvIjYq0Wmihkjm7WW881p5pvvhCFE/SmmZML5S+qO4WRazq3viuqUwARiaTBHh8w==", null, false, "dfb7eae2-6edc-41f9-b6db-25329b04735e", false, "guest10@mail.com" },
                    { "e1d19c01-950c-4bcd-a6ae-a4dffc26c813", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "75635b03-4795-48b9-8a57-7fb17712b904", "guest2@mail.com", true, "guest2", "standart", false, null, null, "guest2@MAIL.COM", "AQAAAAEAACcQAAAAEDRo0x5O3z5JlVjgvDLjzoiR8CjXURnDxbSKbfmk3qW0zTUQqED1buciDAZt3Q4qnw==", null, false, "2281326f-59d5-473f-a706-483416f24129", false, "guest2@mail.com" },
                    { "1102992a-dbcd-4952-9f84-c6015d56aa8f", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "73563b7f-ec28-4b5d-b11f-cef1f5220d11", "guest3@mail.com", true, "guest3", "standart", false, null, null, "guest3@MAIL.COM", "AQAAAAEAACcQAAAAEPL+GUWibZinlzqxIH4wG7s+1113YaK02oJgFmErSSAyr7bf9R02IfBAidM1Ccj3Pw==", null, false, "5f3c5fd7-713a-4236-a216-7f6751f3373e", false, "guest3@mail.com" },
                    { "67021ba0-6124-4d7c-b0eb-814508e620cf", 0, new DateTime(1998, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "495eb166-2f9b-438e-8345-9991de9e5da7", "admin@admin.com", true, "admin", "admin", false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEMOGkGkuTLVfmlLRu1DbK59JalbUlHJj6wsnw2gqXYSFpJtLQPmmzVgPRByFFtW0Jg==", null, false, "90864183-0746-4294-b688-7f560b493af3", false, "admin@admin.com" },
                    { "cde19143-c405-4ac8-a1aa-c6accc83b333", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "1a2fea3d-6240-4a14-8c03-cd1785a93321", "guest5@mail.com", true, "guest5", "standart", false, null, null, "guest5@MAIL.COM", "AQAAAAEAACcQAAAAEJ1l/FnjFxQ16A5KyJCv9YxLfCTHj/7+kolIX277a56T2NY1I0eZQ5KlrwdirfJryg==", null, false, "15d3c137-fccc-4912-9107-7d5de0c5daa6", false, "guest5@mail.com" },
                    { "63d9ccc4-14ea-419c-bb35-d23a04404c19", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "99032dbf-3000-4bc8-824e-dba4e8417d16", "guest6@mail.com", true, "guest6", "standart", false, null, null, "guest6@MAIL.COM", "AQAAAAEAACcQAAAAEAjCcpvJIe7p0oqXFEJ4baW7fhu6qX8Ua6NalSPCQ+dbyo1kivpBZO8tlSszn8SKOQ==", null, false, "07c00651-b3a7-4ad5-a8f2-a438dc1578c0", false, "guest6@mail.com" },
                    { "4ffb58bc-14b5-4f6f-aa0d-7c40c33361dd", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "6dd77622-8338-4439-9065-eaaee9c6d033", "guest7@mail.com", true, "guest7", "standart", false, null, null, "guest7@MAIL.COM", "AQAAAAEAACcQAAAAEHHvBa8+FHTJNK23+QYrYkRBnUsEd1Wt7LfVWEKYUotMa++cDjosGOj8wfe1LBJuLA==", null, false, "a8d94ae5-c8c0-43a7-995b-3916605683ae", false, "guest7@mail.com" },
                    { "6dfe72ca-d5dd-4ed4-861a-6a13a09cc065", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "e27bf3ff-5da7-4bbf-ab8b-40e3ec63343f", "guest8@mail.com", true, "guest8", "standart", false, null, null, "guest8@MAIL.COM", "AQAAAAEAACcQAAAAEEcSOv4PkfnnWvNbxhgqzfdGekAzCHiy97NCNrO5GgKZs4Xpbii3nPi8lYqTr1BbpQ==", null, false, "e5d09280-6b88-4b5d-b978-725a076fe156", false, "guest8@mail.com" },
                    { "99f6cd94-aea2-41d5-b2ff-cc40d5f4531a", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "59decaa6-18d4-48d0-b5f6-564ef6048a57", "guest4@mail.com", true, "guest4", "standart", false, null, null, "guest4@MAIL.COM", "AQAAAAEAACcQAAAAEGNfzx+4hc2YrJfqVrzJzmoGzC9Lbe53lOIY4/r745dKSJ32WHSm0+B3xGUfGsfr6A==", null, false, "1e041a17-8766-435d-a9ef-36909a4c0f03", false, "guest4@mail.com" }
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
                    { "519c1fa8-725c-4a21-99e4-5cea7da2b861", "67021ba0-6124-4d7c-b0eb-814508e620cf" },
                    { "62fd29f0-8e12-450f-8407-8aba848f8b60", "5d9d0627-70d1-4a69-89f9-fe07fdeb93db" }
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
