using System.Text.Json;
using ZooProjectApi.Models;

namespace ZooProjectApi.Services;

public class Animals : IAnimalService
{
    private List<Animal> _animals = new();
    private const string _saveFileName = "saveAnimals.json";
    private static readonly object _lock = new();

    public Animal AddAnimal(Animal animal)
    {
        lock (_lock)
        {
            LoadFromFile();
            if (_animals.Count < 1)
                animal.Id = 1;
            else
                animal.Id = _animals.Max(a => a.Id) + 1;
            _animals.Add(animal);
            SaveToFile();
            return animal;
        }
    }
    public bool DeleteAnimal(int id)
    {
        LoadFromFile();
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal is not null)
        {
            _animals.Remove(animal);
            SaveToFile();
            return true;
        }
        return false;
    }
    public bool FeedAnimal(int id, int amountFood)
    {
        LoadFromFile();
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal is not null)
        {
            animal.Energy += amountFood;
            if (animal.Energy > 100)
                animal.Energy = 100;
            SaveToFile();
            return true;
        }
        return false;
    }
    public Animal? GetAnimal(int id)
    {
        LoadFromFile();
        return _animals.FirstOrDefault(a => a.Id == id);
    }
    public List<Animal> GetAnimals()
    {
        LoadFromFile();
        return _animals;
    }
    private void LoadFromFile()
    {
        lock (_lock)
        {
            if (File.Exists(_saveFileName))
            {
                var json = File.ReadAllText(_saveFileName);
                _animals = JsonSerializer.Deserialize<List<Animal>>(json) ?? [];
            }
        }
    }
    private void SaveToFile()
    {
        var json = JsonSerializer.Serialize(_animals);
        File.WriteAllText(_saveFileName, json);
    }
}
