using GB.MatchSimulator.Models;
using GB.MatchSimulator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        catch (KeyNotFoundException ex)
        {
            return NotFound();
        }
    }

    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<TournamentResult>>> GetTournaments()
    {
        var result = await tournamentService.GetAllTournaments();
        return Ok(result);
    }
}
