using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechChallenge.AutomotiveMechanics.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class startproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(1)"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(1)"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Model",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(1)"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Model_Manufacturer",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    YearManufactured = table.Column<int>(type: "int", nullable: false),
                    Plate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(1)"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Car_Model",
                        column: x => x.ModelId,
                        principalTable: "Model",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(1)"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Car_CarId",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Manufacturer",
                columns: new[] { "Id", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, null, "BMW" },
                    { 2, null, "VW" },
                    { 3, null, "Hyundai" }
                });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "Id", "CarId", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, null, null, "Troca de Óleo" },
                    { 2, null, null, "Troca de Pneu" },
                    { 3, null, null, "Troca de Filtro" },
                    { 4, null, null, "Troca de Pastilha de Freio" },
                    { 5, null, null, "Troca de Correia Dentada" },
                    { 6, null, null, "Troca de Amortecedor" },
                    { 7, null, null, "Troca de Embreagem" },
                    { 8, null, null, "Troca de Bateria" },
                    { 9, null, null, "Troca de Vela" },
                    { 10, null, null, "Troca de Cabo de Vela" }
                });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "LastModifiedDate", "ManufacturerId", "Name" },
                values: new object[,]
                {
                    { 1, null, 1, "X5" },
                    { 2, null, 1, "X6" },
                    { 3, null, 1, "X1" },
                    { 4, null, 1, "X2" },
                    { 5, null, 1, "X3" },
                    { 6, null, 1, "320I" },
                    { 7, null, 1, "330I" },
                    { 8, null, 1, "M3" },
                    { 9, null, 2, "Golf" },
                    { 10, null, 2, "Polo" },
                    { 11, null, 2, "Passat" },
                    { 12, null, 2, "Tiguan" },
                    { 13, null, 2, "Touareg" },
                    { 14, null, 2, "Arteon" },
                    { 15, null, 2, "T-Roc" },
                    { 16, null, 2, "T-Cross" },
                    { 17, null, 2, "Up" },
                    { 18, null, 2, "Amarok" },
                    { 19, null, 2, "Caddy" },
                    { 20, null, 2, "Transporter" },
                    { 21, null, 3, "i30" },
                    { 22, null, 3, "Elantra" },
                    { 23, null, 3, "Kona" },
                    { 24, null, 3, "Tucson" },
                    { 25, null, 3, "Santa Fe" },
                    { 26, null, 3, "Ioniq" },
                    { 27, null, 3, "Veloster" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_ModelId",
                table: "Car",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Model_ManufacturerId",
                table: "Model",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_CarId",
                table: "Service",
                column: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Model");

            migrationBuilder.DropTable(
                name: "Manufacturer");
        }
    }
}
