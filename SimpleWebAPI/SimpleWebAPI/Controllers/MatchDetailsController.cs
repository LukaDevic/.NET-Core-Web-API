using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleWebAPI.Dtos;
using SimpleWebAPI.Entities;
using SimpleWebAPI.ResourceParameters;
using SimpleWebAPI.Services;
using System.Collections.Generic;

namespace SimpleWebAPI.Controllers
{
    [ApiController]
    [Route("api/matchdetails")]
    public class MatchDetailsController : ControllerBase
    {
        private readonly IMatchDetailsRepository _matchDetailsRepository;
        private readonly ILeagueTableRepository _leagueTableRepository;
        private readonly ILeagueService _leagueService;
        private readonly IMapper _mapper;

        public MatchDetailsController(
            IMatchDetailsRepository matchDetailsRepository,
            ILeagueTableRepository leagueTableRepository,
            ILeagueService leagueService,
            IMapper mapper)
        {
            _matchDetailsRepository = matchDetailsRepository;
            _leagueTableRepository = leagueTableRepository;
            _leagueService = leagueService;
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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _leagueService.UpdateLeagueDetails(matchDetailsDtos);
            var leagueTableList = _leagueTableRepository.GetLeagueTables();
            return Ok(new { results = _mapper.Map<IList< LeagueTableDto >>(leagueTableList)});
        }

        [HttpPut]
        public IActionResult UpdateMatches(IEnumerable<MatchDetails> matchDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _leagueService.UpdateMatchDetails(matchDetails);
            return Ok();
        }
    }
}