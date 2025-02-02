using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClienteProjeto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoEnumEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Cidades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Cidades",
                keyColumn: "Id",
                keyValue: 1,
                column: "Estado",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Cidades",
                keyColumn: "Id",
                keyValue: 2,
                column: "Estado",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Cidades",
                keyColumn: "Id",
                keyValue: 3,
                column: "Estado",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Cidades");
        }
    }
}
