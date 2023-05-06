namespace DinamicaApi.Models;

public class RequestTextToCloud
{

  public RequestTextToCloud(string texto)
    {
      this.Text = texto;
      Scale = 0.5f;
      Width = 500;
      Height = 500;
      UseStopwords = false;
      Uppercase = false;
    }

  public string Text { get; set; }
  public float Scale { get; } 
  public int Width { get; }
  public int Height { get; }
  public bool UseStopwords { get; }
  public bool Uppercase { get; }
}