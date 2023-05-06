using DinamicaApi.Models;

public interface INuvemService
{
  Task<ImagemNuvem> GetNuvemDePalavras(string textosCompilados);
}