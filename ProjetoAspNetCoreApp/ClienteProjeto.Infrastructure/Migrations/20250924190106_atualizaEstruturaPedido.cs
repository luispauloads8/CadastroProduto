using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClienteProjeto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class atualizaEstruturaPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_ProdutoServicos_ProdutoServicoId",
                table: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_ProdutoServicoId",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "ItensPedidoId",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "ProdutoServicoId",
                table: "Pedido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItensPedidoId",
                table: "Pedido",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoServicoId",
                table: "Pedido",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_ProdutoServicoId",
                table: "Pedido",
                column: "ProdutoServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_ProdutoServicos_ProdutoServicoId",
                table: "Pedido",
                column: "ProdutoServicoId",
                principalTable: "ProdutoServicos",
                principalColumn: "Id");
        }
    }
}
