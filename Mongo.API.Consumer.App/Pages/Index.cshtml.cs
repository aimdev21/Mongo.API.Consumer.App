using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mongo.API.Consumer.App.Models;
using System.Text.Json;

namespace Mongo.API.Consumer.App.Pages;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public List<JokeModel> JokeModels { get; set; }


    public async Task OnGet()
    {
        var httpClient = _httpClientFactory.CreateClient("Mongo.API");

        using HttpResponseMessage response = await httpClient.GetAsync("");

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            JokeModels = await JsonSerializer.DeserializeAsync<List<JokeModel>>(contentStream);
        }
    }
}
