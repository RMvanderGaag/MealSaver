using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Canteens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<int>(type: "int", nullable: false),
                    OffersWarmMeals = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canteens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DescriptiveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContainsAlcohol = table.Column<bool>(type: "bit", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CanteenEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanteenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanteenEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CanteenEmployees_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DescriptiveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanteenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PickupTimeFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PickupTimeTill = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Is18Plus = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealType = table.Column<int>(type: "int", nullable: false),
                    ReservedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealPackages_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealPackages_Students_ReservedById",
                        column: x => x.ReservedById,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MealPackage_Product",
                columns: table => new
                {
                    MealPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPackage_Product", x => new { x.MealPackageId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_MealPackage_Product_MealPackages_MealPackageId",
                        column: x => x.MealPackageId,
                        principalTable: "MealPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealPackage_Product_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Canteens",
                columns: new[] { "Id", "City", "Location", "OffersWarmMeals" },
                values: new object[,]
                {
                    { new Guid("50edac11-0d16-4733-ad00-a95c6faae7be"), 0, 1, false },
                    { new Guid("6671b047-37e2-437c-8e4b-d9918ca2fd1d"), 0, 2, true },
                    { new Guid("b7793f12-f278-4673-adf7-b2806572d02d"), 0, 0, true }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ContainsAlcohol", "DescriptiveName", "Photo" },
                values: new object[,]
                {
                    { new Guid("1ed81d6a-7e1e-4417-b74b-65f2dd77d4fe"), true, "Hertog Jan", "https://i.imgur.com/InH4TUw.jpg" },
                    { new Guid("3e3b72d4-b617-43c7-bd87-5d52fea507ff"), false, "Kaiserbroodje", "https://i.imgur.com/rMdiQiP.png" },
                    { new Guid("b3f96261-1a7e-483c-b1de-8d166983ea76"), false, "Appel", "https://i.imgur.com/Dy86B5w.png" },
                    { new Guid("e0636857-29ba-4a5f-a127-18acab5f4571"), false, "Peer", "https://i.imgur.com/HLRqlU9.png" },
                    { new Guid("eeb5e5d1-3ad8-4501-b633-7cd2ae560380"), false, "Broodje Frikandel", "https://i.imgur.com/G6puzUN.png" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "City", "Email", "Name", "PhoneNumber", "StudentNumber" },
                values: new object[,]
                {
                    { new Guid("1269dae6-4db3-4259-b8a1-1a3fb5431a3d"), new DateTime(1996, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "michael.johnson@example.com", "Michael Johnson", "0612345678", "STU003" },
                    { new Guid("28a3049a-76a7-46d9-8b9b-20d03b8a1b65"), new DateTime(1995, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "john.doe@student.com", "John Doe", "0612345678", "STU001" },
                    { new Guid("41a4684d-be7d-4c61-b6d7-ffdbf5641901"), new DateTime(2005, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "emily.davis@example.com", "Emily Davis", "0612345678", "STU004" },
                    { new Guid("559ad4b4-94a7-499f-89bc-c85daa2d2ea0"), new DateTime(1999, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "william.brown@example.com", "William Brown", "0612345678", "STU005" },
                    { new Guid("b49371d6-e10a-44d6-9de8-00c75a755bac"), new DateTime(1997, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "jane.smith@example.com", "Jane Smith", "0612345678", "STU002" }
                });

            migrationBuilder.InsertData(
                table: "CanteenEmployees",
                columns: new[] { "Id", "CanteenId", "Email", "EmployeeNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("67ae4cb4-6ad8-4407-b080-61c6f0de37df"), new Guid("b7793f12-f278-4673-adf7-b2806572d02d"), "alice.johnson@mail.com", "EMP001", "Alice Johnson" },
                    { new Guid("6c282c84-423e-4404-89e1-125b6b1da3bb"), new Guid("50edac11-0d16-4733-ad00-a95c6faae7be"), "bob.smith@mail.com", "EMP002", "Bob Smith" }
                });

            migrationBuilder.InsertData(
                table: "MealPackages",
                columns: new[] { "Id", "CanteenId", "DescriptiveName", "Is18Plus", "MealType", "PickupTimeFrom", "PickupTimeTill", "Price", "ReservedById" },
                values: new object[,]
                {
                    { new Guid("3c9a7cfb-1274-4f2e-8a22-ea379e19ac30"), new Guid("6671b047-37e2-437c-8e4b-d9918ca2fd1d"), "Dinner Combo", false, 2, new DateTime(2024, 10, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 1, 19, 30, 0, 0, DateTimeKind.Unspecified), 12.99m, null },
                    { new Guid("73e7c6a3-ad88-4e05-a915-910882174434"), new Guid("b7793f12-f278-4673-adf7-b2806572d02d"), "Snack Pack", false, 3, new DateTime(2024, 10, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 1, 16, 30, 0, 0, DateTimeKind.Unspecified), 3.49m, null },
                    { new Guid("ade3b656-67d4-41cf-9e5d-a3bf3567567d"), new Guid("50edac11-0d16-4733-ad00-a95c6faae7be"), "Lunch Special", false, 1, new DateTime(2024, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 1, 13, 30, 0, 0, DateTimeKind.Unspecified), 7.49m, null },
                    { new Guid("e79d1691-8cb9-4c7c-8c30-a74bcc2334e7"), new Guid("b7793f12-f278-4673-adf7-b2806572d02d"), "Healthy Breakfast", false, 0, new DateTime(2024, 10, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 1, 9, 30, 0, 0, DateTimeKind.Unspecified), 5.99m, null },
                    { new Guid("f27d3465-03f4-4d2d-86ad-16b882ef29e1"), new Guid("6671b047-37e2-437c-8e4b-d9918ca2fd1d"), "Evening Beers", true, 4, new DateTime(2024, 10, 1, 20, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 10, 1, 22, 30, 0, 0, DateTimeKind.Unspecified), 8.99m, null }
                });

            migrationBuilder.InsertData(
                table: "MealPackage_Product",
                columns: new[] { "MealPackageId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("3c9a7cfb-1274-4f2e-8a22-ea379e19ac30"), new Guid("1ed81d6a-7e1e-4417-b74b-65f2dd77d4fe") },
                    { new Guid("3c9a7cfb-1274-4f2e-8a22-ea379e19ac30"), new Guid("3e3b72d4-b617-43c7-bd87-5d52fea507ff") },
                    { new Guid("ade3b656-67d4-41cf-9e5d-a3bf3567567d"), new Guid("3e3b72d4-b617-43c7-bd87-5d52fea507ff") },
                    { new Guid("ade3b656-67d4-41cf-9e5d-a3bf3567567d"), new Guid("eeb5e5d1-3ad8-4501-b633-7cd2ae560380") },
                    { new Guid("e79d1691-8cb9-4c7c-8c30-a74bcc2334e7"), new Guid("b3f96261-1a7e-483c-b1de-8d166983ea76") },
                    { new Guid("e79d1691-8cb9-4c7c-8c30-a74bcc2334e7"), new Guid("e0636857-29ba-4a5f-a127-18acab5f4571") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CanteenEmployees_CanteenId",
                table: "CanteenEmployees",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPackage_Product_ProductId",
                table: "MealPackage_Product",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPackages_CanteenId",
                table: "MealPackages",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_MealPackages_ReservedById",
                table: "MealPackages",
                column: "ReservedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanteenEmployees");

            migrationBuilder.DropTable(
                name: "MealPackage_Product");

            migrationBuilder.DropTable(
                name: "MealPackages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Canteens");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
