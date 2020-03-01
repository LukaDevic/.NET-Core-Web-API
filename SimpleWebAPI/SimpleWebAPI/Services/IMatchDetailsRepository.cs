using System;
using System.Collections.Generic;
using SimpleWebAPI.Entities;
using SimpleWebAPI.ResourceParameters;

namespace SimpleWebAPI.Services
{
    public interface IMatchDetailsRepository
    {
        IEnumerable<MatchDetails> GetMatches();
        IEnumerable<MatchDetails> GetMatches(MatchDetailsResourceParameters matchDetailsResourceParameters);
        MatchDetails GetMatch(Guid matchId);
        IEnumerable<MatchDetails> GetMatchesByLeagueName(string key);
        void AddMatchDetails(MatchDetails matchDetails);
        void UpdateMatch(MatchDetails matchFromDb);
        bool Save();
        void DeleteMatchDetailsEntries();
    }
}
