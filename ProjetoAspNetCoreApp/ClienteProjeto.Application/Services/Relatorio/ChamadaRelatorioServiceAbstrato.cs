using ClienteProjeto.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Relatorio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace ClienteProjeto.Application.Services.Relatorio
{
    public abstract class ChamadaRelatorioServiceAbstrato<TEntidade> : IServicoDeEmissao<TEntidade>
        where TEntidade : ParametrosDeEmissaoBaseDR

    {
        private readonly HttpClient _httpClient;

        protected virtual Task AjusteParametros(TEntidade entidade)
        {
            //entidade.OperadorEmissao = Contexto.Instancia?.Operador?.Nome ?? "";
           // entidade.Dominio = Contexto.Instancia?.Dominio ?? "";
            entidade.Montador = ObtenhaMontador(entidade);

            return Task.FromResult<object>(null);
        }

        protected abstract string ObtenhaMontador(TEntidade entidade);

        public virtual async Task<byte[]> Gerar(TEntidade entidade)
        {
            await AjusteParametros(entidade);

            return await GerarRelatorioAsync(entidade);
        }

        public ChamadaRelatorioServiceAbstrato(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<byte[]> GerarRelatorioAsync(TEntidade entidade)
        {
            var url = "https://localhost:7122/api/MontadorDocumento/Gerar";

            try
            {
                string objetoSerializado = Nucleo.Serializer.SerializeToBase64(entidade);

                var model = new
                {
                    SerializedData = objetoSerializado,
                    ParametrosClassName = entidade.GetType().FullName // ou entidade.GetClassName()
                };

                string payload = JsonConvert.SerializeObject(model);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = content
                };

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    throw new Exception(await response.Content.ReadAsStringAsync());

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao emitir relatório: " + ex.Message, ex);
            }
        }
    }
    
}
