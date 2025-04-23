using Microsoft.EntityFrameworkCore;
using ZooProjectApi.Models;

namespace ZooProjectApi.Services;

public class AnimalService : IAnimalService
{
    private readonly AnimalDbContext context;

    public AnimalService(AnimalDbContext animalDbContext)
    {
        context = animalDbContext;
    }

    public async Task<Animal> AddAnimalAsync(AddAnimalRequest animalRequest)
    {
        var animal = (new Animal
        {
            Name = animalRequest.Name,
            Type = animalRequest.Type
        });
        await context.Animals.AddAsync(animal);
        await context.SaveChangesAsync();
        return animal;
    }
    public async Task DeleteAnimalAsync(Guid id)
    {
        var animal = await context.Animals.FirstOrDefaultAsync(x => x.Id == id);

        if (animal is null)
            throw new KeyNotFoundException($"Animal {id} not found");

        context.Animals.Remove(animal);
        await context.SaveChangesAsync();
    }
    public async Task FeedAnimalAsync(Guid id, int amountFood)
    {
        var animal = await context.Animals.FindAsync(id);

        if (animal is null)
            throw new KeyNotFoundException($"Animal {id} not found");

        if (amountFood is < 1 or > 100)
            throw new ArgumentException("Amount food should between 1 and 100");

        animal.Energy = Math.Min(animal.Energy + amountFood, 100);
        await context.SaveChangesAsync();
    }
    public async Task<Animal> GetAnimalAsync(Guid id)
    {
        var animal = await context.Animals.FindAsync(id);
        return animal ?? throw new KeyNotFoundException($"Animal {id} not found");
    }
    public async Task<List<Animal>> GetAnimalsAsync()
    {
        return await context.Animals.ToListAsync();
    }
}
