using MongoDB.Bson.Serialization.Attributes;

public class Counter
{
    [BsonId]
    public string Id { get; set; } 

    public long Value { get; set; } 
}
