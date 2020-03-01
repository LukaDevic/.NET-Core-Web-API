using SimpleWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleWebAPI.DbContexts;

namespace SimpleWebAPI.Services
{
    public class LeagueTableRepository : ILeagueTableRepository
    {
        private readonly MatchDetailsContext _context;

        public LeagueTableRepository(MatchDetailsContext context)
        {
            _context = context;
        }

        public IEnumerable<LeagueTable> GetLeagueTables()
        {
            return _context.LeagueTables.Include(x => x.Standings).ToList();
        }

        public IEnumerable<LeagueTable> GetLeagueTables(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
             return _context.LeagueTables.Include(x => x.Standings).ToList();
            }
            var trimmedSearchQuery = searchQuery.Trim();
            var collection = _context.LeagueTables.Include(x => x.Standings) as IQueryable<LeagueTable>;
            collection = collection.Where(x => x.LeagueTitle.Contains(trimmedSearchQuery));

            return collection.ToList();
        }

        public LeagueTable GetLeagueTable(string leagueTitle)
        {
            if (leagueTitle == null)
            {
                throw new ArgumentNullException(nameof(leagueTitle));
            }

            var leagueTable = _context.LeagueTables
                                      .Include(x => x.Standings)
                                      .SingleOrDefault(x => x.LeagueTitle == leagueTitle);

            return leagueTable;
        }

        public LeagueTable GetLeagueTable(Guid tableId)
        {
            if (tableId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(tableId));
            }

            var leagueTable = _context.LeagueTables.Include(x=>x.Standings).FirstOrDefault();

            return leagueTable;
        }

        public void AddLeagueTable(LeagueTable leagueTable)
        {
            if (leagueTable == null)
            {
                throw new ArgumentNullException(nameof(leagueTable));
            }

            leagueTable.Id = Guid.NewGuid();

            _context.LeagueTables.Add(leagueTable);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteLeagueTable(Guid tableId)
        {
            if (tableId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(tableId));
            }

            var leagueTable = _context.LeagueTables
                                      .Include(x => x.Standings)
                                      .FirstOrDefault(x=>x.Id == tableId);
            _context.LeagueTables.Remove(leagueTable);
            _context.SaveChanges();
        }

        public void DeleteLeagueTables()
        {
            _context.LeagueTables.RemoveRange(_context.LeagueTables);
            _context.TeamStatistics.RemoveRange(_context.TeamStatistics);
            _context.SaveChanges();
        }


    }
}
