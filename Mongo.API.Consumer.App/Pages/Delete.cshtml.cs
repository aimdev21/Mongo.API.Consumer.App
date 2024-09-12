using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mongo.API.Consumer.App.Models;

namespace Mongo.API.Consumer.App.Pages;

public class DeleteModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DeleteModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    [BindProperty]
    public JokeModel JokeModels { get; set; }

    public async Task OnGet(string id)
    {
        var httpClient = _httpClientFactory.CreateClient("Mongo.API");

        using var response = await httpClient.GetAsync(id);

        if(response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            JokeModels = await JsonSerializer.DeserializeAsync<JokeModel>(contentStream);
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var httpClient = _httpClientFactory.CreateClient("Mongo.API");

        using var response = await httpClient.DeleteAsync(JokeModels.id);

        if(response.IsSuccessStatusCode)
        {
            TempData["success"] = "Joke deleted successfully!";
            return RedirectToPage("Index");
        }
        else 
        {
            TempData["failure"] = "Joke has NOT been Deleted!";
            return RedirectToPage("Index");
        }
    }
}