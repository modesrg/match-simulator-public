using GB.MatchSimulator.DAL.Repositories.Interfaces;
using GB.MatchSimulator.Helpers;
using GB.MatchSimulator.Mapping;
using GB.MatchSimulator.Models;
using GB.MatchSimulator.Options;
using GB.MatchSimulator.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace GB.MatchSimulator.Services;

public class MatchService(ITeamRepository teamRepository, IOptions<SimulatorOptions> options,
    ILogger<MatchService> logger) : IMatchService
{
    public MatchResult SimulateMatch(Fixture match)
    {
        logger.LogInformation($"Simulating match {match.Home} vs {match.Away}");

        var homeTeamEntity = teamRepository.GetTeamByName(match.Home);
        var awayTeamEntity = teamRepository.GetTeamByName(match.Away);

        if (homeTeamEntity is null || awayTeamEntity is null)
            throw new KeyNotFoundException("Team not found");

        var homeInfo = homeTeamEntity.ToMatch();
        var awayInfo = awayTeamEntity.ToMatch();

        double homePower = CalculateTeamPower(homeInfo.Offense - awayInfo.Defense);
        double awayPower = CalculateTeamPower(awayInfo.Offense - homeInfo.Defense);

        var totalPower = homePower + awayPower;
        var homeLambda = options.Value.AverageScorePerMatch * (homePower / totalPower);
        var awayLambda = options.Value.AverageScorePerMatch * (awayPower / totalPower);


        var result = new MatchResult()
        {
            Home = new TeamResult() { Name = homeInfo.Name, Score = ChanceCalculator.PoissonScore(homeLambda) },
            Away = new TeamResult() { Name = awayInfo.Name, Score = ChanceCalculator.PoissonScore(awayLambda) }
        };

        logger.LogInformation($"Match result: {result.Home.Name} {result.Home.Score} - {result.Away.Score} {result.Away.Name}");

        return result;
    }

    private static double CalculateTeamPower(double teamBasePower)
    {
        // This could be tweaked with boosters in the future (Home advantage, Injured players, etc)
        return Math.Max(1, teamBasePower);
    }
}
