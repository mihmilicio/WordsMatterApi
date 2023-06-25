namespace DinamicaApi.Models;

public class EnvioParticipante
{
  public EnvioParticipante(string? NomeParticipante, string? IdSala) {
      this.NomeParticipante = NomeParticipante;
      this.IdSala = IdSala;
  }

  public string? NomeParticipante { get; set; } 
  public string? IdSala { get; set; }
}