using iTextSharp.text;
using iTextSharp.text.pdf;
using Relatorio.Dto.ViewModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace RelatoriosProjeto.Montadores.Pdf.Lancamento
{
    public class MontadorDeEmissaoLancamento : MontadorRelatorioCabecalhoRodapeAbstrato<ParametroEmissaoLancamentosVM>
    {
        public MontadorDeEmissaoLancamento(ParametroEmissaoLancamentosVM parametros) 
            : base(parametros)
        {
            
        }

        public override void MonteDocumento()
        {
            MonteTabela();
        }

        public void MonteTabela()
        {

            PdfPTable tabela = new PdfPTable(5)
            {
                WidthPercentage = 100
            };


            tabela.AddCell(new Phrase("Conta Contabil", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9)));
            tabela.AddCell(new Phrase("Data Lançamento", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9)));
            tabela.AddCell(new Phrase("Produto/Serviço", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9)));
            tabela.AddCell(new Phrase("Observação", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9)));
            tabela.AddCell(new Phrase("Valor", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9)));

            foreach (var lancamento in Parametros.Lancamentos)
            {
                tabela.AddCell(lancamento.ContaContabil?.Descricao ?? "N/A");
                tabela.AddCell(lancamento.DataLancamento.ToString("dd/MM/yyyy"));
                tabela.AddCell(lancamento.ProdutoServico?.Descricao.ToString() ?? "N/A");
                tabela.AddCell(lancamento.Observacao ?? "N/A");
                tabela.AddCell(lancamento.Valor.ToString("C2"));

                // 🟦 Subtabela: ItensLancamentos
                //if (lancamento.ItensLancamentos != null && lancamento.ItensLancamentos.Any())
                //{
                //    PdfPTable subTabela = new PdfPTable(2)
                //    {
                //        WidthPercentage = 90
                //    };

                //    // Cabeçalhos da subtabela
                //    subTabela.AddCell(new Phrase("Quantidade", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9)));
                //    subTabela.AddCell(new Phrase("Valor Item", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9)));

                //    // Dados dos itens
                //    foreach (var item in lancamento.ItensLancamentos)
                //    {
                //        subTabela.AddCell(item.Quantidade.ToString());
                //        subTabela.AddCell(item.ValorItem.ToString("C2"));
                //    }

                //    // Adiciona a subtabela como uma célula mesclada
                //    PdfPCell subCell = new PdfPCell(subTabela)
                //    {
                //        Colspan = 6,
                //        PaddingTop = 5,
                //        PaddingBottom = 5
                //    };

                //    tabela.AddCell(subCell);
                //}

                Documento.Add(tabela);
            }
        }
    }
}
