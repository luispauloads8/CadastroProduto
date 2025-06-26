using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto
{
    [Serializable]
    public class ParametrosDeEmissaoBaseDR
    {

        public string Dominio { get; set; }
        public string Montador { get; set; }

        public bool EmitirWord { get; set; }
        public bool Layout_FormatoPaisagem { get; set; }
        public string Layout_TituloDoRelatorio { get; set; }
        public float Layout_TamanhoDaFonte { get; set; }
        public decimal Layout_MargemSuperior { get; set; }
        public decimal Layout_MargemInferior { get; set; }
        public decimal Layout_MargemEsquerda { get; set; }
        public decimal Layout_MargemDireita { get; set; }
        public bool Layout_EmitirAssinaturas { get; set; }
        public bool Layout_EmitirCampoDeAssinaturaVazio { get; set; }
        public bool Layout_EmitirAssinaturasEmTodasAsPaginas { get; set; }
        public bool Layout_EmitirAssinaturasSempreNoFinalDaPagina { get; set; }
        public int Layout_QuantidadeDeAssinaturasPorLinha { get; set; }
        public decimal Layout_TamanhoDoPapelAltura { get; set; }
        public decimal Layout_TamanhoDoPapelLargura { get; set; }
        public byte[] Layout_ImagemCabecalho { get; set; }
        public bool Layout_NaoEmitirCabecalho { get; set; }
        public string HashAssinaturaEletronicaDoDocumento { get; set; }

        public bool ExibirQrCodeEduqSign { get; set; }
        public bool ExibirTextoEduqSign { get; set; }
        public DateTime? ValidadeDocumento { get; set; }

        public string OperadorEmissao { get; set; }

        public bool Layout_EmitirFiltros { get; set; }
        public Dictionary<string, string> CamposFiltro { get; set; } = new Dictionary<string, string>();

        public void DefinirConfiguracaoDefault()
        {
            SetMargin(1M);
            Layout_TamanhoDaFonte = 8;
        }

        public void SetMargin(decimal all)
        {
            SetMargin(all, all);
        }

        public void SetMargin(decimal topBottom, decimal leftRigh)
        {
            Layout_MargemSuperior = topBottom;
            Layout_MargemInferior = topBottom;
            Layout_MargemDireita = leftRigh;
            Layout_MargemEsquerda = leftRigh;
        }

        public string GetClassName()
        {
            var className = GetType().Name;
            return className;
        }
    }
}
