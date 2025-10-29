using GB.MatchSimulator.DAL.Entities;

namespace GB.MatchSimulator.DAL.Repositories.Interfaces;

public interface ITournamentRepository
{
    Task<string> RegisterTournament(TournamentResultEntity tournamentResultEntity);
    Task<IEnumerable<TournamentResultEntity>> GetAllTournaments();
}