using System.Text.Json;
using ZooProjectApi.Models;

namespace ZooProjectApi.Services;

public class Animals : IAnimalService
{
    private List<Animal> _animals = new List<Animal>();
    private int _animalId = 1;
    private string saveFileName = "saveAnimals.json";
    public Animals()
    {
        LoadFromFile();
    }

    public Animal AddAnimal(Animal animal)
    {
        animal.Id = _animalId++;
        _animals.Add(animal);
        SaveToFile();
        return animal;
    }

    public void DeleteAnimal(int id)
    {
        Animal? animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal is not null)
            _animals.Remove(animal);
        SaveToFile();
    }

    public bool FeedAnimal(int id, int amountFood)
    {
        Animal? animal = _animals.FirstOrDefault(a => a.Id == id);
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
        return _animals.FirstOrDefault(a => a.Id == id);
    }

    public List<Animal> GetAnimals()
    {
        return _animals;
    }

    private void LoadFromFile()
    {
        if (File.Exists(saveFileName))
        {
            var json = File.ReadAllText(saveFileName);
            _animals = JsonSerializer.Deserialize<List<Animal>>(json);
            if (_animals.Count > 0) 
                _animalId = _animals.Max(a => a.Id) + 1;
        }
    }
    private void SaveToFile()
    {
        var json = JsonSerializer.Serialize(_animals);
        File.WriteAllText(saveFileName, json);
    }
}
