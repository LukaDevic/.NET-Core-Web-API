using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleWebAPI.Dtos;
using SimpleWebAPI.Entities;
using SimpleWebAPI.Services;
using System.Collections.Generic;
using System.Linq;

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

        [HttpPost]
        public IActionResult AddResults(IEnumerable<MatchDetailsDto> matchDetailsDtos)
        {
            //For testing
            _matchDetailsRepository.DeleteMatchDetailsEntries();
            _leagueTableRepository.DeleteLeagueTables();

            var tables = matchDetailsDtos.GroupBy(x => x.LeagueTitle);

            foreach (var table in tables)
            {
                var matches = new List<MatchDetailsDto>();
                foreach (var matchDetailsDto in table)
                {
                    var matchDetail = _mapper.Map<MatchDetails>(matchDetailsDto);
                    _matchDetailsRepository.AddMatchDetails(matchDetail);
                    _matchDetailsRepository.Save();
                    matches.Add(matchDetailsDto);
                }

                var leagueTable = _legueTableFactory.CreateLeagueTable(matches);
                _leagueTableRepository.AddLeagueTable(leagueTable);
                _leagueTableRepository.Save();
            }
            var leagueTableList = _leagueTableRepository.GetLeagueTables();

            return Ok(new { results = _mapper.Map<IList< LeagueTableDto >>(leagueTableList)});
        }
    }
}