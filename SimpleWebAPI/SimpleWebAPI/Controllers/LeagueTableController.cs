using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleWebAPI.Dtos;
using SimpleWebAPI.Services;
using System;
using System.Collections.Generic;

namespace SimpleWebAPI.Controllers
{
    [ApiController]
    [Route("api/leagueTables")]
    public class LeagueTableController : ControllerBase
    {
        private readonly ILeagueTableRepository _leagueTableRepository;
        private readonly IMapper _mapper;

        public LeagueTableController(
            ILeagueTableRepository leagueTableRepository,
            IMapper mapper)
        {
            _leagueTableRepository = leagueTableRepository;
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