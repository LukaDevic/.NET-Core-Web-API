using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleWebAPI.Dtos;
using SimpleWebAPI.Entities;
using SimpleWebAPI.Services;
using System.Collections.Generic;
using System.Linq;
using SimpleWebAPI.ResourceParameters;

namespace SimpleWebAPI.Controllers
{
    [ApiController]
    [Route("api/matchdetails")]
    public class MatchDetailsController : ControllerBase
    {
        private readonly IMatchDetailsRepository _matchDetailsRepository;
        private readonly ILeagueTableRepository _leagueTableRepository;
        private readonly ILegueTableFactory _legueTableFactory;
        private readonly IMapper _mapper;

        public MatchDetailsController(
            IMatchDetailsRepository matchDetailsRepository,
            ILeagueTableRepository leagueTableRepository,
            ILegueTableFactory legueTableFactory,
            IMapper mapper)
        {
            _matchDetailsRepository = matchDetailsRepository;
            _leagueTableRepository = leagueTableRepository;
            _legueTableFactory = legueTableFactory;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetResults(
            [FromQuery] MatchDetailsResourceParameters matchDetailsResourceParameters)
        {
            var matches = _matchDetailsRepository.GetMatches(matchDetailsResourceParameters);
            return Ok(_mapper.Map<IList<MatchDetailsDto>>(matches));
        }

        [HttpPost]
        public IActionResult AddResults(IEnumerable<MatchDetailsDto> matchDetailsDtos)
        {
            //For testing
            _matchDetailsRepository.DeleteMatchDetailsEntries();
            _leagueTableRepository.DeleteLeagueTables();

            var tables = matchDetailsDtos.GroupBy(x => x.LeagueTitle);

            foreach (var table in tables)
            {
                var matchesWithoutLegue = new List<MatchDetailsDto>();
                foreach (var matchDetailsDto in table)
                {
                    var matchDetail = _mapper.Map<MatchDetails>(matchDetailsDto);
                    _matchDetailsRepository.AddMatchDetails(matchDetail);
                    _matchDetailsRepository.Save();
                    matchesWithoutLegue.Add(matchDetailsDto);
                }

                var leagueFromDb = _leagueTableRepository.GetLeagueTable(table.Key);
                LeagueTable leagueTable;
                if (leagueFromDb == null)
                {
                    leagueTable = _legueTableFactory.CreateLeagueTable(matchesWithoutLegue);
                    _leagueTableRepository.AddLeagueTable(leagueTable);
                    _leagueTableRepository.Save();
                    continue;;
                }

                var oldMatches = _matchDetailsRepository.GetMatchesByLeagueName(table.Key);
                _leagueTableRepository.DeleteLeagueTable(leagueFromDb.Id);
                leagueTable = _legueTableFactory.CreateLeagueTable(_mapper.Map<IList<MatchDetailsDto>>(oldMatches));
                _leagueTableRepository.AddLeagueTable(leagueTable);
                _leagueTableRepository.Save();
            }
            var leagueTableList = _leagueTableRepository.GetLeagueTables();

            return Ok(new { results = _mapper.Map<IList< LeagueTableDto >>(leagueTableList)});
        }

        [HttpPut]
        public IActionResult UpdateMatches(IEnumerable<MatchDetails> matchDetails)
        {
            foreach (var match in matchDetails)
            {
                var matchFromDb = _matchDetailsRepository.GetMatch(match.Id);
                LeagueTable leagueTable;
                var leagueFromDb = _leagueTableRepository.GetLeagueTable(match.LeagueTitle);

                if (matchFromDb == null)
                {
                    _matchDetailsRepository.AddMatchDetails(match);
                    _matchDetailsRepository.Save();
                    if (leagueFromDb == null)
                    {
                        leagueTable = _legueTableFactory.CreateLeagueTable(_mapper.Map<IList<MatchDetailsDto>>(match));
                        _leagueTableRepository.AddLeagueTable(leagueTable);
                        _leagueTableRepository.Save();
                    }
                    continue;
                }

                _matchDetailsRepository.UpdateMatch(match);
                var oldMatches = _matchDetailsRepository.GetMatchesByLeagueName(matchFromDb.LeagueTitle);
                _leagueTableRepository.DeleteLeagueTable(leagueFromDb.Id);
                leagueTable = _legueTableFactory.CreateLeagueTable(_mapper.Map<IList<MatchDetailsDto>>(oldMatches));
                _leagueTableRepository.AddLeagueTable(leagueTable);
                _leagueTableRepository.Save();
            }

            _matchDetailsRepository.Save();
            return NoContent();
        }
    }
}