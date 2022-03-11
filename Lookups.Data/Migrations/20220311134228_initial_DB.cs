using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orders.Data.Migrations
{
    public partial class initial_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NameFl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameSl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenderNameFl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GenderNameSl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsShown = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "CreatedDate", "IsDelete", "ModifiedDate", "NameFl", "NameSl" },
                values: new object[,]
                {
                    { new Guid("5c60f693-bef5-e011-a485-80ee7300c695"), null, new DateTime(2022, 3, 11, 15, 42, 27, 847, DateTimeKind.Local).AddTicks(9139), false, null, "Egypt", "مصر" },
                    { new Guid("5c60f693-bef5-e011-a485-80ee7300c696"), null, new DateTime(2022, 3, 11, 15, 42, 27, 849, DateTimeKind.Local).AddTicks(2154), false, null, "Kuwait", "الكويت" }
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "CreatedDate", "GenderNameFl", "GenderNameSl", "IsDelete", "IsShown", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("1160f693-bef5-e011-a485-80ee7300c611"), new DateTime(2022, 3, 11, 15, 42, 27, 851, DateTimeKind.Local).AddTicks(8129), "Male", "ذكر", false, true, null },
                    { new Guid("5160f693-bef5-e011-a485-80ee7300c612"), new DateTime(2022, 3, 11, 15, 42, 27, 853, DateTimeKind.Local).AddTicks(2246), "Female", "أنثي", false, true, null },
                    { new Guid("2260f693-bef5-e011-a485-80ee7300c693"), new DateTime(2022, 3, 11, 15, 42, 27, 853, DateTimeKind.Local).AddTicks(3052), "Both", "كلاهما", false, false, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Gender");
        }
    }
}
