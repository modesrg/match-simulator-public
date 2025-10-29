using GB.MatchSimulator.DAL.Entities;

namespace GB.MatchSimulator.DAL.Repositories.Interfaces;

public interface ITeamRepository
{
    List<TeamEntity> GetAllTeams();
    TeamEntity? GetTeamByName(string teamName);
}