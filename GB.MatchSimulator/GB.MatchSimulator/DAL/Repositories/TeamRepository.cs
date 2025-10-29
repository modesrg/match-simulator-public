using GB.MatchSimulator.DAL.Entities;
using GB.MatchSimulator.DAL.Local;
using GB.MatchSimulator.DAL.Repositories.Interfaces;

namespace GB.MatchSimulator.DAL.Repositories;

public class TeamRepository(SimulatorDbContext dbContext) : ITeamRepository
{
    public List<TeamEntity> GetAllTeams()
    {
        return dbContext.Teams.ToList() ?? DummyTeams.GetDummyTeams();
    }

    public TeamEntity? GetTeamByName(string teamName)
    {
        var team = dbContext.Teams.FirstOrDefault(t => t.Name.Equals(teamName))
            ?? DummyTeams.GetDummyTeams().FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase));

        return team ?? null;
    }
}
