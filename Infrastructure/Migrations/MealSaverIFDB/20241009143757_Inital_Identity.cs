using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.MealSaverIFDB
{
    /// <inheritdoc />
    public partial class Inital_Identity : Migration
    {
        /// <inheritdoc />
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9cc33e13-6c36-436b-b785-c3e89aaa59cb", null, "Student", "STUDENT" },
                    { "bd235c47-9a2f-4ec0-a952-0a1948a643b4", null, "CanteenEmployee", "CANTEENEMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3524ebb6-e080-41c4-950f-aecb500bb14d", 0, "165fcf03-46d9-41c1-ae65-6c23e9851060", "william.brown@example.com", false, false, null, "WILLIAM.BROWN@EXAMPLE.COM", "WILLIAM BROWN", "AQAAAAIAAYagAAAAEFzMUfV9gc6UTVvVhaNkU7ihdBVU1F4LuMqG8HJEiY2ZMYedeKY/tbet0JzJHO70SQ==", null, false, "5e15058f-f4a0-4157-a332-833acad1270c", false, "William Brown" },
                    { "5230f2b1-933d-4a02-a590-bbea9c7b3903", 0, "e67bbd9d-2358-4a05-875e-5ececd034f40", "john.doe@student.com", false, false, null, "JOHN.DOE@STUDENT.COM", "JOHN DOE", "AQAAAAIAAYagAAAAEDwnICtuUK65YWKtvTvqo+HRN6TAc3hsoXORUC7MfF1FiglIBltODt8zb/vREG3JiA==", null, false, "c8e75dd7-0c77-4e4e-9fcc-7d708b8bc4ef", false, "John Doe" },
                    { "906e0a92-970d-47d6-8bc5-34d3f8dfbea3", 0, "db9b7cb9-79b4-43a2-8914-31ed99976d6d", "alice.johnson@mail.com", false, false, null, "ALICE.JOHNSON@MAIL.COM", "ALICE", "AQAAAAIAAYagAAAAEN38QN+2JIHf6gFPguKUAYK9+tVDww8qMIweSiDRLr5w26taLFZgqBaESYF9+vaK4g==", null, false, "30c61126-f338-4db9-a3f2-0955da0cab58", false, "Alice" },
                    { "99717a7d-fedc-4db5-8707-8c67fda1b708", 0, "7180d0b6-3489-483a-838c-b5b85b9ef062", "michael.johnson@example.com", false, false, null, "MICHAEL.JOHNSON@EXAMPLE.COM", "MICHAEL JOHNSON", "AQAAAAIAAYagAAAAEGdqLrnRFrJ34umWzt1f5epCIsn4EnTfDk55wlYyl4BZ529O+oasDgv6xLnN6ED5gg==", null, false, "b7dc6fc8-4f4d-452c-9c4f-8072fdb47f5e", false, "Michael Johnson" },
                    { "9a18f83d-1894-447a-8b06-2258c01b30b9", 0, "293c8156-3569-4b1b-8b53-6b2a2da45cb3", "jane.smith@example.com", false, false, null, "JANE.SMITH@EXAMPLE.COM", "JANE SMITH", "AQAAAAIAAYagAAAAEKCB0cPePYlzvDsdVrpe7X6LgetWzcNwTcGAv3n6CdVlAT/eJdZW2PFZj7boWhaiDg==", null, false, "f7ce4d33-48d9-4211-9f1e-3664ebdc6478", false, "Jane Smith" },
                    { "9e01849c-f116-4999-b36d-ae7b5965622d", 0, "8f12ef69-f873-482f-a0db-3a2b3499ef8b", "emily.davis@example.com", false, false, null, "EMILY.DAVIS@EXAMPLE.COM", "EMILY DAVIS", "AQAAAAIAAYagAAAAEL1vt99y0mR+tDp+DRRi3UEZ3NSI+MB6yNqsT7p9hp4y9JxtjUi7SOYiNsAk6SMaig==", null, false, "81bd8d44-cc62-41f4-9e95-b8a22c0bb285", false, "Emily Davis" },
                    { "c9d88b8e-2f7a-4ded-89ba-1004721be682", 0, "e8e35286-94ac-4d13-b454-29bad4d6ff6a", "bob.smith@mail.com", false, false, null, "BOB.SMITH@MAIL.COM", "BOB", "AQAAAAIAAYagAAAAEFy0eCprZsShgNz2K9pUB0vj8NUGSzagAFIfJ30Xu4g5hl3u/WOIGxLXlN6r5cRrmw==", null, false, "0f5bf510-f245-4098-a413-c370fbe6ad89", false, "Bob" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9cc33e13-6c36-436b-b785-c3e89aaa59cb", "3524ebb6-e080-41c4-950f-aecb500bb14d" },
                    { "9cc33e13-6c36-436b-b785-c3e89aaa59cb", "5230f2b1-933d-4a02-a590-bbea9c7b3903" },
                    { "bd235c47-9a2f-4ec0-a952-0a1948a643b4", "906e0a92-970d-47d6-8bc5-34d3f8dfbea3" },
                    { "9cc33e13-6c36-436b-b785-c3e89aaa59cb", "99717a7d-fedc-4db5-8707-8c67fda1b708" },
                    { "9cc33e13-6c36-436b-b785-c3e89aaa59cb", "9a18f83d-1894-447a-8b06-2258c01b30b9" },
                    { "9cc33e13-6c36-436b-b785-c3e89aaa59cb", "9e01849c-f116-4999-b36d-ae7b5965622d" },
                    { "bd235c47-9a2f-4ec0-a952-0a1948a643b4", "c9d88b8e-2f7a-4ded-89ba-1004721be682" }
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
        }

        /// <inheritdoc />
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
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
