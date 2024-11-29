using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClienteProjeto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PopulaFornecedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO fornecedores (Descricao,CNPJ,Telefone,Endereco,CEP,Observacao,CidadeId,Email)" +
                "VALUES('loreal brasil','76073920000100','(62)987541235','rua rocha lima','75388740','produto com qualidade',3,'empresa@gmail.com')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from fornecedores");
        }
    }
}
