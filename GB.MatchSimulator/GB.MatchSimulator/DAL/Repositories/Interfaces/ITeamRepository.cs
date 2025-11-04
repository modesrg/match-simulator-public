using GB.MatchSimulator.DAL.Entities;

namespace GB.MatchSimulator.DAL.Repositories.Interfaces;

public interface ITeamRepository
{
    Task<List<TeamEntity>> GetAllTeams();
    Task<TeamEntity?> GetTeamByName(string teamName);
}