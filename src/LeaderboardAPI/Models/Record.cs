
namespace LeaderboardAPI.Models
{
    public class Record
    {
        public int ID {get; set;}
        public string CompetitionName {get; set;}
        public string TeamName {get; set;}
        public string UserNames {get; set;}
        public double Score {get; set;}
        public System.DateTime ScoreFirstSubmittedDate {get; set;}
        public int NumSubmissions {get; set;}
    }
}