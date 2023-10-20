using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallenge.AutomotiveMechanics.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoServiceCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Car_CarId",
                table: "Service");

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Car_CarId",
                table: "Service",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Car_CarId",
                table: "Service");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Service",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Car_CarId",
                table: "Service",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id");
        }
    }
}
