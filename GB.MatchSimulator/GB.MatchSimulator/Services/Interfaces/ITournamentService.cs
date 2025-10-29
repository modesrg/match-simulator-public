using GB.MatchSimulator.Models;

namespace GB.MatchSimulator.Services.Interfaces;

public interface ITournamentService
{
    Task<IEnumerable<TournamentResult>> GetAllTournaments();
    Task<TournamentResult> SimulateTournament();
}