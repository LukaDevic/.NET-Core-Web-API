using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebAPI.Entities
{
    public class LeagueTable
    {
        [Key]
        public Guid Id { get; set; }
        public string LeagueTitle { get; set; }

        public int Matchday { get; set; }
        public string Group { get; set; }

        public IEnumerable<TeamStatistics> Standings { get; set; }

    }
}
