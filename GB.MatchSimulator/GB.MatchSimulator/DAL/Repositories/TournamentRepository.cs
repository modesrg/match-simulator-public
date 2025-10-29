using GB.MatchSimulator.DAL.Entities;
using GB.MatchSimulator.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GB.MatchSimulator.DAL.Repositories;

public class TournamentRepository(SimulatorDbContext dbContext) : ITournamentRepository
{
    public async Task<string> RegisterTournament(TournamentResultEntity tournamentResultEntity)
    {
        await dbContext.Tournaments.AddAsync(tournamentResultEntity);
        await dbContext.SaveChangesAsync();
        return tournamentResultEntity.Id;
    }
    public async Task<IEnumerable<TournamentResultEntity>> GetAllTournaments()
    {
        return await dbContext.Tournaments.ToListAsync();
    }
}
