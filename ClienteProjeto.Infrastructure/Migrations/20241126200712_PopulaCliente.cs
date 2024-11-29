using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClienteProjeto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO clientes(Nome,CPF,DataNascimento,RG,Sexo,CEP,Endereco,Telefone,Email,EstadoCivil,Nacionalidade,Observacao,CidadeEnderecoId)" +
                "VALUES('Isabella  Castro Neres','01234567890','2024-09-04',50630003,0,'75388740','rua pereira lima','(62) 985613331','Isabella@gmail.com',1,'brasileira'" +
                ",'empresaria e estudante',1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from clientes");
        }
    }
}
