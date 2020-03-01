using System;

namespace SimpleWebAPI.ResourceParameters
{
    public class MatchDetailsResourceParameters
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Group { get; set; }
        public string HomeTeam { get; set; }
    }
}
