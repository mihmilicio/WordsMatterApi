namespace DinamicaApi.Models;

public class TextoEnviado
{
  public long Id { get; set; }
  public string? NomeParticipante { get; set; } 
  public string? Texto { get; set; }
  public string? IdSala { get; set; }
}