using iTextSharp.text;
using iTextSharp.text.pdf;
using Relatorio.Dto.ViewModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using static iTextSharp.text.pdf.AcroFields;

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
                foreach (var item in lancamento.ItensLancamentos)
                {
                    tabela.AddCell(lancamento.ContaContabil?.Descricao ?? "N/A");
                    tabela.AddCell(lancamento.DataLancamento.ToString("dd/MM/yyyy"));
                    tabela.AddCell(lancamento.ProdutoServico?.Descricao.ToString() ?? "N/A");
                    tabela.AddCell(lancamento.Observacao ?? "N/A");
                    tabela.AddCell((item.ValorItem * item.Quantidade).ToString("C2"));
                }
                
            }

            Documento.Add(tabela);
        }
    }
}
