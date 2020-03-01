using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleWebAPI.Dtos;
using SimpleWebAPI.Entities;
using SimpleWebAPI.Services;
using System.Collections.Generic;

namespace SimpleWebAPI.Controllers
{
    [ApiController]
    [Route("api/leagueTables")]
    public class LeagueTableController : ControllerBase
    {
        private readonly IMatchDetailsRepository _matchDetailsRepository;
        private readonly ILeagueTableRepository _leagueTableRepository;
        private readonly ILegueTableFactory _legueTableFactory;
        private readonly IMapper _mapper;

        public LeagueTableController(
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
        public IActionResult GetTables(string searchQuery)
        {
            var leagueTableList = _leagueTableRepository.GetLeagueTables(searchQuery);
            return Ok(_mapper.Map<IList<LeagueTableDto>>(leagueTableList));
        }


        [HttpGet("{tableId}")]
        public IActionResult GetTable(Guid tableId)
        {
            var tableFromRepo = _leagueTableRepository.GetLeagueTable(tableId);
            if (tableFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<LeagueTableDto>(tableFromRepo));
        }
    }
}