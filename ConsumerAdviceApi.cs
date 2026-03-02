using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization; // Para o atributo JsonPropertyName
using System.Threading.Tasks;

using HttpClient client = new HttpClient();

try 
{
    string url = "https://api.adviceslip.com/advice";
    
    
    string jsonResponse = await client.GetStringAsync(url);

    var resultado = JsonSerializer.Deserialize(
        jsonResponse, 
        AdviceContext.Default.AdviceResponse
    );

    if (resultado?.Slip != null)
    {
        Console.WriteLine($"Conselho de Hoje:\n{resultado.Slip.Advice}");
    }
}
catch (Exception ex) 
{
    Console.WriteLine($"Erro ao buscar conselho: {ex.Message}");
}

public class AdviceResponse
{
    [JsonPropertyName("slip")]
    public required Slip Slip { get; set; }
}

public class Slip
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("advice")]
    public required string Advice { get; set; }
}
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(AdviceResponse))]
internal partial class AdviceContext : JsonSerializerContext
{
}