namespace AchievementManager;

public class MongoDbOptions
{
    public const string MongoDb = "MongoDb";
    public string ConnectionString { get; set; } = String.Empty;
    public string Database { get; set; } = String.Empty;
    public string AchievementsCollection { get; set; } = String.Empty;
    public string UserAchievementsCollection { get; set; } = String.Empty;
}