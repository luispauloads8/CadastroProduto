using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Services.Relatorio;
using Microsoft.AspNetCore.Mvc;
using Relatorio.Dto.ViewModel;

namespace ClienteProjeto.API.Controllers.Relatorio.Lancamento
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {

        private readonly ChamadaRelatorioLancamentoService _chamadaRelatorioService;

        public RelatoriosController(ChamadaRelatorioLancamentoService chamadaRelatorioService)
        {
            _chamadaRelatorioService = chamadaRelatorioService;
        }

        [HttpPost("emissao_lancamento")]
        public async Task<IActionResult> GerarRelatorio([FromBody] ParametroEmissaoLancamentosVM lancamento)
        {
            try
            {
                var pdfBytes = await _chamadaRelatorioService.Gerar(lancamento);
                return File(pdfBytes, "application/pdf", "relatorio.pdf");
            }
            catch (Exception ex)
            {
                // Log ex, ou retorne como resultado para debug
                return StatusCode(500, new { erro = ex.Message, stack = ex.StackTrace });
            }
        }
    }
}
