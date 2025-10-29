using GB.MatchSimulator.Models;

namespace GB.MatchSimulator.Clients.Interfaces;
public interface ITournamentApiClient
{
    Task<TournamentResult?> SimulateTournament();
}