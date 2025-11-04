using GB.MatchSimulator.Data;
using GB.MatchSimulator.Models;
using GB.MatchSimulator.Options;
using GB.MatchSimulator.Services;
using GB.MatchSimulator.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace GB.MatchSimulator.Tests;

public class RoundServiceTests
{
    private Mock<IMatchService> mockMatchService = new();
    private Mock<IOptions<SimulatorOptions>> mockOptions = new();
    private Mock<ILogger<RoundService>> logger = new();

    private RoundService _service;

    public RoundServiceTests()
    {
        mockOptions.Setup(o => o.Value).Returns(new SimulatorOptions
        {
            SecondStage = false,
            RandomFixtures = false
        });

        _service = new RoundService(mockMatchService.Object, mockOptions.Object, logger.Object);
    }

    [Fact]
    public async Task Success_Round_Simulated()
    {
        var matchResults = TestData.GetTestMatchResults();
        List<Fixture> testFixtures = TestData.GetTestFixtures();
        var roundName = "Success_Round";

        var testRound = new Round() { Name = roundName, Matches = testFixtures };

        mockMatchService.Setup(s => s.SimulateMatch(testRound.Matches.FirstOrDefault()))
            .ReturnsAsync(matchResults.FirstOrDefault()!);

        var result = await _service.SimulateRound(testRound);

        Assert.True(result.Results.FirstOrDefault()!.Home.Score == 3);
        Assert.Equal(result.Name, roundName);
    }

    [Fact]
    public void Success_Rounds_Generated()
    {
        var dummyTeams = TestData.GetTestTeams().Select(t => t.Name).ToList();
        var result = _service.GenerateRoundRobin(dummyTeams);

        Assert.True(result.Count == 3);
    }
}