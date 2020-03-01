using SimpleWebAPI.Entities;
using System.Collections.Generic;
using SimpleWebAPI.Dtos;

namespace SimpleWebAPI.Services
{
    public interface ILegueTableFactory
    {
        LeagueTable CreateLeagueTable(IEnumerable<MatchDetailsDto> matchDetails);
    }
}
