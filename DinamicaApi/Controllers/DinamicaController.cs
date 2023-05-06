using Microsoft.AspNetCore.Mvc;
using DinamicaApi.Models;

namespace LojaApi.Controllers;

[ApiController]
[Route("api/dinamica")]
public class DinamicaController : ControllerBase
{
    private readonly ILogger<DinamicaController> _logger;
    private readonly DinamicaContext _context;

    public DinamicaController(ILogger<DinamicaController> logger, DinamicaContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("salas/{idSala}")]
    public async Task<IActionResult> Create(long idSala, TextoEnviado textoEnviado)
    {
      textoEnviado.IdSala = idSala;

      // TODO verificar que sala existe

      if (textoEnviado.Texto == null) {
        return BadRequest(new Erro("É necessário inserir um texto para enviar"));
      }

      try {
        _context.TextosEnviados.Add(textoEnviado);
        await _context.SaveChangesAsync();

        return Created("", textoEnviado);
      }
      catch (Exception)
      {
        return BadRequest(new Erro("Não foi possível registrar esse envio"));
      }
    }

    [HttpGet("salas/{idSala}")]
    public ActionResult<IEnumerable<TextoEnviado>> GetTextosEnviadosNaSala(long idSala)
    {
      return _context.TextosEnviados
          .Where(x => x.IdSala == idSala)
          .ToList();

    }
}