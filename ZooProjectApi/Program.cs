using ZooProjectApi.Models;
using ZooProjectApi.Sevices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/animals", (IAnimalService animalService) =>
{
    return Results.Ok(animalService.GetAnimals());
});

app.MapGet("/api/animals/{id}", (int id, IAnimalService animalService) =>
{
    var animal = animalService.GetAnimal(id);
    if (animal == null) return Results.NotFound();

    return Results.Ok(animal);

});

app.MapPost("/api/animals", (Animal animal, IAnimalService animalService) =>
{
    if (string.IsNullOrEmpty(animal.Name)) return Results.BadRequest();
    if (string.IsNullOrEmpty(animal.Type)) return Results.BadRequest();
    var addedAnimal = animalService.AddAnimal(animal);
    return Results.Created($"/animals/{addedAnimal.Id}", addedAnimal);
});

app.MapPut("/api/animals/{id}/feed", (int id, FeedRequest feedRequest, IAnimalService animalService) =>
{
    if (feedRequest.AmountFood > 100 || feedRequest.AmountFood < 0) return Results.BadRequest();
    var animal = animalService.FeedAnimal(id, feedRequest.AmountFood);
    if (animal == null) return Results.NotFound();
    return Results.Ok();
});

app.MapDelete("/api/animals/{id}", (int id, IAnimalService animalService) =>
{
    if (animalService.GetAnimal(id) == null) return Results.NotFound();
    animalService.DeleteAnimal(id);
    return Results.NoContent();
});

app.Run();
