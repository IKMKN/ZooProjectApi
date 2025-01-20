using ZooProjectApi.Models;
using ZooProjectApi.Services;

namespace ZooProjectApi;

public static class AnimalEndpoints
{
    public static void MapAnimalEndpionts(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/animals");

        group.MapGet("", GetAnimals);
        group.MapGet("/{id}", GetAnimal);
        group.MapPost("", AddAnimal);
        group.MapPut("/{id}/feed", FeedAnimal);
        group.MapDelete("/{id}", DeleteAnimal);
    }
    private static IResult GetAnimals(IAnimalService animalService)
    {
        var animals = animalService.GetAnimals();
        return Results.Ok(animals);
    }
    private static IResult GetAnimal(int id, IAnimalService animalService)
    {
        var animal = animalService.GetAnimal(id);
        return animal is null ? Results.NotFound() : Results.Ok(animal);
    }
    private static IResult AddAnimal(AddAnimalRequest animal, IAnimalService animalService)
    {
        var addedAnimal = animalService.AddAnimal(new Animal(animal.Name, animal.Type));
        return Results.Created($"/animals/{addedAnimal.Id}", addedAnimal);
    }
    private static IResult FeedAnimal(int id, FeedRequest feedRequest, IAnimalService animalService)
    {
        if (feedRequest.FoodAmount < 1 || feedRequest.FoodAmount > 100)
            return Results.BadRequest();

        var result = animalService.FeedAnimal(id, feedRequest.FoodAmount);
        return result ? Results.Ok(result) : Results.NotFound();
    }
    private static IResult DeleteAnimal(int id, IAnimalService animalService)
    {
        var result = animalService.DeleteAnimal(id);
        return result ? Results.NoContent() : Results.NotFound();
    }
}
