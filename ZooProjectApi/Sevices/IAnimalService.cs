﻿using ZooProjectApi.Models;

namespace ZooProjectApi.Sevices;

public interface IAnimalService
{
    List<Animal> GetAnimals();
    Animal? GetAnimal(int id);
    Animal AddAnimal(Animal animal);
    Animal? FeedAnimal(int id, int amountFood);
    void DeleteAnimal(int id);
}