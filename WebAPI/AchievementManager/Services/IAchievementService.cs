using AchievementManager.Models;

namespace AchievementManager.Services;

public interface IAchievementService
{
    Task ProvideAchievement(string transferMessage);
    List<Achievement> GetAchievements();
}