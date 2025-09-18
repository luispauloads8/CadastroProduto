using ClienteProjeto.Application.Services.Relatorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relatorio.Dto.ProdutoServico;
using Relatorio.Dto.ViewModel;

namespace ClienteProjeto.API.Controllers.Relatorio.Categoria
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmissaoCategoriaController : ControllerBase
    {
        private readonly ChamadaRelatorioCategoriaService _chamdaServiceCategoria;

        public EmissaoCategoriaController(ChamadaRelatorioCategoriaService chamadaServiceCategoria)
        {
            _chamdaServiceCategoria = chamadaServiceCategoria;
        }

        [HttpPost("emissao_categoria")]
        public async Task<IActionResult> GerarRelatorio([FromBody] ParametroEmissaoCategoriaVM categoria)
        {
            try
            {
                var pdfBytes = await _chamdaServiceCategoria.Gerar(categoria);
                return File(pdfBytes, "application/pdf", "relatorio.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message, stack = ex.StackTrace });
            }

        }
    }
}
