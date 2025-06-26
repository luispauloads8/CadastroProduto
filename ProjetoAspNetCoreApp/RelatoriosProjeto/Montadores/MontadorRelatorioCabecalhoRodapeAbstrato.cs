using ClienteProjeto.API.Montadores;
using Relatorio.Dto;

namespace RelatoriosProjeto.Montadores
{
    public abstract class MontadorRelatorioCabecalhoRodapeAbstrato<TParametros> : MontadorAbstrato<TParametros>
        where TParametros : ParametrosDeEmissaoBaseDR
    {
        protected MontadorRelatorioCabecalhoRodapeAbstrato(TParametros parametros) 
            : base(parametros) 
        {
            
        }
    }
}
