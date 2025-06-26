using ClienteProjeto.API.Montadores.Service;
using ClienteProjeto.Nucleo.Excecoes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Relatorio.Dto;
using ClienteProjeto.Nucleo.MetodoExtensao;


namespace ClienteProjeto.API.Montadores
{
    public abstract class MontadorAbstrato<TParametros> : IMontador
        where TParametros : ParametrosDeEmissaoBaseDR
    {

        protected PageSize TamanhoDaPagina;
        protected TParametros Parametros { get; private set; }
        protected PdfWriter EscritorDePdf { get; private set; }
        protected Document Documento { get; private set; }
        protected MemoryStream MemoryStreamDoDocumento { get; set; }

        protected MontadorAbstrato(TParametros parametros)
        {
            Parametros = parametros;
            MemoryStreamDoDocumento = new MemoryStream();

            Rectangle tamanhoPagina = ObterTamanhoDaPagina();
            Documento = new Document(tamanhoPagina);

            EscritorDePdf = PdfWriter.GetInstance(Documento, MemoryStreamDoDocumento);

            Documento.Open();
            DefinaMargens();

            MonteDocumento();
        }

        public abstract void MonteDocumento();

        public virtual byte[] Gerar()
        {
            try
            {

                if (Documento != null && Documento.IsOpen())
                    Documento.Close();

                return MemoryStreamDoDocumento.ToArray();
            }
            catch (Exception ex)
            {
                throw new InconsistenciaException(ex.Message.Equals("Document has no pages.") ? "Não foi encontrado nenhum resultado para os filtros informados." : ex.Message);
            }
            finally
            {
                MemoryStreamDoDocumento.Dispose();
            }
        }

        protected virtual void DefinaMargens()
        {
            Documento.SetMargins(Parametros.Layout_MargemSuperior.CentimetrosToPoints(),
                                 Parametros.Layout_MargemDireita.CentimetrosToPoints(),
                                 Parametros.Layout_MargemInferior.CentimetrosToPoints(),
                                 Parametros.Layout_MargemEsquerda.CentimetrosToPoints()
            );
        }

        protected virtual Rectangle ObterTamanhoDaPagina()
        {
            if (Parametros.Layout_TamanhoDoPapelAltura == 0 || Parametros.Layout_TamanhoDoPapelLargura == 0)
            {
                return PageSize.A4;
            }

            return new Rectangle(
                Parametros.Layout_TamanhoDoPapelLargura.MilimetrosToPoints(),
                Parametros.Layout_TamanhoDoPapelAltura.MilimetrosToPoints()
            );
        }

    }
}
