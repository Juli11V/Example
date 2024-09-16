using AchievementManager.Models;
using Microsoft.Extensions.Options;
using WebAPI.Application.DTOs;
using MongoDB.Driver;
using Newtonsoft.Json;


namespace AchievementManager.Services;

public class AchievementService(IOptions<MongoDbOptions> options) : IAchievementService
{
    private readonly MongoDbOptions _options = options.Value;

    public async Task ProvideAchievement(string transferMessage)
    {
        var dto = JsonConvert.DeserializeObject<TransferMessageDto>(transferMessage);

        switch (dto?.Amount)
        {
            case >= 10_000 and < 100_000:
                await CreateAchievement(dto, "Silver Surfer");
                break;
            case >= 100_000 and < 1_000_000:
                await CreateAchievement(dto, "Golden Glider");
                break;
            case >= 1_000_000:
                await CreateAchievement(dto,  "Platinum Pioneer");
                break;
        }
    }

    public List<Achievement> GetAchievements()
    {
        var db = GetDatabase();
        var collection = db.GetCollection<Achievement>(_options.AchievementsCollection).AsQueryable();

        return collection.ToList();
    }

    private IMongoDatabase GetDatabase()
    {
        var client = new MongoClient(_options.ConnectionString);
        return client.GetDatabase(_options.Database);
    }

    private async Task CreateAchievement(TransferMessageDto dto, string title)
    {
        var db = GetDatabase();
        var achievementsCollection = db.GetCollection<Achievement>(_options.AchievementsCollection);
        var userAchievementsCollection = db.GetCollection<UserAchievement>(_options.UserAchievementsCollection);

        var achievement = await achievementsCollection.Find(x => x.Title == title).FirstAsync();
        
        await userAchievementsCollection.InsertOneAsync(new UserAchievement(dto.SenderId.ToString(), achievement.Id,
            dto.Timestamp));
    }
}