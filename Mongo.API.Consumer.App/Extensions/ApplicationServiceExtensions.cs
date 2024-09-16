namespace Mongo.API.Consumer.App.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddHttpClient("Mongo.API", httpClient =>
        {
            httpClient.BaseAddress = new Uri("http://localhost:5000/api/Joke/");
        });

        return services;
    }
}
