using GB.MatchSimulator.Extensions;
using GB.MatchSimulator.Models;
using GB.MatchSimulator.Options;
using GB.MatchSimulator.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace GB.MatchSimulator.Services;

public class RoundService(IMatchService matchService, IOptions<SimulatorOptions> options
    , ILogger<RoundService> logger) : IRoundService
{

    public async Task<RoundResult> SimulateRound(Round round)
    {
        logger.LogInformation($"Simulating round: {round.Name}");

        var matchResults = new List<MatchResult>();
        round.Matches.ForEach(async m => matchResults.Add(await matchService.SimulateMatch(m)));
        return new RoundResult() { Name = round.Name, Results = matchResults };
    }

    public List<Round> GenerateRoundRobin(List<string> teamList)
    {
        var rounds = !options.Value.RandomFixtures
            ? DefaultRounds(teamList)
            : RealRoundRobin(teamList);

        if (options.Value.SecondStage)
            rounds.AddRange(GenerateSecondHalfRounds(rounds));

        return rounds;
    }

    private List<Round> GenerateSecondHalfRounds(List<Round> rounds)
    {
        var secondHalfRounds = new List<Round>();
        var secondHalfIndex = rounds.Count + 1;

        foreach (var round in rounds)
        {
            var revertedMatches = round.Matches.Select(m => new Fixture() { Home = m.Away, Away = m.Home });

            secondHalfRounds.Add(
                new Round()
                {
                    Name = "Round " + secondHalfIndex,
                    Matches = revertedMatches.ToList()
                });
            secondHalfIndex++;
        }

        return secondHalfRounds;
    }

    private List<Round> RealRoundRobin(List<string> teamList)
    {
        logger.LogInformation("Generating random rounds");

        teamList.Shuffle();

        var randomRounds = new List<Round>();
        var numTeams = teamList.Count;

        var totalRounds = (numTeams - 1);
        var halfSize = numTeams / 2;
        List<string> teams = new();
        teams.AddRange(teamList); // Copy all the elements.
        teams.RemoveAt(0); // Exclude the first team.

        var teamSize = teams.Count;
        for (var roundNo = 0; roundNo < totalRounds; roundNo++)
        {
            var teamIdx = roundNo % teamSize;

            var baseHomeTeam = teams[teamIdx];
            var baseAwayTeam = teamList[0];

            var newRound = new Round()
            {
                Name = $"Round {roundNo + 1}",
                Matches = new List<Fixture>() { new Fixture { Home = baseHomeTeam, Away = baseAwayTeam } }
            };

            for (int idx = 1; idx < halfSize; idx++)
            {
                var firstTeamIdx = (roundNo + idx) % teamSize;
                var secondTeamIdx = (roundNo + teamSize - idx) % teamSize;

                var subHomeTeam = teams[firstTeamIdx];
                var subAwayTeam = teams[secondTeamIdx];

                newRound.Matches.Add(new Fixture() { Home = subHomeTeam, Away = subAwayTeam });

                randomRounds.Add(newRound);
            }
        }

        return randomRounds;
    }

    private List<Round> DefaultRounds(List<string> teamList)
    {
        logger.LogInformation("Generating default rounds");

        var defaultRounds = new List<Round>()
        {
            new Round()
            {
                Name = "Round 1",
                Matches = new List<Fixture>()
            {
                new Fixture(){ Home = teamList[0], Away = teamList[3]},
                new Fixture(){ Home = teamList[1], Away = teamList[2]}
            }
            },
            new Round()
            {
                Name = "Round 2",
                Matches = new List<Fixture>()
            {
                new Fixture(){ Home = teamList[1], Away = teamList[0]},
                new Fixture(){ Home = teamList[3], Away = teamList[2]}
            }
            },
            new Round()
            {
                Name = "Round 3",
                Matches = new List<Fixture>()
            {
                new Fixture(){ Home = teamList[3], Away = teamList[1]},
                new Fixture(){ Home = teamList[2], Away = teamList[0]}
            }
            }
        };

        return defaultRounds;
    }

}
