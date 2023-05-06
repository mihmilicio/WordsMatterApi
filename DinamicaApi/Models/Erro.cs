namespace DinamicaApi.Models;

public class Erro 
{
  public Erro(string mensagem)
    {
      this.Mensagem = mensagem;
    }


  public string Mensagem { get; set; }
}