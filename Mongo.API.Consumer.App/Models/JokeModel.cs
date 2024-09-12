using System.ComponentModel.DataAnnotations;

namespace Mongo.API.Consumer.App.Models;

public class JokeModel
{
    [Key]
    public string? id { get; set; }

    [Display(Name = "Joke Question")]
    public string jokeQuestion { get; set; } = null!;
    [Display(Name = "Joke Answer")]
    public string jokeAnswer { get; set; } = null!;
}
