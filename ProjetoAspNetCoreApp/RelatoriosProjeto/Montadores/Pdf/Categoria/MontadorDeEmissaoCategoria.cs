using iTextSharp.text.pdf;
using iTextSharp.text;
using Relatorio.Dto.ViewModel;

namespace RelatoriosProjeto.Montadores.Pdf.Categoria
{
    public class MontadorDeEmissaoCategoria : MontadorRelatorioCabecalhoRodapeAbstrato<ParametroEmissaoCategoriaVM>
    {
        public MontadorDeEmissaoCategoria(ParametroEmissaoCategoriaVM parametros) : base(parametros)
        {
        }

        public override void MonteDocumento()
        {
            MonteTabela();
        }

        private void MonteTabela()
        {
            // Título
            var titulo = new Paragraph("Tabela de Categorias", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
            titulo.Alignment = Element.ALIGN_CENTER;
            titulo.SpacingAfter = 20;
            Documento.Add(titulo);

            // Criar tabela com 2 colunas
            PdfPTable tabela = new PdfPTable(2);
            tabela.WidthPercentage = 100;
            tabela.SetWidths(new float[] { 1, 4 }); // Proporção das colunas

            // Cabeçalhos
            PdfPCell cabecalho1 = new PdfPCell(new Phrase("ID", FontFactory.GetFont("Arial", 12, Font.BOLD)));
            cabecalho1.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabela.AddCell(cabecalho1);

            PdfPCell cabecalho2 = new PdfPCell(new Phrase("Descrição", FontFactory.GetFont("Arial", 12, Font.BOLD)));
            cabecalho2.BackgroundColor = BaseColor.LIGHT_GRAY;
            tabela.AddCell(cabecalho2);

            // Adicionar dados
            foreach (var categoria in Parametros.Categorias)
            {
                tabela.AddCell(categoria.Id.ToString());
                tabela.AddCell(categoria.Descricao);
            }

            Documento.Add(tabela);
        }
    }
}
