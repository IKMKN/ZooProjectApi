using System.Text.Json.Serialization;

namespace ZooProjectApi.Models;

public class FeedRequest
{
    [JsonPropertyName("foodAmount")]
    public int FoodAmount { get; set; }
}
