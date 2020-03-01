using System.Collections.Generic;
using SimpleWebAPI.Dtos;
using SimpleWebAPI.Entities;

namespace SimpleWebAPI.Services
{
    public interface ILeagueService
    {
        void UpdateLeagueDetails(IEnumerable<MatchDetailsDto> matchDetailsDtos);
        void UpdateLeagueTable(string leagueName, LeagueTable leagueFromDb);
        void UpdateMatchDetails(IEnumerable<MatchDetails> matchDetails);
    }
}
