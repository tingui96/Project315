using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project315.Migrations
{
    /// <inheritdoc />
    public partial class initRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53f28b1e-edd8-42a9-9c48-59e8b9405b91", null, "Viewer", "VIEWER" },
                    { "6f1c4d78-95db-4fcd-b922-e4ca9173f742", null, "Administrador", "ADMINISTRADOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53f28b1e-edd8-42a9-9c48-59e8b9405b91");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f1c4d78-95db-4fcd-b922-e4ca9173f742");
        }
    }
}
