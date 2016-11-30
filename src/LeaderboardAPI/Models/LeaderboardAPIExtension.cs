using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LeaderboardAPI.Models
{
    public static class LeaderboardAPIExtension
    {
        public static void EnsureSeedData(this LeaderboardAPIContext context)
        {
            if (!context.Records.Any())
            {
                context.Records.Add(new Record{ 
                    ID = 1, 
                    CompetitionName = "ClaimPredictionChallenge",
                    TeamName = "Matt C",
                    UserNames = "Matthew Carle",
                    Score = 0.201556870289149,
                    ScoreFirstSubmittedDate = System.DateTime.Parse("2011-10-11 13:22:00.163333"),
                    NumSubmissions = 35 });
                
                context.SaveChanges();
            }
            
        }
    }
}