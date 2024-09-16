using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AchievementManager.Models;

public class Achievement
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
}