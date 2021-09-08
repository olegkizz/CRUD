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
                    { "8c7c8095-2336-43b3-9494-1828cec9924f", "a6bfc6ba-a913-4eec-ba75-c0f93ee517d6", "Admin", "ADMIN" },
                    { "69ee48cd-e2e2-4632-941b-77744320a070", "a504955e-c44f-439f-81a1-246f70f3b715", "Manager", "MANAGER" },
                    { "93a14dc9-a206-4b15-98d8-6e52f32616f4", "9de2ea9f-4558-4581-9e1b-e838e72b038e", "Student", "STUDENT" },
                    { "5f567727-6f15-4e9d-99da-98a00f365f94", "7b8a585f-a9a5-47ef-b77f-f313851bdedb", "Teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7b22d19f-eeca-4d21-8d4b-5d14dce78de9", 0, "d30bc011-d1fc-45e9-bd1a-6f48eac72e4b", "guest5@mail.com", true, false, null, null, "guest5@MAIL.COM", "AQAAAAEAACcQAAAAELEjOmkkTnlGLn1zly3zVIQ9/UkzdxSwFDSf7xfwYiKnUVmlbRRXrjjCbKYc6Pcicw==", null, false, "891c83c0-44c1-4238-b94f-9d0b5f89eff2", false, "guest5@mail.com" },
                    { "38e59ded-44c3-48ad-82f8-86b78308de4c", 0, "9d62d070-b4a4-4507-b3f5-a6798119af93", "guest17@mail.com", true, false, null, null, "guest17@MAIL.COM", "AQAAAAEAACcQAAAAEAxF27nEVwxTYbsHUH0CNrjWeAY15JgpDFRIQRQXVktpG97aTz/GLLz3mXWiPNsDLA==", null, false, "89b96c54-cc23-4e5f-8a52-3eb6918ac375", false, "guest17@mail.com" },
                    { "48d5174e-f0d0-4087-8e3b-b1028b8f048e", 0, "fa1810b6-480c-470a-8443-9dfa0764443c", "guest16@mail.com", true, false, null, null, "guest16@MAIL.COM", "AQAAAAEAACcQAAAAEEypFDzq3tIL1BszkMgohELo6e/A1mvB9yVOzOHY7vFJGALGJkMplhN7+qhKaPm0ig==", null, false, "9bba04ac-44b4-40a6-be2a-0712f9be2a71", false, "guest16@mail.com" },
                    { "71ac5a95-210a-4cee-9f1d-7dc828266a02", 0, "ce03f228-c161-4e4b-99d6-841246f92846", "guest15@mail.com", true, false, null, null, "guest15@MAIL.COM", "AQAAAAEAACcQAAAAEJp46RIYq7DASx0cMCVZ597qwSEVCVv7GYUi9+Gbhef2FR5FCP2u+HMTRJ02qwR7/g==", null, false, "1ea1db76-a399-4b29-9a04-2d5686b1be1a", false, "guest15@mail.com" },
                    { "635fc400-b452-4fc6-9d1b-6417156f09e3", 0, "407c4247-1ebb-4e78-8830-cdffaf7723fd", "guest14@mail.com", true, false, null, null, "guest14@MAIL.COM", "AQAAAAEAACcQAAAAELrWHmYG8jt7Ul0zWmKXknVIQdZ0vlr5hkfXZEa5v6m0yxF1DUkh15agdbZEoqS/qw==", null, false, "81bc2b8c-5a7b-4243-b66d-da7a9b60c6f7", false, "guest14@mail.com" },
                    { "2a295840-a4ec-43a7-9751-cd18e37e3cc9", 0, "1b23acb0-32eb-478f-af4f-6c2848dfb00c", "guest13@mail.com", true, false, null, null, "guest13@MAIL.COM", "AQAAAAEAACcQAAAAEOzJLIe/xuV7PzuU4Bkmkb6mBsAFNsYD4z1RSIYKaq4dRjVZkMzFN9bv36eX/VdtuA==", null, false, "7d46a2b7-eac7-4fab-8314-fc73d94f5f9e", false, "guest13@mail.com" },
                    { "66565893-ca7a-4452-9631-a1aeac089013", 0, "1b654e14-cf36-4aad-b5e4-fac0bf474e51", "guest12@mail.com", true, false, null, null, "guest12@MAIL.COM", "AQAAAAEAACcQAAAAEHzMgTRrkOrt/beuklZzcra/D7e9zW81L0A7bdpxt1U5Dl5CB/nLqb5bZSNlmDGVLg==", null, false, "37acbc39-eb6c-4c86-90db-7a50a8361ff8", false, "guest12@mail.com" },
                    { "d84fc4e1-1292-49b0-8b1e-74cd3a5afc77", 0, "26181ffc-cd83-4d04-b219-65781ef1919e", "guest11@mail.com", true, false, null, null, "guest11@MAIL.COM", "AQAAAAEAACcQAAAAEDokQyjoepqFU23LOKW0ZZfIc9PMiRpjHQZdmGJ0MH5yYakP60yYBgkHfkZFfoXWAw==", null, false, "902dd0f0-9c20-41dc-a857-56e89314d798", false, "guest11@mail.com" },
                    { "fdfbabd7-92a9-4cf0-949b-04ba5303a25f", 0, "2d1cfd66-c58c-47d1-8447-5caceefc9f6f", "guest10@mail.com", true, false, null, null, "guest10@MAIL.COM", "AQAAAAEAACcQAAAAEDdyMpPJaE2KtGGF7bld1Sq93AD9eZz9KEB+5zcTGUnAv+NAghMvcoK4Fc6Ft64Oew==", null, false, "4543b85b-4ae3-47fd-9e6f-6df15fc0f75b", false, "guest10@mail.com" },
                    { "7a3e0a03-0b8f-4bef-8d9f-8b4ead73bad6", 0, "e39e9ae1-fcc2-4d98-aece-4d808810ef33", "guest9@mail.com", true, false, null, null, "guest9@MAIL.COM", "AQAAAAEAACcQAAAAEGTj4wn7eENTlMFCMTiQIlW0Ts+kV9CIZUyAaDijqKQQDEkdwUeT+04mUxlLqBE3dQ==", null, false, "b705c9fa-76f8-48c2-a3af-58aefdc34855", false, "guest9@mail.com" },
                    { "f88c42ed-58cf-4f92-b1b0-6fa4ebaa65a7", 0, "3540ed14-6bc7-4c20-8f57-fe46700f53ac", "guest8@mail.com", true, false, null, null, "guest8@MAIL.COM", "AQAAAAEAACcQAAAAEJUFV5Q/KwzZMQ7LC4HWQrF6SfLj1GaW9aGWBAECSItShj770ziL1xdNdnqsfU8Cow==", null, false, "c8623f8a-6c65-4d56-994f-351a25ef2504", false, "guest8@mail.com" },
                    { "99770179-1e1a-4484-8bb7-77d7d4b12e76", 0, "d4f1c3f0-b3b3-4c6b-9778-9623250e1d6f", "guest19@mail.com", true, false, null, null, "guest19@MAIL.COM", "AQAAAAEAACcQAAAAEAFVqEqrGu+ZM7JCENbsBNHYsZ7BvI2BQr7YqAXlcdMqElnopA0+vO2AYM6O12iTtA==", null, false, "aa7e59e1-cc11-4f01-bd99-d192590ff3b5", false, "guest19@mail.com" },
                    { "c04e5168-84b1-4967-a57f-1d7a3265185a", 0, "7632b4a8-5b56-4bbd-9502-0f544df207fd", "guest6@mail.com", true, false, null, null, "guest6@MAIL.COM", "AQAAAAEAACcQAAAAENnUHRNZe8fBH3loJ0b74/2fgk51TO7gGDjaLYmfGfBrC3FaPpjbV4YULnyeoiNnQQ==", null, false, "7e8dbfd2-fbf2-40d0-b525-711a40dc8c35", false, "guest6@mail.com" },
                    { "87987032-3400-41db-bd2f-7a75a011d4da", 0, "c6e3a4fd-9da6-48cc-90e8-cf04e1037bf5", "guest18@mail.com", true, false, null, null, "guest18@MAIL.COM", "AQAAAAEAACcQAAAAEKi26ABVVdYzf91V6p+YOiWfvzg0rxo6LQD379SvQ5M0p9XyGw1cAa3T0HsHa7n8gA==", null, false, "2d3f1513-e5a7-4954-b837-41e0b675b267", false, "guest18@mail.com" },
                    { "7c19351b-6c51-46e7-8675-461e77553e85", 0, "8aecf1de-922b-49fc-b623-20679ea32159", "guest4@mail.com", true, false, null, null, "guest4@MAIL.COM", "AQAAAAEAACcQAAAAEB324aZGtgU8tY9G8dHgPTKwQqK5UodYacxf1jFXWt6r8lS5yCfV+9kJ+K20LPJ7bA==", null, false, "16e77c63-4163-400c-8558-5f23e6795903", false, "guest4@mail.com" },
                    { "c3dafc38-6e05-46a1-8673-b1cbf66bddf1", 0, "563d98a6-0c0c-4672-b4ca-23763aaa8aa7", "guest3@mail.com", true, false, null, null, "guest3@MAIL.COM", "AQAAAAEAACcQAAAAENX5qI2b7EeJQLieeLzNOajMmtUysWS0dRkdbbGG9u2Q0EQGHJVM9Wpq3GMdwXRysA==", null, false, "ad2ede27-05ab-47e8-a0ad-c84508c53bb5", false, "guest3@mail.com" },
                    { "03c7522d-4fb9-44ca-95a3-5941e872dbe7", 0, "4b556c93-d887-4272-8dac-6263da98501b", "guest2@mail.com", true, false, null, null, "guest2@MAIL.COM", "AQAAAAEAACcQAAAAENjkOBZRxXHDO6C2F0yzzADuaeapuZI0kbHVatSFWcussqR2QkZQgfu1ebfDN15pDg==", null, false, "4643ff9e-cb1b-451e-be2c-1259865960e8", false, "guest2@mail.com" },
                    { "ac3f39a1-98aa-4db4-92fc-7b4e2bf33a88", 0, "b3ccda03-74a9-45e1-9584-933c2c1c5f6c", "guest1@mail.com", true, false, null, null, "guest1@MAIL.COM", "AQAAAAEAACcQAAAAEKML2avxRsjBMpHQDwDsfHF1fH1PNE3tGLlsLi4kOIy7KGSjnCJ5LQEJyC6cztbTOg==", null, false, "75aa0d40-c7ab-4f30-93d0-bb81d3f1ce8c", false, "guest1@mail.com" },
                    { "8e1cff68-a820-4bc9-8569-d074545d2f4d", 0, "a82f25b3-1a72-4788-9bc1-37b22ffc7cfb", "manager@manager.com", true, false, null, null, "MANAGER@MANAGER.COM", "AQAAAAEAACcQAAAAEHH6pj7jKPNeUM4gAJzxpiTssbO3frzDQynUGXQzLJSbCF4i13fUW8Co+TDliz5UxQ==", null, false, "13ce17e0-99b7-4a46-bbe2-5659402d40ae", false, "manager@manager.com" },
                    { "a7504080-eb7c-452c-b703-72f7fc747273", 0, "833ea57a-3bfa-4406-ae3e-f7bcc69d91b8", "admin@admin.com", true, false, null, null, "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEIfJ0yai7QhVQryUH/IxZo/AxrQ61KHyY6MXM3vbQy9c7f3VrPpKeVjXAujYUEoC4g==", null, false, "3ba542ab-c979-45c5-8ba3-0be099869d64", false, "admin@admin.com" },
                    { "7fac2d51-a858-49a1-9ef7-dd5348b818e1", 0, "2e0dc051-63ae-462b-88d8-57ef4ba20ed1", "guest7@mail.com", true, false, null, null, "guest7@MAIL.COM", "AQAAAAEAACcQAAAAEKkGWC0Y7nM0u1lFKvu726P8GzpmLu/XSOurMHOOq3bopnSwFH/Xm/0hqzKMaNResQ==", null, false, "a2591e74-3543-4e5d-a7a4-7ccfd2460643", false, "guest7@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Program", "StartDate", "Title", "TopicId" },
                values: new object[,]
                {
                    { 3, "Super JavaScript", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JavaScript", 3 },
                    { 2, "Super Spring", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Java", 2 },
                    { 1, "Super MVC", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "8c7c8095-2336-43b3-9494-1828cec9924f", "a7504080-eb7c-452c-b703-72f7fc747273" },
                    { "69ee48cd-e2e2-4632-941b-77744320a070", "8e1cff68-a820-4bc9-8569-d074545d2f4d" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "CourseId", "Description", "ParentId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Super MVC", null, ".NET" },
                    { 2, 2, "Super Spring", null, "Spring" },
                    { 3, 3, "Super ReactJS", null, "ReactJS" }
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
