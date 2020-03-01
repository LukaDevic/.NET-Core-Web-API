using System.Collections.Generic;
using SimpleWebAPI.Entities;

namespace SimpleWebAPI.Dtos
{
    public class LeagueTableDto
    {
        public string LeagueTitle { get; set; }
        public int Matchday { get; set; }
        public string Group { get; set; }
        public IEnumerable<TeamStatisticsDto> Standings { get; set; }
    }

}
