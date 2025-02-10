using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClienteProjeto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeIdCidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Cidades_CidadeEmpresaId",
                table: "Empresas");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_CidadeEmpresaId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "CidadeEmpresaId",
                table: "Empresas");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_CidadeId",
                table: "Empresas",
                column: "CidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Cidades_CidadeId",
                table: "Empresas",
                column: "CidadeId",
                principalTable: "Cidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Cidades_CidadeId",
                table: "Empresas");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_CidadeId",
                table: "Empresas");

            migrationBuilder.AddColumn<int>(
                name: "CidadeEmpresaId",
                table: "Empresas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_CidadeEmpresaId",
                table: "Empresas",
                column: "CidadeEmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Cidades_CidadeEmpresaId",
                table: "Empresas",
                column: "CidadeEmpresaId",
                principalTable: "Cidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
