using ZooProjectApi.Contracts;
using ZooProjectApi.Models;

namespace ZooProjectApi.Services;

public interface IAnimalService
{
    Task<List<Animal>> GetAnimalsAsync();
    Task <Animal> GetAnimalAsync(Guid id);
    Task<Animal> AddAnimalAsync(AnimalRequest animalRequest);
    Task FeedAnimalAsync(Guid id, int amountFood);
    Task DeleteAnimalAsync(Guid id);
}
