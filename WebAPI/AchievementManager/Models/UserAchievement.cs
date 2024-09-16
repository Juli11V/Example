using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AchievementManager.Models;

public record UserAchievement(string AccountId, string AchievementId, DateTime DateReceived)
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string AccountId { get; set; } = AccountId;

    [BsonRepresentation(BsonType.ObjectId)]
    public string AchievementId { get; set; } = AchievementId;

    public DateTime DateReceived { get; set; } = DateReceived;
}