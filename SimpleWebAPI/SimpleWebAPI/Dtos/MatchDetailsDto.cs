
using System.ComponentModel.DataAnnotations;

namespace SimpleWebAPI.Dtos
{
    public class MatchDetailsDto
    {
        [Required]
        [MaxLength(50)]
        public string LeagueTitle { get; set; }
        public int Matchday { get; set; }
        [Required]
        [MaxLength(1)]
        public string Group { get; set; }
        [Required]
        [MaxLength(50)]
        public string HomeTeam { get; set; }
        [Required]
        [MaxLength(50)]
        public string AwayTeam { get; set; }
        [Required]
        public string KickoffAt { get; set; }
        [MaxLength(10)]
        public string Score { get; set; }
    }

}
