using Microsoft.AspNetCore.Mvc;
using DinamicaApi.Models;

namespace LojaApi.Controllers;

[ApiController]
[Route("api/dinamica")]
public class DinamicaController : ControllerBase
{
    private readonly ILogger<DinamicaController> _logger;
    private readonly DinamicaContext _context;
    private readonly INuvemService _nuvemSvc;

    public DinamicaController(
        ILogger<DinamicaController> logger, 
        DinamicaContext context,
        INuvemService nuvemSvc
    )
    {
        _logger = logger;
        _context = context;
        _nuvemSvc = nuvemSvc;
    }

    [HttpPost("salas/{idSala}")]
    public async Task<IActionResult> Create(long idSala, TextoEnviado textoEnviado)
    {
      textoEnviado.IdSala = idSala;

      // TODO verificar que sala existe, retornar erro caso não

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
      // TODO verificar que sala existe, retornar erro caso não

      return _context.TextosEnviados
          .Where(x => x.IdSala == idSala)
          .ToList();

    }

    [HttpGet("salas/{idSala}/nuvem")]
    public async Task<ActionResult<ImagemNuvem>> GetNuvemDePalavras(long idSala)
    {
      var textosEnviados = GetTextosEnviadosNaSala(idSala).Value; // TODO tratar quando sala não existe

      if (textosEnviados == null || textosEnviados.Count() == 0) {
        return NoContent();
      }

      var conteudos = textosEnviados.Select(x => x.Texto);

      var textosCompilados = String.Join("|", conteudos);

      try 
      {
        var nuvem = await _nuvemSvc.GetNuvemDePalavras(textosCompilados);
        return Ok(nuvem);
      }
      catch (Exception)
      {
        return BadRequest(new Erro("Não foi possível gerar a nuvem de palavras no momento"));
      }

    }
}