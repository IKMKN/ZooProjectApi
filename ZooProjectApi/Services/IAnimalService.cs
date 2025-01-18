using ZooProjectApi.Models;

namespace ZooProjectApi.Services;

public interface IAnimalService
{
    List<Animal> GetAnimals();
    Animal? GetAnimal(int id);
    Animal AddAnimal(Animal animal);
    bool FeedAnimal(int id, int amountFood);
    void DeleteAnimal(int id);
}
