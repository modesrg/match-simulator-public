using GB.MatchSimulator.Models;

namespace GB.MatchSimulator.Services.Interfaces;
public interface IMatchService
{
    Task<MatchResult> SimulateMatch(Fixture match);
}