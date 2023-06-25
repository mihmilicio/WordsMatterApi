using Microsoft.AspNetCore.Mvc;
using DinamicaApi.Models;
using System.Net;
using System.Text.Json;

namespace LojaApi.Controllers;

[ApiController]
[Route("api/dinamica")]
public class DinamicaController : ControllerBase
{
    private readonly ILogger<DinamicaController> _logger;
    private readonly DinamicaContext _context;
    private readonly INuvemService _nuvemSvc;
    private readonly RabbitMQProducer _producer;

    public DinamicaController(
        ILogger<DinamicaController> logger, 
        DinamicaContext context,
        INuvemService nuvemSvc,
        RabbitMQProducer producer
    )
    {
        _logger = logger;
        _context = context;
        _nuvemSvc = nuvemSvc;
        _producer = producer;
    }

    [HttpPost("salas/{idSala}")]
    public async Task<IActionResult> Create(string idSala, TextoEnviado textoEnviado)
    {
      
      var sala = await GetSalaAsync(idSala);
      if (sala == null)
      {
        return NotFound(new Erro("É necessário inserir uma sala valida para enviar"));
      }

      Sala? salaAux = JsonSerializer.Deserialize<Sala>(sala);

      if (salaAux == null)
      {
         return BadRequest(new Erro("Sala indisponivél."));
      }

      if (salaAux.maxEnvios == 0)
      {
         return BadRequest(new Erro("A  sala selecionada atingiu o numero máximo de envios"));
      }
      salaAux.maxEnvios = salaAux.maxEnvios.GetValueOrDefault() - 1;
      textoEnviado.IdSala = idSala;
      if (textoEnviado.Texto == null) {
        return BadRequest(new Erro("É necessário inserir um texto para enviar"));
      }

      try {
        _context.TextosEnviados.Add(textoEnviado);
        await _context.SaveChangesAsync();

        HttpClient req = new HttpClient();
        HttpResponseMessage response = req.PutAsJsonAsync("http://localhost:3000/sala/" + idSala, salaAux).Result;
        
        _producer.SendEnvioMessage<EnvioParticipante>(new EnvioParticipante(textoEnviado.NomeParticipante, textoEnviado.IdSala));

        return Created("", textoEnviado);
      }
      catch (Exception)
      {
        return BadRequest(new Erro("Não foi possível registrar esse envio"));
      }
    }

    [HttpGet("salas/{idSala}")]
    public ActionResult<IEnumerable<TextoEnviado>> GetTextosEnviadosNaSala(string idSala)
    {
      return _context.TextosEnviados
          .Where(x => x.IdSala == idSala)
          .ToList();

    }

    [HttpGet("salas/{idSala}/nuvem")]
    public async Task<ActionResult<ImagemNuvem>> GetNuvemDePalavras(string idSala)
    {
      var sala = await GetSalaAsync(idSala);
      if (sala == null)
      {
         return NotFound(new Erro("É necessário inserir uma sala valida para enviar"));
      }
      var textosEnviados = GetTextosEnviadosNaSala(idSala).Value;

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
    [HttpGet("salas/sala/{idSala}")]
    public async Task<string?> GetSalaAsync(string idsala)
    {
        HttpClient req = new HttpClient();
        var content = await req.GetAsync("http://localhost:3000/sala/" + idsala);
        if (content.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        return await content.Content.ReadAsStringAsync();
    }
}