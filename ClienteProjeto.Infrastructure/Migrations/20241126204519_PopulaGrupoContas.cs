using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClienteProjeto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PopulaGrupoContas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO grupocontas (Descricao) VALUES ('Receita')");
            migrationBuilder.Sql("INSERT INTO grupocontas (Descricao) VALUES ('Despesa')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from grupocontas");
        }
    }
}
