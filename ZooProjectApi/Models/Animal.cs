namespace ZooProjectApi.Models;

public class Animal
{
    public Animal (string name, string type) 
    {
        Name = name;
        Type = type;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Energy { get; set; } = 100;
}

