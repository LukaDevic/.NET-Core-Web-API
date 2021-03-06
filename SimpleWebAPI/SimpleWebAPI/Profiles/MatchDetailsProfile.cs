﻿using AutoMapper;

namespace SimpleWebAPI.Profiles
{
    public class MatchDetailsProfile : Profile
    {
        public MatchDetailsProfile()
        {
            CreateMap<Dtos.MatchDetailsDto, Entities.MatchDetails>();
            CreateMap<Entities.MatchDetails, Dtos.MatchDetailsDto>();
            CreateMap<Entities.LeagueTable, Dtos.LeagueTableDto>();
            CreateMap<Entities.TeamStatistics, Dtos.TeamStatisticsDto>();
        }
    }
}
