using System.ComponentModel.DataAnnotations;

namespace ZooProjectApi.Models;

public class Animal
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public int Energy { get; set; } = 100;
}
