using GB.MatchSimulator.Constants;
using GB.MatchSimulator.DAL.Repositories.Interfaces;
using GB.MatchSimulator.Helpers;
using GB.MatchSimulator.Mapping;
using GB.MatchSimulator.Models;
using GB.MatchSimulator.Services.Interfaces;

namespace GB.MatchSimulator.Services;

public class TournamentService(IRoundService roundService,
    ITeamRepository teamRepository, ITournamentRepository tournamentRepository,
    ILogger<TournamentService> logger) : ITournamentService
{
    public async Task<TournamentResult> SimulateTournament()
    {
        logger.LogInformation("Starting tournament simulation");

        var teams = await teamRepository.GetAllTeams();
        var teamNames = teams.Select(t => t.Name).ToList();

        var boardResults = InitializeBoard(teamNames);

        var rounds = roundService.GenerateRoundRobin(teamNames);
        var roundResults = new List<RoundResult>();

        foreach (var round in rounds)
        {
            var roundResult = await roundService.SimulateRound(round);
            roundResults.Add(roundResult);
            roundResult.Results.ForEach(r => UpdateBoard(boardResults, r));
        }

        CalculateHeadToHeadTieBreakers(boardResults, roundResults);

        var orderedResults = boardResults
            .OrderByDescending(r => r.Points)
            .ThenByDescending(r => r.GD)
            .ThenByDescending(r => r.For)
            .ThenBy(r => r.Against)
            .ThenByDescending(r => r.HeadToHead)
            .ToList();

        var tournamentResult = new TournamentResult() { Rounds = roundResults, ScoreBoard = orderedResults };
        await tournamentRepository.RegisterTournament(tournamentResult.ToNewEntity());

        logger.LogInformation($"Finished tournament simulation, " +
            $"Result: {string.Join(", ", tournamentResult.ScoreBoard.Select(sb => sb.Name))}");

        return tournamentResult;
    }

    public async Task<IEnumerable<TournamentResult>> GetAllTournaments()
    {
        var tournamentEntities = await tournamentRepository.GetAllTournaments();
        var result = tournamentEntities.Select(te => te.ToModel());
        return result;
    }

    private static List<BoardResult> InitializeBoard(List<string> teamNames)
    {
        var tournamentStart = teamNames.Select(name => new BoardResult() { Name = name });
        var boardResults = new List<BoardResult>(tournamentStart);
        return boardResults;
    }

    private static void UpdateBoard(List<BoardResult> boardResults, MatchResult matchResult)
    {
        var matchOutcome = MatchConstants.GetMatchOutcome(matchResult.Home.Score, matchResult.Away.Score);
        var homeBoard = boardResults.FirstOrDefault(br => br.Name.Equals(matchResult.Home.Name))!;
        var awayBoard = boardResults.FirstOrDefault(br => br.Name.Equals(matchResult.Away.Name))!;
        homeBoard.Played++;
        awayBoard.Played++;

        switch (matchOutcome)
        {
            case MatchConstants.MatchOutcome.HomeWins:
                homeBoard.Win++;
                homeBoard.Points += 3;
                homeBoard.For += matchResult.Home.Score;
                homeBoard.Against += matchResult.Away.Score;
                homeBoard.GD = homeBoard.For - homeBoard.Against;

                awayBoard.Loss++;
                awayBoard.For += matchResult.Away.Score;
                awayBoard.Against += matchResult.Home.Score;
                awayBoard.GD = awayBoard.For - awayBoard.Against;
                break;
            case MatchConstants.MatchOutcome.AwayWins:
                awayBoard.Win++;
                awayBoard.Points += 3;
                awayBoard.For += matchResult.Away.Score;
                awayBoard.Against += matchResult.Home.Score;
                awayBoard.GD = awayBoard.For - awayBoard.Against;

                homeBoard.Loss++;
                homeBoard.For += matchResult.Home.Score;
                homeBoard.Against += matchResult.Away.Score;
                homeBoard.GD = homeBoard.For - homeBoard.Against;
                break;

            default:
                homeBoard.Draw++;
                homeBoard.Points++;
                homeBoard.For += matchResult.Home.Score;
                homeBoard.Against += matchResult.Away.Score;
                homeBoard.GD = homeBoard.For - homeBoard.Against;

                awayBoard.Draw++;
                awayBoard.Points++;
                awayBoard.For += matchResult.Away.Score;
                awayBoard.Against += matchResult.Home.Score;
                awayBoard.GD = awayBoard.For - awayBoard.Against;
                break;
        }
    }

    private static void CalculateHeadToHeadTieBreakers(List<BoardResult> boardResults, List<RoundResult> roundResults)
    {
        foreach (var group in boardResults.GroupBy(r => new { r.Points, r.GD, r.For, r.Against }))
        {
            var tiedTeams = group.ToList();
            if (tiedTeams.Count <= 1) continue;

            foreach (var team in tiedTeams)
            {
                team.HeadToHead = tiedTeams
                    .Where(t => t != team)
                    .Sum(other => HeadToHeadCalculator
                    .Calculate(team.Name, other.Name, roundResults.SelectMany(r => r.Results)
                    .ToList()));
            }
        }
    }
}
