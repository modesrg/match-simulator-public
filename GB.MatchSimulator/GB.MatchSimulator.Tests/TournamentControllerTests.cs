using GB.MatchSimulator.Controllers;
using GB.MatchSimulator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GB.MatchSimulator.Tests;

public class TournamentControllerTests
{
    private Mock<ITournamentService> mockTournamentService = new();
    private TournamentController _controller;

    public TournamentControllerTests()
    {
        _controller = new TournamentController(mockTournamentService.Object);
    }

    [Fact]
    public async Task Success_200()
    {
        mockTournamentService.Setup(s => s.SimulateTournament())
            .ReturnsAsync(new Models.TournamentResult());

        var result = await _controller.SimulateTournament();
        var objectResult = Assert.IsType<OkObjectResult>(result.Result);

        Assert.Equal(200, objectResult.StatusCode);
    }

    [Fact]
    public async Task Failure_404()
    {
        mockTournamentService.Setup(s => s.SimulateTournament())
            .Throws(new KeyNotFoundException());

        var result = await _controller.SimulateTournament();
        var codeResult = Assert.IsType<NotFoundResult>(result.Result);

        Assert.Equal(404, codeResult.StatusCode);
    }

    [Fact]
    public async Task Failure_500()
    {
        mockTournamentService.Setup(s => s.SimulateTournament())
            .Throws(new Exception());

        await Assert.ThrowsAnyAsync<Exception>(async () => await _controller.SimulateTournament());
    }
}