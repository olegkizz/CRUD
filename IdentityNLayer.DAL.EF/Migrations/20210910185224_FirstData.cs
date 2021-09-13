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
                    { "f974cfc9-615e-40ca-9be9-7c9b73ee80be", "0845952e-d55d-4f0e-894d-0044fb11ead4", "Admin", "ADMIN" },
                    { "9139cbbb-2459-4edc-b1e7-5066480bc2f4", "5393bf86-6f1d-42b5-aecd-fd8dd286b561", "Manager", "MANAGER" },
                    { "b670ed94-2ce7-4759-8e6b-afb0f705b48c", "715de2cd-3e39-4216-b52c-a94c4896f4ee", "Student", "STUDENT" },
                    { "90066eaa-99a5-4968-be1f-b423c3a070e6", "ec7d6dfd-4c69-4422-9ec4-3dac567c2e8e", "Teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a4e3680b-6791-45ed-b2ec-8dbee81577a5", 0, "8f1e315f-ce48-4103-b2a8-fecd70d26f1f", "guest5@mail.com", true, false, null, null, "guest5@MAIL.COM", "AQAAAAEAACcQAAAAEI2pru7JkuBdc7zfD2LZwKBO8JpJw2EhwAuN22W7Mj1mfWEbiRQMU5Huof7KMxkNNg==", null, false, "2e326771-b0d3-4e98-8a01-33acc71e5331", false, "guest5@mail.com" },
                    { "891acd0b-40e2-4b44-aa86-e33b964ce923", 0, "99c03f6d-4451-48b8-8348-d09fbb2ef734", "guest17@mail.com", true, false, null, null, "guest17@MAIL.COM", "AQAAAAEAACcQAAAAEKDUgqNU9FdP1HWEkK+kTxUuvkbSEg8XBaiHKORC+HdkR2wSkhNe8m8SQTT08PcABQ==", null, false, "41767906-78bd-4dfd-8aa7-2f9578366b13", false, "guest17@mail.com" },
                    { "36b4c48b-18cd-46e3-b1c2-e3cb66acc40b", 0, "0ffa9b86-596f-4053-9ed5-30f4f23448b9", "guest16@mail.com", true, false, null, null, "guest16@MAIL.COM", "AQAAAAEAACcQAAAAECoWGvC66wJl3y8/7ClFiIeRzUjC2bXgI3rQAiXEmOe0oB+r4zUF2prhWiqGWvnWCA==", null, false, "82cfa8c3-7a31-43a1-83e1-7a83ad765108", false, "guest16@mail.com" },
                    { "dc3ac115-a993-4e5b-b675-f92bf5053ba3", 0, "1c6537b7-a5ea-453d-8195-b6e10b9744a3", "guest15@mail.com", true, false, null, null, "guest15@MAIL.COM", "AQAAAAEAACcQAAAAEFeRFZYljSXdbyLba7Rvq73OmTS8RZpgzPlIdCwT/OVzJWFEB1zbhtPIYHzhM+yeZw==", null, false, "dc3801e1-ca89-42b6-b625-907799ed2490", false, "guest15@mail.com" },
                    { "dcc7b531-1b55-41af-8819-0ab4478b65e3", 0, "5f8dca84-37c3-46f4-96e0-5af3e9d85ae2", "guest14@mail.com", true, false, null, null, "guest14@MAIL.COM", "AQAAAAEAACcQAAAAEMv5SQU78wXva8cjy3DaGapR5Gfg0AUHeukFpQvtRZf1tPJ7EAgTXBypzi+gsugGEw==", null, false, "abbbc3c9-ff60-4fcf-ae9a-73c6a7526c95", false, "guest14@mail.com" },
                    { "1a04fe4f-60aa-460d-8fe0-06a0c5aaaceb", 0, "8479d6c6-318f-46fa-b3a5-5d9e7ac97c4f", "guest13@mail.com", true, false, null, null, "guest13@MAIL.COM", "AQAAAAEAACcQAAAAEB6cOCH0KykM1MX7RJlQpggHXU17HgvabQPvFEaWQ0oPITo6TSabWjzNSghCK5lM3w==", null, false, "7817d6dc-3d3e-4530-87d6-83ba1ffdabc0", false, "guest13@mail.com" },
                    { "f8f6db97-1c7b-429d-b94a-3da89b3419f8", 0, "7c5c1a6b-58cf-462b-8475-96db91c8c1fc", "guest12@mail.com", true, false, null, null, "guest12@MAIL.COM", "AQAAAAEAACcQAAAAEJmpSMO2QX0TlmWsKlXq72xqOVKwHSlgj6ZoSbrW1ApKkVElryQz/fbrLFFR5GomUw==", null, false, "e28d4bad-c027-4b3a-af3b-8c6b0464a1a1", false, "guest12@mail.com" },
                    { "817db1aa-7569-4c74-915a-451779e30be3", 0, "355be7b2-fd2a-4011-a400-6850f689bac4", "guest11@mail.com", true, false, null, null, "guest11@MAIL.COM", "AQAAAAEAACcQAAAAEL2dxw/NFnEnvlihyU8T1EpWdjhVFfGNxOHMD4yyL60TTKETvhwoKcACLRPC4ym9dw==", null, false, "972a55db-6cfe-4422-bd74-0d2191275646", false, "guest11@mail.com" },
                    { "af199eb4-3433-4856-af06-e7654b9d5bf7", 0, "3221fabc-ceb6-430e-975c-e7fe22cce4e7", "guest10@mail.com", true, false, null, null, "guest10@MAIL.COM", "AQAAAAEAACcQAAAAEMNZzt5OA7OHoGibDHCuczapzlME3mC82bpSGNGsNlkavcs+seHpXbxLReLo662gxg==", null, false, "8f4c6ddd-c71d-4ea0-b10f-0ce033a9a2dd", false, "guest10@mail.com" },
                    { "06396765-0b99-48e3-8ade-b4d8b1ac6c73", 0, "a9daee95-decb-48c1-83d3-5ffc1f2e9c5f", "guest9@mail.com", true, false, null, null, "guest9@MAIL.COM", "AQAAAAEAACcQAAAAEPp+2J+xvg2Wg9KP3WNWl/D7liDlYsEFWsOKRapfx5xeewFouWpnp4P4FdhJb+9qUg==", null, false, "b80de4cd-6144-4ae0-8054-98c515718a42", false, "guest9@mail.com" },
                    { "dc66f024-8723-4b24-94fa-57cc75b92fda", 0, "ffce8b0d-a8e7-4180-a1fd-b7740df683e1", "guest8@mail.com", true, false, null, null, "guest8@MAIL.COM", "AQAAAAEAACcQAAAAEPoCZHx3fOyo20lvWV2Ee3e4EaDRFDrVDoraV/T0GlG2IpkPhG3yBx6TB3mRyxT4rA==", null, false, "089f9b7d-60a3-4f18-9a45-6cffce25de9d", false, "guest8@mail.com" },
                    { "fb95a654-d906-49b5-8247-b827c1dc0106", 0, "4a7e7047-5cc5-404f-99e8-40b26f6e1fcc", "guest19@mail.com", true, false, null, null, "guest19@MAIL.COM", "AQAAAAEAACcQAAAAEFXrTwDxkuLos0ln+jQBA1tPvprfn/c7iVZjWzetDCY2rJG09zWxIU5qvxYexX/fiA==", null, false, "effb2556-6f07-4540-8c79-45ba2f668f8e", false, "guest19@mail.com" },
                    { "3f39d911-b85b-4c2f-8f56-c72a08421e57", 0, "651edb3c-676a-41e6-9502-4706a8b2288c", "guest6@mail.com", true, false, null, null, "guest6@MAIL.COM", "AQAAAAEAACcQAAAAEL09IKIrSNP+UhzQ5RUkekVrJZ6KYESStc8KfJHfjHpP90/pBBYAMfdOaLsje2peZg==", null, false, "3120089b-4102-45eb-a657-d4903edbd8ba", false, "guest6@mail.com" },
                    { "b5d12a7b-9012-40b4-9be4-c60edc0752f8", 0, "d5457c0a-ec54-4c8a-8996-58ead37411f5", "guest18@mail.com", true, false, null, null, "guest18@MAIL.COM", "AQAAAAEAACcQAAAAEGgbVUf2D5z4czdBwa5e5NYC7lMsM5TDNTErE/1Z7CubQQ3hcSl9eANDE6CuXts7QA==", null, false, "a65566f2-f23e-4a48-8351-d02e816aec17", false, "guest18@mail.com" },
                    { "b39932d7-4db7-4674-a3f2-fe00c72f8748", 0, "0cbe9e17-a432-4dd1-b841-b23c18428c27", "guest4@mail.com", true, false, null, null, "guest4@MAIL.COM", "AQAAAAEAACcQAAAAEFYXCCB2L8P3OkhxfgT3hLl/wey/EaeVvAaa0OXFwqxys/VPsurIOHUCuPtAms+x2g==", null, false, "86650fde-bfa4-4056-9b23-92fced99525b", false, "guest4@mail.com" },
                    { "9a64d4a7-d6f4-4d50-8a6b-b9ca32ed5d87", 0, "665c36ee-fcd4-4a96-b63c-b04d69039630", "guest3@mail.com", true, false, null, null, "guest3@MAIL.COM", "AQAAAAEAACcQAAAAENLjRgMkmisw6x3pZVKL5sbbDStXEnByWUIX9hHVO90ekaP+tEswUMQhIEAEd2zU2Q==", null, false, "f4ebf4ed-3bb5-486d-8590-d3f02d36343e", false, "guest3@mail.com" },
                    { "83176973-1496-4fb8-8e0f-b4f9b651ab01", 0, "3af4dbff-4f6f-4c2f-9aff-356da40bc4eb", "guest2@mail.com", true, false, null, null, "guest2@MAIL.COM", "AQAAAAEAACcQAAAAEHFxwwwr/Pmao2ZuuMlsQpM0EUt4IDj7RimEmuWqTKng/pA8MCc0bWHz0efiLBAEdQ==", null, false, "8ce84607-8197-42f2-a143-58a13d79b6bb", false, "guest2@mail.com" },
                    { "5aa7651c-a469-4995-a9ba-a3724644882a", 0, "3f412604-38e1-45eb-946e-8634d7959a69", "guest1@mail.com", true, false, null, null, "guest1@MAIL.COM", "AQAAAAEAACcQAAAAEHvOj/oA6gL1tpqt56g5qj2upakDwcm8Ifnnzx5p2s8EBT8acP/YNZwUMyNqHiPdIw==", null, false, "5d5342d7-7e99-4027-b216-6e5e03dd1555", false, "guest1@mail.com" },
                    { "8ba459fd-a82a-444d-8283-028b5f9cb501", 0, "5a435f49-06c7-47b7-9c8b-e332e2766425", "manager@manager.com", true, false, null, null, "MANAGER@MANAGER.COM", "AQAAAAEAACcQAAAAEGoheJdd302ZvFg4QoeKflONDYYIRxNj0UwsF1CPIE0Y5i/iHLdP4BK24s6kq1MXTQ==", null, false, "50c2e4be-cebe-4798-bc36-0fc0ef3dbee2", false, "manager@manager.com" },
                    { "87b8ae22-f7bb-4a34-8305-b67d4ca8cf24", 0, "549c13bc-5f91-45f7-a8a0-6dd267e87192", "admin@admin.com", true, false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEMHDV1lMWYBbXobHEiiTeP6dbnNgfZWsXscxcc2VOvE6DFvj0JwlQUJwJfrxKe1tuA==", null, false, "56247421-dbef-4202-ad49-ef736f3234ff", false, "admin@admin.com" },
                    { "2c83f54e-49fe-4619-8046-806af9473a24", 0, "2ba8c7c1-b36e-4fdd-a211-9db009031ba5", "guest7@mail.com", true, false, null, null, "guest7@MAIL.COM", "AQAAAAEAACcQAAAAENbo5kcPRQLmg4yMw3u3AheL8ux+QyMbteeYLfr1DNbFwFuLv9RuH3ja5hmJ7LJ/jw==", null, false, "e5714087-c207-4091-81a3-b71fe614b545", false, "guest7@mail.com" }
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
                    { "f974cfc9-615e-40ca-9be9-7c9b73ee80be", "87b8ae22-f7bb-4a34-8305-b67d4ca8cf24" },
                    { "9139cbbb-2459-4edc-b1e7-5066480bc2f4", "8ba459fd-a82a-444d-8283-028b5f9cb501" }
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
