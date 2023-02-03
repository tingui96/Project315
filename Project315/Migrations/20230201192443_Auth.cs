using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project315.Migrations
{
    /// <inheritdoc />
    public partial class Auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedido_shoppycar_ShoppyCarId",
                table: "pedido");

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "shoppycar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "shoppycar",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShoppyCarId",
                table: "pedido",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Usuario = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rol = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_shoppycar_UserId",
                table: "shoppycar",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_shoppycar_ShoppyCarId",
                table: "pedido",
                column: "ShoppyCarId",
                principalTable: "shoppycar",
                principalColumn: "ShoppyCarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shoppycar_user_UserId",
                table: "shoppycar",
                column: "UserId",
                principalTable: "user",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedido_shoppycar_ShoppyCarId",
                table: "pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_shoppycar_user_UserId",
                table: "shoppycar");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropIndex(
                name: "IX_shoppycar_UserId",
                table: "shoppycar");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "shoppycar");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "shoppycar");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShoppyCarId",
                table: "pedido",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_shoppycar_ShoppyCarId",
                table: "pedido",
                column: "ShoppyCarId",
                principalTable: "shoppycar",
                principalColumn: "ShoppyCarId");
        }
    }
}
