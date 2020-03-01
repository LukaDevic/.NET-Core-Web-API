using SimpleWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using SimpleWebAPI.DbContexts;

namespace SimpleWebAPI.Services
{
    public class MatchDetailsRepository : IMatchDetailsRepository
    {
        private readonly MatchDetailsContext _context;

        public MatchDetailsRepository(MatchDetailsContext context)
        {
            _context = context;
        }

        public IEnumerable<MatchDetails> GetMatches()
        {
            return _context.MatchDetails.ToList();
        }

        public void AddMatchDetails(MatchDetails matchDetails)
        {
            if (matchDetails == null)
            {
                throw new ArgumentNullException(nameof(matchDetails));
            }

            matchDetails.Id = Guid.NewGuid();

            _context.MatchDetails.Add(matchDetails);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteMatchDetailsEntries()
        {
            _context.MatchDetails.RemoveRange(_context.MatchDetails);
            _context.SaveChanges();
        }


    }
}
