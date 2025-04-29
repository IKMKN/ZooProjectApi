using System.Text.Json.Serialization;

namespace ZooProjectApi.Contracts;

public class FeedRequest
{
    [JsonPropertyName("foodAmount")]
    public int FoodAmount { get; set; }
}
