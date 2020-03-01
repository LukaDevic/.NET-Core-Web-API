using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SimpleWebAPI.Dtos;
using SimpleWebAPI.Entities;

namespace SimpleWebAPI.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly IMatchDetailsRepository _matchDetailsRepository;
        private readonly ILeagueTableRepository _leagueTableRepository;
        private readonly ILegueTableFactory _leagueTableFactory;
        private readonly IMapper _mapper;

        public LeagueService(
            IMatchDetailsRepository matchDetailsRepository,
            ILeagueTableRepository leagueTableRepository,
            ILegueTableFactory leagueTableFactory,
            IMapper mapper)
        {
            _matchDetailsRepository = matchDetailsRepository;
            _leagueTableRepository = leagueTableRepository;
            _leagueTableFactory = leagueTableFactory;
            _mapper = mapper;
        }

        public void UpdateLeagueDetails(IEnumerable<MatchDetailsDto> matchDetailsDtos)
        {
            var tables = matchDetailsDtos.GroupBy(x => x.LeagueTitle);

            foreach (var table in tables)
            {
                var matchesWithoutLeague = new List<MatchDetailsDto>();

                foreach (var matchDetailsDto in table)
                {
                    var matchDetail = _mapper.Map<MatchDetails>(matchDetailsDto);
                    _matchDetailsRepository.AddMatchDetails(matchDetail);
                    _matchDetailsRepository.Save();
                    matchesWithoutLeague.Add(matchDetailsDto);
                }

                var leagueFromDb = _leagueTableRepository.GetLeagueTable(table.Key);
                if (leagueFromDb == null)
                {
                    var leagueTable = _leagueTableFactory.CreateLeagueTable(matchesWithoutLeague);
                    _leagueTableRepository.AddLeagueTable(leagueTable);
                    _leagueTableRepository.Save();
                    continue; ;
                }

                UpdateLeagueTable(leagueFromDb);
            }
        }

        public void UpdateLeagueTable(LeagueTable leagueFromDb)
        {
            var oldMatches = _matchDetailsRepository.GetMatchesByLeagueName(leagueFromDb.LeagueTitle);
            var leagueTable = _leagueTableFactory.CreateLeagueTable(_mapper.Map<IList<MatchDetailsDto>>(oldMatches));
            leagueFromDb.Standings = leagueTable.Standings;
            _leagueTableRepository.Save();
        }

        public void UpdateMatchDetails(IEnumerable<MatchDetails> matchDetails)
        {
            foreach (var match in matchDetails)
            {
                var matchFromDb = _matchDetailsRepository.GetMatch(match.Id);
                var leagueFromDb = _leagueTableRepository.GetLeagueTable(match.LeagueTitle);

                if (matchFromDb == null)
                {
                    _matchDetailsRepository.AddMatchDetails(match);
                    if (leagueFromDb == null)
                    {
                        var listforLeagueCreation = new List<MatchDetails>();
                        listforLeagueCreation.Add(match);
                        var leagueTable = _leagueTableFactory
                                            .CreateLeagueTable(
                                                _mapper.Map<IList<MatchDetailsDto>>(listforLeagueCreation));
                        _leagueTableRepository.AddLeagueTable(leagueTable);
                    }
                    continue;
                }

                _matchDetailsRepository.UpdateMatch(match);
            }

            var leagues = matchDetails.Select(x=>x.LeagueTitle).Distinct();

            foreach (var league in leagues)
            {
                var leagueFromDb = _leagueTableRepository.GetLeagueTable(league);
                UpdateLeagueTable(leagueFromDb);
            }
            _matchDetailsRepository.Save();
        }
    }
}
