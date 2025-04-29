using System.ComponentModel.DataAnnotations;

namespace ZooProjectApi.Contracts;

public class AnimalRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Type { get; set; }
}
