using ZooProjectApi.Models;
using ZooProjectApi.Services;

namespace ZooProjectApi;

public static class AnimalEndpoints
{
    public static void MapAnimalEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/animals");

        group.MapGet("", GetAnimalsAsync);
        group.MapGet("/{id}", GetAnimalAsync);
        group.MapPost("", AddAnimalAsync);
        group.MapPut("/{id}/feed", FeedAnimalAsync);
        group.MapDelete("/{id}", DeleteAnimalAsync);
    }
    private static async Task<IResult> GetAnimalsAsync(IAnimalService animalService)
    {
        var animals = await animalService.GetAnimalsAsync();
        return Results.Ok(animals);
    }
    private static async Task<IResult> GetAnimalAsync(Guid id, IAnimalService animalService)
    {
        var animal = await animalService.GetAnimalAsync(id);
        return Results.Ok(animal);
    }
    private static async Task<IResult> AddAnimalAsync(AddAnimalRequest animalRequest, IAnimalService animalService)
    {
        var animal = await animalService.AddAnimalAsync(animalRequest);
        return Results.Created($"/animals/{animal.Id}", animal);
    }
    private static async Task<IResult> FeedAnimalAsync(Guid id, FeedRequest feedRequest, IAnimalService animalService)
    {
        await animalService.FeedAnimalAsync(id, feedRequest.FoodAmount);
        return Results.Ok();
    }
    private static async Task<IResult> DeleteAnimalAsync(Guid id, IAnimalService animalService)
    {
        await animalService.DeleteAnimalAsync(id);
        return Results.NoContent();
    }
}
