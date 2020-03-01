using SimpleWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using SimpleWebAPI.DbContexts;
using SimpleWebAPI.ResourceParameters;

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

        public IEnumerable<MatchDetails> GetMatches(
            MatchDetailsResourceParameters matchDetailsResourceParameters)
        {
            if (matchDetailsResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(matchDetailsResourceParameters));
            }

            var collection = _context.MatchDetails as IQueryable<MatchDetails>;
            if (!string.IsNullOrEmpty(matchDetailsResourceParameters.Group))
            {
                var group = matchDetailsResourceParameters.Group.Trim();
                collection = collection.Where(x => x.Group == group);
            }

            if (matchDetailsResourceParameters.StartDate != null)
            {
                collection = collection.Where(x => x.KickoffAt >= matchDetailsResourceParameters.StartDate);
            }

            if (matchDetailsResourceParameters.EndDate != null)
            {
                collection = collection.Where(x => x.KickoffAt <= matchDetailsResourceParameters.EndDate);
            }

            return collection.ToList();
        }

        public MatchDetails GetMatch(Guid matchId)
        {
            return _context.MatchDetails.FirstOrDefault(x => x.Id == matchId);
        }

        public IEnumerable<MatchDetails> GetMatchesByLeagueName(string key)
        {
            return _context.MatchDetails.Where(x=> x.LeagueTitle == key).ToList();
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

        public void UpdateMatch(MatchDetails match)
        {
            if (match == null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            var matchFromDb = _context.MatchDetails
                                                .FirstOrDefault(x=>x.Id == match.Id);

            matchFromDb.LeagueTitle = match.LeagueTitle;
            matchFromDb.Matchday = match.Matchday;
            matchFromDb.Group = match.Group;
            matchFromDb.HomeTeam = match.HomeTeam;
            matchFromDb.AwayTeam = match.AwayTeam;
            matchFromDb.KickoffAt = match.KickoffAt;
            matchFromDb.Score = match.Score;
            _context.SaveChanges();
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
