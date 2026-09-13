using System.Text.Json;
using ConsumerViaCep.Models;
using static System.Console;

WriteLine("Digite o seu CEP: ");
var cep = ReadLine();

var enderecoUrl = $@"https://viacep.com.br/ws/{cep}/json/";

WriteLine($"Realizando requisição para o endpoint: {enderecoUrl}");

var client = new HttpClient();

try
{
    HttpResponseMessage? response = await client.GetAsync(enderecoUrl);
    response.EnsureSuccessStatusCode();

    WriteLine("API funcionou: " + response.IsSuccessStatusCode);
    WriteLine("Status Code: " + response.StatusCode);

    string responseString = await response.Content.ReadAsStringAsync();

    Endereco? enderecoRetornadoDaApi = JsonSerializer.Deserialize<Endereco>(responseString);

    if (enderecoRetornadoDaApi is not null)
    {
        WriteLine($"CEP: {enderecoRetornadoDaApi.Cep}");
        WriteLine($"Rua: {enderecoRetornadoDaApi.Logradouro}");
        WriteLine($"Bairro: {enderecoRetornadoDaApi.Bairro}");
        WriteLine($"Cidade: {enderecoRetornadoDaApi.Localidade}");
        WriteLine($"UF: {enderecoRetornadoDaApi.Uf}");
        WriteLine($"DDD: {enderecoRetornadoDaApi.Ddd}");
    }
}
catch (System.Exception e)
{
    WriteLine("Erro: " + e.InnerException);
    WriteLine("Aconteceu um erro ao consultar a api: " + e.Message);
}
