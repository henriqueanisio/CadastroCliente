using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroCliente.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthKey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Key = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthKey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Logo = table.Column<string>(type: "VARCHAR(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Neighborhood = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    Complement = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_ClientId",
                table: "Address",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "AuthKey");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
