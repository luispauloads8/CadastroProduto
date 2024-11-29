using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClienteProjeto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PopulaEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO empresas (RazaoSocial,NomeFantasia,CNPJ,Email,Telefone,CidadeEmpresaId)" +
                "VALUES('cosmeticos','cosmeticos','62415029000191','123@hotmail.com','(62) 985613331',1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from empresas");
        }
    }
}
