using System.Net;
using Polly;
using Polly.Contrib.WaitAndRetry;
using RestaurantService.Clients;

namespace RestaurantService.Extensions;

public static class HttpClientServiceCollectionExtensions
{
    private const string MealServiceClientBaseAddress = "http://localhost:5059/";

    public static IServiceCollection AddRawMealServiceClient(this IServiceCollection services)
    {
        services.AddHttpClient<IMealServiceClient, MealServiceClient>(client =>
        {
            client.BaseAddress = new(MealServiceClientBaseAddress);
        });

        return services;
    }

    public static IServiceCollection AddMealServiceClientWithRetry(this IServiceCollection services)
    {
        services.AddHttpClient<IMealServiceClient, MealServiceClient>(client =>
        {
            client.BaseAddress = new(MealServiceClientBaseAddress);
        }).AddPolicyHandler(Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(x => x.StatusCode >= HttpStatusCode.InternalServerError)
            .RetryAsync(3));

        return services;
    }

    public static IServiceCollection AddMealServiceClientWithExponentialBackoffRetry(this IServiceCollection services)
    {
        services.AddHttpClient<IMealServiceClient, MealServiceClient>(client =>
        {
            client.BaseAddress = new(MealServiceClientBaseAddress);
        }).AddPolicyHandler(Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(x => x.StatusCode >= HttpStatusCode.InternalServerError)
            .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 3)));

        return services;
    }

    public static IServiceCollection AddMealServiceClientWithExponentialBackoffRetryAndCircuitBreaker(this IServiceCollection services)
    {
        services.AddHttpClient<IMealServiceClient, MealServiceClient>(client =>
        {
            client.BaseAddress = new(MealServiceClientBaseAddress);
        })
            .AddPolicyHandler(
                Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .OrResult(x => x.StatusCode >= HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 2)))
        .AddPolicyHandler(
            Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(x => x.StatusCode >= HttpStatusCode.InternalServerError)
            .CircuitBreakerAsync(3, TimeSpan.FromSeconds(10)));

        return services;
    }
}