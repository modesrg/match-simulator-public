using GB.MatchSimulator.DAL.Entities;
using GB.MatchSimulator.DAL.Repositories.Interfaces;
using GB.MatchSimulator.Data;
using GB.MatchSimulator.Models;
using GB.MatchSimulator.Options;
using GB.MatchSimulator.Services;
using GB.MatchSimulator.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Moq;

namespace GB.MatchSimulator.Tests;

public class MatchServiceTests
{
    private Mock<ITeamRepository> mockTeamRepo = new();
    private Mock<IChanceService> mockchanceService = new();
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
        _service = new MatchService(mockchanceService.Object, mockTeamRepo.Object, mockOptions.Object, logger.Object);
    }

    [Fact]
    public async Task Success_Strongest_Wins()
    {
        mockTeamRepo.Setup(s => s.GetTeamByName(TestData.TeamA)).ReturnsAsync(TestData.GetTestTeams()[0]);
        mockTeamRepo.Setup(s => s.GetTeamByName(TestData.TeamD)).ReturnsAsync(TestData.GetTestTeams()[3]);

        var testFixture = new Fixture() { Home = TestData.TeamA, Away = TestData.TeamD };

        mockchanceService.Setup(s => s.PoissonScore(It.Is<double>(v => v < 1))).Returns(0);
        mockchanceService.Setup(s => s.PoissonScore(It.Is<double>(v => v > 1))).Returns(3);

        var result = await _service.SimulateMatch(testFixture);

        Assert.True(result.Home.Score > result.Away.Score);
    }

    [Fact]
    public async Task Fail_Team_Not_Found()
    {
        mockTeamRepo.Setup(s => s.GetTeamByName(It.IsAny<string>())).ReturnsAsync((TeamEntity)null);
        var testFixture = new Fixture() { Home = "Team_404_1", Away = "Team_404_2" };

        await Assert.ThrowsAnyAsync<KeyNotFoundException>(async () => await _service.SimulateMatch(testFixture));
    }
}