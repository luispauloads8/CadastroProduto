using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClienteProjeto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PopulaContaContabeis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO contacontabeis (Descricao,GrupoContaId) " +
                "VALUES('recebimento produto',1)");
            migrationBuilder.Sql("INSERT INTO contacontabeis (Descricao, GrupoContaId) " +
                "VALUES ('pagamento de fornecedor', 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from contacontabeis");
        }
    }
}
