using GB.MatchSimulator.DAL.Entities;
using GB.MatchSimulator.DAL.Local;
using GB.MatchSimulator.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GB.MatchSimulator.DAL.Repositories;

public class TeamRepository(SimulatorDbContext dbContext) : ITeamRepository
{
    public async Task<List<TeamEntity>> GetAllTeams()
    {
        return await dbContext.Teams.ToListAsync() ?? DummyTeams.GetDummyTeams();
    }

    public async Task<TeamEntity?> GetTeamByName(string teamName)
    {
        var team = await dbContext.Teams.FirstOrDefaultAsync(t => t.Name.Equals(teamName))
            ?? DummyTeams.GetDummyTeams().FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase));

        return team ?? null;
    }
}
