using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityNLayer.DAL.EF.Migrations
{
    public partial class SecondEntities : Migration
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
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
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
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    { "99ad8179-3dd8-4632-ace1-6a3085f94f39", "05039e76-2f2c-4309-a98a-69fd8a1b5447", "Teacher", "TEACHER" },
                    { "fc4232ad-8e29-4ab2-8ed4-d71c54e8de9e", "dcb8bf14-45d7-493b-83a6-7f5fccd52088", "Manager", "MANAGER" },
                    { "ffbc16ed-3ac4-41b2-9760-865ec3042cae", "99f77b93-1339-4b77-99b5-7e33b248203a", "Admin", "ADMIN" },
                    { "b77d3d06-7a75-428d-a4cf-0d51027f7199", "ea3ad26c-58e3-445c-91c1-eb5f7931f27f", "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "35344d10-92a3-4268-b84a-4e8b646ae647", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "9feca757-2d2b-4638-84cd-250001e769de", "guest19@mail.com", true, "guest19", "standart", false, null, null, "guest19@MAIL.COM", "AQAAAAEAACcQAAAAENX4n5lLXuk57UXN7OC/tNseM9sHspUJLwFYMFAYbCbOadNQMwOIb2lobmT5j4ubQg==", null, false, "345fa68e-d238-439c-913f-69f1ef5135d8", false, "guest19@mail.com" },
                    { "4855ea08-395a-4adf-9331-a881e7d57650", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "12667beb-7f9d-4498-a7d3-a858225a0d02", "guest18@mail.com", true, "guest18", "standart", false, null, null, "guest18@MAIL.COM", "AQAAAAEAACcQAAAAEDv75OpOgK1SBSGxtEe3I7mTdRjn49XitVEheCTTakmC/LP+fyVIC2LEQ0SpAI9FbQ==", null, false, "08943156-9c8e-445a-98c5-2371007ae72d", false, "guest18@mail.com" },
                    { "b94a148d-2378-4ab0-9d73-d8b505d4061a", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "412268c6-fbe6-45c8-90f8-682adefa9d35", "guest17@mail.com", true, "guest17", "standart", false, null, null, "guest17@MAIL.COM", "AQAAAAEAACcQAAAAEIxLJOn0Wt7ZJ6zUSq2spKJ6MmYgR5JbV++4GWPexxEZ9fRMpRR06j8XSv1WX5Wm2A==", null, false, "008c7c49-3f7e-443d-80c0-e4a72e65c0fd", false, "guest17@mail.com" },
                    { "acd3d29a-84c3-45b8-9db6-cb5e4b7a80de", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "a9069450-f6a8-418b-8492-4ef90c83aab4", "guest16@mail.com", true, "guest16", "standart", false, null, null, "guest16@MAIL.COM", "AQAAAAEAACcQAAAAEAJPgiFFlNSSZWHdMQJWajWE5JJQoMhQPPFIKNMSUZHpbaHJPjnuT9t4xyg25uassQ==", null, false, "c03d4dbe-c2e9-4308-b810-fdfe03eb20f3", false, "guest16@mail.com" },
                    { "26c006fd-2197-4dcf-9c42-220b902382b7", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "f760a07f-617a-4116-a4cb-dbeeeaa9b12b", "guest15@mail.com", true, "guest15", "standart", false, null, null, "guest15@MAIL.COM", "AQAAAAEAACcQAAAAEETDrnD1Q48nTMtU+wMrwVHfc+dv1oiAP8Q3IMqgJg40DaOwjnTgllxAZJUXZnQ8mw==", null, false, "1f6394f3-7b59-4a51-a5c5-3a86203b3ffd", false, "guest15@mail.com" },
                    { "44a849e9-ec55-40da-aa58-0e1d3d650f86", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "7d36f917-0731-4ad6-9901-7eefd42a4ac0", "guest14@mail.com", true, "guest14", "standart", false, null, null, "guest14@MAIL.COM", "AQAAAAEAACcQAAAAEKykpVFX8Rva00HDEftK2ZAOx5tgDm3P84d/HlQltXeD8OC46iwQxsSY2QwuD5EZVA==", null, false, "79339505-0be8-4b0c-b67b-5ff942b6d55c", false, "guest14@mail.com" },
                    { "6828054f-2951-402e-888c-747c4f78d782", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "f981eb82-90de-4183-a933-db7f5576e129", "guest13@mail.com", true, "guest13", "standart", false, null, null, "guest13@MAIL.COM", "AQAAAAEAACcQAAAAELDdZROpoJp6UHNEKMUeiduvVXyyP8gbhCqpyZ8gi+ZVzZLnrpg38RS/X7jzIAFt/g==", null, false, "1601ddba-5693-427c-9290-4990562f5075", false, "guest13@mail.com" },
                    { "c332e745-32e7-46c5-bc2c-2ae7d5a6e280", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "daea905a-ef6d-4359-884b-6e6f478a175f", "guest12@mail.com", true, "guest12", "standart", false, null, null, "guest12@MAIL.COM", "AQAAAAEAACcQAAAAEFI81hbTS2ApZ7Vm0mOZrhgBM9k0ZN8YWjEdYzbuGxyS/FWcz+Fntr4HEPg4W0zlnQ==", null, false, "276bf39d-99dc-449f-863c-0c1e54c98ce0", false, "guest12@mail.com" },
                    { "3e7940cf-24fd-4778-b7c0-192a78791d28", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "c5e1ea8a-c380-44f3-8f7c-897dc307ac96", "guest11@mail.com", true, "guest11", "standart", false, null, null, "guest11@MAIL.COM", "AQAAAAEAACcQAAAAEAL36CvVO2BVWE6/0XhzOJLjZJBJCJQiHuJgHn56A0CbjJw63DCfmCtjsrvzdMu4/g==", null, false, "946f90f8-9851-4677-9ce5-6ae64a139134", false, "guest11@mail.com" },
                    { "868ade20-84c5-4db3-8a8c-030c187d45b2", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "5bf573fb-4483-4ea0-b6aa-5aa55b948aef", "guest10@mail.com", true, "guest10", "standart", false, null, null, "guest10@MAIL.COM", "AQAAAAEAACcQAAAAEHNJrywMnb1IE57LdEHT5L+VX7w1UEkSIyJO1ofLTaxvb8G+z6eAu+c4tL3X91KU/g==", null, false, "8a119fa2-2719-4495-9757-1a07092f9770", false, "guest10@mail.com" },
                    { "f46f8be0-e2a0-4f1a-918b-618582ca0c29", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "598ad0d2-6da4-49e9-9c10-de87558b644b", "guest9@mail.com", true, "guest9", "standart", false, null, null, "guest9@MAIL.COM", "AQAAAAEAACcQAAAAEAHAmzf4FpNM3bqKNgMQQ2Hbr2VMEufEqZIdGepsGoBmo0Ko8pgPNasz85VGibt3rw==", null, false, "db439da1-61bb-4756-a36c-e5854dfd4472", false, "guest9@mail.com" },
                    { "5973dc53-9341-4b33-982e-8168dfaed098", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "f5b11c93-e7d6-4c60-ba01-e1ce95640d58", "guest7@mail.com", true, "guest7", "standart", false, null, null, "guest7@MAIL.COM", "AQAAAAEAACcQAAAAEJc9Sz2WCQVljLajg4Z4we5lnQxGfHLHT6uO0dVGYqN1zmG4w577a5I6WH7+YMWpjg==", null, false, "ed7bd2d7-d93f-4ae4-9f41-9e68fc00a9d6", false, "guest7@mail.com" },
                    { "f400b39a-9171-433f-a2c7-5a5107c14038", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "7b04d166-1e9d-424d-8440-19ce23904e05", "guest6@mail.com", true, "guest6", "standart", false, null, null, "guest6@MAIL.COM", "AQAAAAEAACcQAAAAEOP+T6LFkbMn2xsLXfGOo0cp1vlPXqxNWoVTxpoyXUxj+jKBZEWb6aoZaGWz0zDOFw==", null, false, "5122a6b3-5565-48ae-bdfa-c29e12ec52ac", false, "guest6@mail.com" },
                    { "dd59d864-4338-4d26-b5ec-c2ac2d6cd64e", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "20340e3c-2d5b-4a28-979f-eccb62f99da1", "guest5@mail.com", true, "guest5", "standart", false, null, null, "guest5@MAIL.COM", "AQAAAAEAACcQAAAAEEbDzbLEECKFvpokefWMmvZJIlkE8vSvQiX2EACJUfkVBDrd/2zF0srKhw/GvVF0mg==", null, false, "6d7e07e4-6ee1-4e9a-a365-d24609e81d1c", false, "guest5@mail.com" },
                    { "b63c23af-25df-468c-9947-1554c6de73b3", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "9023c433-3c34-49f3-b078-e784bb462632", "guest4@mail.com", true, "guest4", "standart", false, null, null, "guest4@MAIL.COM", "AQAAAAEAACcQAAAAEBT+KbUQZdEcIPpqDgq6ogP3wpq74wJP674s3Wc4d7fKlau5X6hFhIeBPZJKPsVNdQ==", null, false, "d07458c8-f0b0-419c-9a8f-bfd37a78265e", false, "guest4@mail.com" },
                    { "312f78f2-99d2-422b-a062-17edc44930b2", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "203b37ec-124d-4e3a-a2b2-08a1738b2f24", "guest3@mail.com", true, "guest3", "standart", false, null, null, "guest3@MAIL.COM", "AQAAAAEAACcQAAAAEKQ7Vmsn8EZgahGikZl/nr3ktA50pkcVh6/uHLIK3kNEdtVSBA4tq2H3/RH2Jin8sQ==", null, false, "5eee5735-81ef-4927-8dd1-07ea74a98552", false, "guest3@mail.com" },
                    { "7b3c6cfb-ec63-4544-8612-6d28e0f3bb0a", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "a1ed0885-3578-4909-ad17-f49fceec8c30", "guest2@mail.com", true, "guest2", "standart", false, null, null, "guest2@MAIL.COM", "AQAAAAEAACcQAAAAEG63pPsLKeuH623BljdeBZxxRkmCwwhD6c1F5AZMsZKwt8aXMWHy1NShB7hMumCWww==", null, false, "8b77dbcf-c00e-4bc5-a8c0-cf66a2675f0e", false, "guest2@mail.com" },
                    { "9937ec8b-8759-4cd8-8574-1711ef7952d1", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "6bfcee05-03a3-4134-9b7e-698041688187", "guest1@mail.com", true, "guest1", "standart", false, null, null, "guest1@MAIL.COM", "AQAAAAEAACcQAAAAEMqpt4H/vrfcjUa/aR6y9+usqa0kNwf0TjWYjpC8jmy6Kl2oEFOBnwSEQ83TODtFWw==", null, false, "08b7c5ab-e1fb-433b-865d-99cfddbcb7fd", false, "guest1@mail.com" },
                    { "79dcbef0-88e7-4ce0-aec0-f6b8c81a3984", 0, new DateTime(2000, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "1a8c5a28-f6ce-42a6-aa07-4f279a7ae2bd", "manager@manager.com", true, "manager", "manager", false, null, null, "MANAGER@MANAGER.COM", "AQAAAAEAACcQAAAAEBv/abwPeJGyNFv+Oi60ZIokBfET4J8tMauCMuJSibq42w69KT97fMmOMr4VELVgHw==", null, false, "6f74c636-f230-4624-a88a-18f1f9ce7455", false, "manager@manager.com" },
                    { "72a6510e-7885-4af7-830d-2284f78c586e", 0, new DateTime(1998, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "307eb9dd-3f12-423d-85ac-5a6b0395570d", "admin@admin.com", true, "admin", "admin", false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEErhuITLiE/EsXBAaxLvTNEzMuKD6t+EbuDN3v+WIlhJCpj1SfQH18h70zag3pcfmA==", null, false, "be67a7d6-b716-4156-a28b-1ae3c8f9a784", false, "admin@admin.com" },
                    { "3fadaf43-6511-481c-b9e2-b6983d4ea85e", 0, new DateTime(1999, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "e7fc7165-9966-4d0c-b430-d1d4bcec3b34", "guest8@mail.com", true, "guest8", "standart", false, null, null, "guest8@MAIL.COM", "AQAAAAEAACcQAAAAECE/FWRd7fxXP2dYlolnUrXYxnirRxcC0fDcvc3SAo6z3d82JPCgJsSa/cJ582nwQg==", null, false, "d8c40e00-217a-4364-ae21-d0f9cf05693d", false, "guest8@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Title", "TopicId" },
                values: new object[,]
                {
                    { 3, "Super JavaScript", "JavaScript", 0 },
                    { 2, "Super Spring", "Java", 0 },
                    { 1, "Super MVC", "ASP", 0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ffbc16ed-3ac4-41b2-9760-865ec3042cae", "72a6510e-7885-4af7-830d-2284f78c586e" },
                    { "fc4232ad-8e29-4ab2-8ed4-d71c54e8de9e", "79dcbef0-88e7-4ce0-aec0-f6b8c81a3984" }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CourseId", "FileId", "Name", "Theme" },
                values: new object[,]
                {
                    { 1, 1, null, "Lesson 1", "Super Lesson 1" },
                    { 2, 1, null, "Lesson 2", "Super Lesson 2" },
                    { 3, 1, null, "Lesson 3", "Super Lesson 3" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "CourseId", "Description", "ParentId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Super MVC", null, ".NET" },
                    { 2, 2, "Super Spring", null, "Spring" },
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
                column: "CourseId",
                unique: true);

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
                name: "StudentToGroupActions");

            migrationBuilder.DropTable(
                name: "Topics");

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
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
