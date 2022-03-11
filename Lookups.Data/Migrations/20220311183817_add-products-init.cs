using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orders.Data.Migrations
{
    public partial class addproductsinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    TotalPayment = table.Column<float>(type: "real", nullable: false),
                    Change = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameFl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameSl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NameFl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameSl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    AmountInStock = table.Column<int>(type: "int", nullable: false),
                    RemainAmountInStock = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "NameFl", "NameSl" },
                values: new object[] { 1, "Edible", "Edible" });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "NameFl", "NameSl" },
                values: new object[] { 2, "Donated", "Donated" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AmountInStock", "Code", "CreatedDate", "Currency", "IsDelete", "ModifiedDate", "NameFl", "NameSl", "Price", "ProductTypeId", "RemainAmountInStock" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), 48, "SL-BR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "c", false, null, "Brownie", "Brownie", 65f, 1, 0 },
                    { new Guid("10000000-0000-0000-0000-000000000002"), 36, "SL-MF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "�", false, null, "Muffin", "Muffin", 1f, 1, 0 },
                    { new Guid("10000000-0000-0000-0000-000000000003"), 24, "SL-CP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "�", false, null, "Cake Pop", "Cake Pop", 1.35f, 1, 0 },
                    { new Guid("10000000-0000-0000-0000-000000000004"), 60, "SL-AT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "�", false, null, "Apple Tart", "Apple Tart", 1.5f, 1, 0 },
                    { new Guid("10000000-0000-0000-0000-000000000005"), 30, "SL-WR", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "�", false, null, "Water", "Water", 1.5f, 1, 0 },
                    { new Guid("20000000-0000-0000-0000-000000000001"), 0, "DN-ST", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "�", false, null, "Shirt", "Shirt", 2f, 2, 0 },
                    { new Guid("20000000-0000-0000-0000-000000000002"), 0, "DN-PN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "�", false, null, "Pants", "Pants", 3f, 2, 0 },
                    { new Guid("20000000-0000-0000-0000-000000000003"), 0, "DN-JK", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "�", false, null, "Jacket", "Jacket", 4f, 2, 0 },
                    { new Guid("20000000-0000-0000-0000-000000000004"), 0, "DN-TY", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "�", false, null, "Toy", "Toy", 1f, 2, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId1",
                table: "OrderDetails",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId1",
                table: "OrderDetails",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
