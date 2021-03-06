﻿using System;
using System.Collections.Generic;
using SimpleWebAPI.Entities;

namespace SimpleWebAPI.Services
{
    public interface ILeagueTableRepository
    {
        IEnumerable<LeagueTable> GetLeagueTables();
        IEnumerable<LeagueTable> GetLeagueTables(string searchQuery);
        LeagueTable GetLeagueTable(string leagueTitle);
        LeagueTable GetLeagueTable(Guid tableId);
        void AddLeagueTable(LeagueTable leagueTable);
        bool Save();
        void DeleteLeagueTable(Guid tableId);
        void DeleteLeagueTables();
    }
}
