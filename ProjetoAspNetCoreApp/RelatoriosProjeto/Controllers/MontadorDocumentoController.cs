using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relatorio.Dto;
using RelatoriosProjeto.Montadores;
using System.Reflection;

namespace RelatoriosProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MontadorDocumentoController : ControllerBase
    {

        [HttpPost, Route("Gerar")]
        public IActionResult Gerar([FromBody] RelatorioRequest request)
        {
            var assembly = typeof(Program).Assembly;
            var types = assembly.GetTypes();

            var typesRelatorio = Assembly.Load("Relatorio.Dto").GetTypes();
            var parametrosType = typesRelatorio.FirstOrDefault(t => t.FullName == request.ParametrosClassName);
            if (parametrosType == null) throw new Exception("O objeto dos parametros não foi encontrado");

            var parametros = (ParametrosDeEmissaoBaseDR)ClienteProjeto.Nucleo.Deserializer.DeserializeFromBase64(request.SerializedData, parametrosType);

            var montadorType = types.FirstOrDefault(t => t.Name == parametros.Montador);

            if (montadorType == null) throw new Exception("Montador não encontrado");

            var montador = FabricaDeMontadores.Crie(parametros.Montador, parametros);

            var monte = montador.Gerar();

            Console.WriteLine("Tamanho do PDF: " + monte.Length);

            return File(monte, "application/pdf");

            //return monte;
        }

    }

    public class RelatorioRequest
    {
        public string SerializedData { get; set; }
        public string ParametrosClassName { get; set; }
    }

}
