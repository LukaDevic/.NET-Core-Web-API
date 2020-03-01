using Microsoft.EntityFrameworkCore;
using SimpleWebAPI.Entities;

namespace SimpleWebAPI.DbContexts
{
    public class MatchDetailsContext : DbContext
    {
        public MatchDetailsContext(DbContextOptions<MatchDetailsContext> options)
        : base(options)
        {
            
        }

        public DbSet<MatchDetails> MatchDetails { get; set; }
        public DbSet<LeagueTable> LeagueTables { get; set; }
        public DbSet<TeamStatistics> TeamStatistics { get; set; }
    }
}
