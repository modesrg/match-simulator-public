using GB.MatchSimulator.DAL.Entities;
using GB.MatchSimulator.DAL.Repositories.Interfaces;
using GB.MatchSimulator.Data;
using GB.MatchSimulator.Models;
using GB.MatchSimulator.Options;
using GB.MatchSimulator.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace GB.MatchSimulator.Tests;

public class MatchServiceTests
{
    private Mock<ITeamRepository> mockTeamRepo = new();
    private MatchService _service;
    private Mock<IOptions<SimulatorOptions>> mockOptions = new();
    private Mock<ILogger<MatchService>> logger = new();

    public MatchServiceTests()
    {
        mockOptions.Setup(o => o.Value).Returns(new SimulatorOptions
        {
            SecondStage = false,
            RandomFixtures = false
        });
        _service = new MatchService(mockTeamRepo.Object, mockOptions.Object, logger.Object);
    }

    [Fact]
    public void Success_Strongest_Wins()
    {
        mockTeamRepo.Setup(s => s.GetTeamByName(TestData.TeamA)).Returns(TestData.GetTestTeams()[0]);
        mockTeamRepo.Setup(s => s.GetTeamByName(TestData.TeamD)).Returns(TestData.GetTestTeams()[3]);
        var testFixture = new Fixture() { Home = TestData.TeamA, Away = TestData.TeamD };

        var result = _service.SimulateMatch(testFixture);

        Assert.True(result.Home.Score > result.Away.Score);
    }

    [Fact]
    public void Fail_Team_Not_Found()
    {
        mockTeamRepo.Setup(s => s.GetTeamByName(It.IsAny<string>())).Returns<TeamEntity>(null);
        var testFixture = new Fixture() { Home = "Team_404_1", Away = "Team_404_2" };

        Assert.Throws<KeyNotFoundException>(() => _service.SimulateMatch(testFixture));
    }
}