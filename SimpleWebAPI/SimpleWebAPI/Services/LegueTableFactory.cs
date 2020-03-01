using SimpleWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using SimpleWebAPI.Dtos;

namespace SimpleWebAPI.Services
{
    public class LegueTableFactory : ILegueTableFactory
    {
        public LeagueTable CreateLeagueTable(IEnumerable<MatchDetailsDto> matchDetails)
        {
            var leagueTable = new LeagueTable();
            var firstMatchDetails = matchDetails.OrderByDescending(x => x.Matchday).FirstOrDefault();
            if (firstMatchDetails == null)
            {
                return leagueTable;
            }

            leagueTable.LeagueTitle = firstMatchDetails.LeagueTitle;
            leagueTable.Matchday = firstMatchDetails.Matchday;
            leagueTable.Group = firstMatchDetails.Group;
            leagueTable.Standings = CreateStandings(matchDetails);

            return leagueTable;
        }

        private IEnumerable<TeamStatistics> CreateStandings(IEnumerable<MatchDetailsDto> matchDetails)
        {
            var teamStatisticsList = new List<TeamStatistics>();
            foreach (var matchDetail in matchDetails)
            {
                var homeTeam = matchDetail.HomeTeam;
                var awayTeam = matchDetail.AwayTeam;
                var results = matchDetail.Score
                                                 .Split(":")
                                                 .Select(Int32.Parse)
                                                 .ToList();

                AddHomeTeamStatistic(homeTeam, awayTeam, results, teamStatisticsList);
                AddAwayTeamStatistic(homeTeam, awayTeam, results, teamStatisticsList);
            }

            var orderedteamStatisticsList = 
                teamStatisticsList.OrderByDescending(x=>x.Points)
                                  .ThenByDescending(x=>x.Goals)
                                  .ThenByDescending(x=>x.GoalDifference)
                                  .ToList();

            for (var i = 0; i < orderedteamStatisticsList.Count(); i++)
            {
                orderedteamStatisticsList[i].Rank = i + 1;
            }
            return orderedteamStatisticsList;
        }

        private void AddAwayTeamStatistic(string homeTeam, string awayTeam, List<int> results, List<TeamStatistics> teamStatisticsList)
        {
            var homeTeamScore = results.First();
            var awayTeamScore = results.Last();
            var awayTeamPoints = GetPoints(homeTeamScore, awayTeamScore);
            var awayTeamExistInList = true;
            var awayTeamStatistics = teamStatisticsList.FirstOrDefault(x => x.Team == awayTeam);
            if (awayTeamStatistics == null)
            {
                awayTeamExistInList = false;
                awayTeamStatistics = new TeamStatistics();
            }

            awayTeamStatistics.Team = awayTeam;
            awayTeamStatistics.PlayedGames++;
            awayTeamStatistics.Points += awayTeamPoints;
            awayTeamStatistics.Goals += awayTeamScore;
            awayTeamStatistics.GoalsAgainst += homeTeamScore;
            awayTeamStatistics.GoalDifference = awayTeamStatistics.Goals - awayTeamStatistics.GoalsAgainst;
            UpdateWinLoseDraw(awayTeamStatistics, awayTeamPoints);

            if (!awayTeamExistInList)
            {
                teamStatisticsList.Add(awayTeamStatistics);
            }
        }

        private void AddHomeTeamStatistic(string homeTeam, string awayTeam, List<int> results,
            List<TeamStatistics> teamStatisticsList)
        {
            var homeTeamScore = results.First();
            var awayTeamScore = results.Last();
            var homeTeamPoints = GetPoints(homeTeamScore, awayTeamScore, true);
            var homeTeamExistInList = true;
            var homeTeamStatistics = teamStatisticsList.FirstOrDefault(x => x.Team == homeTeam);
            if (homeTeamStatistics == null)
            {
                homeTeamExistInList = false;
                homeTeamStatistics = new TeamStatistics();
            }
            homeTeamStatistics.Team = homeTeam;
            homeTeamStatistics.PlayedGames++;
            homeTeamStatistics.Points += homeTeamPoints;
            homeTeamStatistics.Goals += homeTeamScore;
            homeTeamStatistics.GoalsAgainst += awayTeamScore;
            homeTeamStatistics.GoalDifference = homeTeamStatistics.Goals - homeTeamStatistics.GoalsAgainst;
            UpdateWinLoseDraw(homeTeamStatistics, homeTeamPoints);

            if (!homeTeamExistInList)
            {
                teamStatisticsList.Add(homeTeamStatistics);
            }
        }

        private void UpdateWinLoseDraw(TeamStatistics teamStatistics, int teamPoints)
        {
            if (teamPoints == 3)
            {
                teamStatistics.Win++;
            }
            else if(teamPoints == 1)
            {
                teamStatistics.Draw++;
            }
            else
            {
                teamStatistics.Lose++;
            }
        }

        private int GetPoints(int homeTeamScore, int awayTeamScore, bool isHomeTeam = false)
        {
            if (isHomeTeam)
            {
                if (homeTeamScore > awayTeamScore) return 3;
                return homeTeamScore == awayTeamScore ? 1 : 0;
            }
            if (awayTeamScore > homeTeamScore) return 3;
            return awayTeamScore == homeTeamScore ? 1 : 0;
        }
    }
}
