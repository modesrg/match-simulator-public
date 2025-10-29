using GB.MatchSimulator.Models;
using GB.MatchSimulator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GB.MatchSimulator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TournamentController(ITournamentService tournamentService) : ControllerBase
{

    [HttpGet("simulate")]
    public async Task<ActionResult<TournamentResult>> SimulateTournament()
    {
        try
        {
            var result = await tournamentService.SimulateTournament();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return ex is KeyNotFoundException
                ? NotFound()
                : StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<TournamentResult>>> GetTournaments()
    {
        try
        {
            var result = await tournamentService.GetAllTournaments();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
