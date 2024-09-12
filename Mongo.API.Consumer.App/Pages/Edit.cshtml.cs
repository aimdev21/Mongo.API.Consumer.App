using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mongo.API.Consumer.App.Models;

namespace Mongo.API.Consumer.App.Pages;

public class EditModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EditModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
        public JokeModel JokeModels { get; set; }

    public async Task OnGet(string id)
    {
        var httpClient = _httpClientFactory.CreateClient("Mongo.API");

        using HttpResponseMessage response = await httpClient.GetAsync(id);

        if(response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            JokeModels = await JsonSerializer.DeserializeAsync<JokeModel>(contentStream);
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(JokeModels), Encoding.UTF8, "application/json");

        var httpClient = _httpClientFactory.CreateClient("Mongo.API");

        using var response = await httpClient.PutAsync("", jsonContent);

        if(response.IsSuccessStatusCode)
        {
            TempData["success"] = "Joke edit has been successful!";
            return RedirectToPage("Index");
        }
        else 
        {
            TempData["failure"] = "The Joke has NOT been updated!";
            return RedirectToPage("Index");
        }
    }
}