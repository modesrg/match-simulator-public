using GB.MatchSimulator.DAL.Repositories.Interfaces;
using GB.MatchSimulator.Data;
using GB.MatchSimulator.Helpers;
using GB.MatchSimulator.Models;
using GB.MatchSimulator.Services;
using GB.MatchSimulator.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace GB.MatchSimulator.Tests;

public class TournamentServiceTests
{
    private Mock<IRoundService> mockRoundService = new();
    private Mock<ITeamRepository> mockTeamRepo = new();
    private Mock<ITournamentRepository> mockTournamentRepo = new();
    private Mock<ILogger<TournamentService>> logger = new();


    private TournamentService _service;

    public TournamentServiceTests()
    {
        SetupMockServices();
        _service = new TournamentService(mockRoundService.Object, mockTeamRepo.Object, mockTournamentRepo.Object, logger.Object);
    }

    [Fact]
    public async Task Success_Stronger_Wins()
    {
        var result = await _service.SimulateTournament();
        Assert.Equal(result.ScoreBoard.FirstOrDefault()!.Name, TestData.TeamA);
    }

    [Fact]
    public async Task Success_Winner_Higher_Than_Loser()
    {
        var result = await _service.SimulateTournament();
        Assert.True(result.ScoreBoard.FirstOrDefault()!.Points > result.ScoreBoard.Last()!.Points);
    }

    [Fact]
    public async Task Sum_Zero_Score()
    {
        var result = await _service.SimulateTournament();
        var totalFor = result.ScoreBoard.Select(sb => sb.For).Sum();
        var fotalAgainst = result.ScoreBoard.Select(sb => sb.For).Sum();

        Assert.True(totalFor.Equals(fotalAgainst));
    }

    [Fact]
    public async Task Sum_Zero_Results()
    {
        var result = await _service.SimulateTournament();
        var totalWins = result.ScoreBoard.Select(sb => sb.Win).Sum();
        var totalLoses = result.ScoreBoard.Select(sb => sb.Loss).Sum();

        Assert.True(totalWins.Equals(totalLoses));
    }

    [Fact]
    public async Task Sum_Zero_GD()
    {
        var result = await _service.SimulateTournament();
        var totalGoals = result.ScoreBoard.Select(sb => sb.GD).Sum();

        Assert.True(totalGoals.Equals(0));
    }

    [Fact]
    public void Head_To_Head_Tiebreaker()
    {
        var matches = new List<MatchResult>
        {
            new MatchResult {
                Home = new TeamResult(){ Name = TestData.TeamA, Score = 3 },
                Away = new TeamResult(){ Name = TestData.TeamB, Score = 1 }  },
            new MatchResult {
                Home = new TeamResult(){ Name = TestData.TeamB, Score = 1 },
                Away = new TeamResult(){ Name = TestData.TeamA, Score = 1 }  },
             };

        int pointsTeamA = HeadToHeadCalculator.Calculate(TestData.TeamA, TestData.TeamB, matches);
        int pointsTeamB = HeadToHeadCalculator.Calculate(TestData.TeamB, TestData.TeamA, matches);

        Assert.Equal(4, pointsTeamA);
        Assert.Equal(1, pointsTeamB);
    }

    private void SetupMockServices()
    {
        var testRounds = TestData.GetTestRounds();
        var testRound1 = testRounds[0];
        var testRound2 = testRounds[1];
        var testRound3 = testRounds[2];

        var testRoundResults = TestData.GetTestRoundResults();
        var testRoundResult1 = testRoundResults[0];
        var testRoundResult2 = testRoundResults[1];
        var testRoundResult3 = testRoundResults[2];

        mockTeamRepo.Setup(s => s.GetAllTeams()).Returns(TestData.GetTestTeams());
        mockRoundService.Setup(s => s.GenerateRoundRobin(It.IsAny<List<string>>()))
            .Returns(testRounds);

        mockRoundService.Setup(s => s.SimulateRound(testRound1))
           .Returns(testRoundResult1);
        mockRoundService.Setup(s => s.SimulateRound(testRound2))
              .Returns(testRoundResult2);
        mockRoundService.Setup(s => s.SimulateRound(testRound3))
              .Returns(testRoundResult2);
    }

}