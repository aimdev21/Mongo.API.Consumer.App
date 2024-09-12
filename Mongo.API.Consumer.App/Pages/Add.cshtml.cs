using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using Mongo.API.Consumer.App.Models;


namespace Mongo.API.Consumer.App.Pages;

public class AddModel : PageModel
{
    // IHttpClientFactory set using dependency injection 
    private readonly IHttpClientFactory _httpClientFactory;

    public AddModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    // Add the data model and bind the form data to the page model properties
    [BindProperty]
    public JokeModel JokeModels { get; set; }

    // Begin POST operation code
    
    public async Task<IActionResult> OnPost()
    {
        // Serialize the information to be added to the database
        var JsonContent = new StringContent(JsonSerializer.Serialize(JokeModels), Encoding.UTF8, "application/json");

        // Create the HTTP client using the FruitAPI named factory
        var httpClient = _httpClientFactory.CreateClient("Mongo.API");

        // Execute the POST request and store the response. The parameters in PostAsync 
        // direct the POST to use the base address and passes the serialized data to the API
        using HttpResponseMessage response = await httpClient.PostAsync("", JsonContent);

        // Return to the home (Index) page and add a temporary success/failure message to the page.
        if(response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Data was added successfully!";
            return RedirectToPage("Index");
        }
        else
        {
            TempData["failure"] = "Operation was not successful!";
            return RedirectToAction("Index");
        }
    }

    // End POST operation code
}

