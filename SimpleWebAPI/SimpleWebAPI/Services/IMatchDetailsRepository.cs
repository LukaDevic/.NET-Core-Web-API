using System.Collections.Generic;
using SimpleWebAPI.Entities;

namespace SimpleWebAPI.Services
{
    public interface IMatchDetailsRepository
    {
        IEnumerable<MatchDetails> GetMatches();
        void AddMatchDetails(MatchDetails matchDetails);
        bool Save();
        void DeleteMatchDetailsEntries();
    }
}
