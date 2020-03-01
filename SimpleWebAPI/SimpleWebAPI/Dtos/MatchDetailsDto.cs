using System;

namespace SimpleWebAPI.Dtos
{
    public class MatchDetailsDto
    {
        public string LeagueTitle { get; set; }
        public int Matchday { get; set; }
        public string Group { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string KickoffAt { get; set; }
        public string Score { get; set; }
    }

}
