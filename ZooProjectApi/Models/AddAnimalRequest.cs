using System.ComponentModel.DataAnnotations;

namespace ZooProjectApi.Models;

public class AddAnimalRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Type { get; set; }
}
