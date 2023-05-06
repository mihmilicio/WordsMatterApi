using System.Net.Http.Headers;
using System.Text.Json;
using DinamicaApi.Models;

public class NuvemService : INuvemService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public NuvemService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<ImagemNuvem> GetNuvemDePalavras(string textosCompilados)
    {
        var uri = "https://textvis-word-cloud-v1.p.rapidapi.com/v1/textToCloud";

        var requestData = new RequestTextToCloud(textosCompilados);

        var request = new HttpRequestMessage() {
            RequestUri = new Uri(uri),
            Method = HttpMethod.Post,
            Content = JsonContent.Create(requestData)
        };

        request.Headers.Add("x-rapidapi-host", "textvis-word-cloud-v1.p.rapidapi.com");
        request.Headers.Add("x-rapidapi-key", _config["RapidAPIKey"]);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await _httpClient.SendAsync(request);

        // To read the response as string
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);

        response.EnsureSuccessStatusCode();

        var imagemNuvem = new ImagemNuvem();
        imagemNuvem.Imagem = responseString;

        return imagemNuvem;
    }
}