using System.Text.Json;
using ZooProjectApi.Models;

namespace ZooProjectApi.Sevices;

public class AnimalService : IAnimalService
{
    private List<Animal> _animals = new List<Animal>();
    private int _animalId = 1;
    private string _saveAnimals = "saveAnimals.json";
    public AnimalService()
    {
        LoadFromFile();
    }

    public Animal AddAnimal(Animal animal)
    {
        animal.Id = _animalId++;
        _animals.Add(animal);
        SaveFromFile();
        return animal;
    }

    public void DeleteAnimal(int id)
    {
        Animal? animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal != null) _animals.Remove(animal);
        SaveFromFile();
    }

    public Animal? FeedAnimal(int id, int amountFood)
    {
        Animal? animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal != null)
        {
            animal.Energy += amountFood;
            if (animal.Energy > 100) animal.Energy = 100;
            SaveFromFile();
            return animal;
        }
        return null;
    }

    public Animal? GetAnimal(int id)
    {
        return _animals.FirstOrDefault(a => a.Id == id);
    }

    public List<Animal> GetAnimals()
    {
        return _animals;
    }

    private void LoadFromFile()
    {
        if (File.Exists(_saveAnimals))
        {
            var json = File.ReadAllText(_saveAnimals);
            _animals = JsonSerializer.Deserialize<List<Animal>>(json);
            if (_animals.Count > 0) _animalId = _animals.Max(a => a.Id) + 1;
        }
    }
    public void SaveFromFile()
    {
        var json = JsonSerializer.Serialize(_animals);
        File.WriteAllText(_saveAnimals, json);
    }
}
