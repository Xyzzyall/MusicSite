using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicSite.Server.Migrations
{
    public partial class Version3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Name", "Rights", "Secret" },
                values: new object[] { -1, "test", "test1;test2", "test" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
