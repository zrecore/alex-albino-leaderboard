using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using CsvHelper;

namespace LeaderboardAPI.Models
{
    public static class LeaderboardAPIExtension
    {
        public static void EnsureSeedData(this LeaderboardAPIContext context)
        {
            if (!context.Records.Any())
            {
                
                using (TextReader textReader = File.OpenText("./SeedData/leaderboards.csv"))
                {
                    var csv = new CsvReader(textReader);
                    var intLine = 0;

                    while( csv.Read() )
                    {
                        if (intLine > 0) {
                            context.Records.Add(new Record{ 
                                ID = intLine, 
                                CompetitionName = csv.GetField<string>(0),
                                TeamName = csv.GetField<string>(1),
                                UserNames = csv.GetField<string>(2),
                                Score = csv.GetField<double>(3),
                                ScoreFirstSubmittedDate = System.DateTime.Parse(csv.GetField<string>(4)),
                                NumSubmissions = csv.GetField<int>(5) });
                        }

                        intLine++; 

                    }

                }
                context.SaveChanges();
            }
            
        }
    }
}