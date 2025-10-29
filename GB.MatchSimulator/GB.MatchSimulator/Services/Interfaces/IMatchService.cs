using GB.MatchSimulator.Models;

namespace GB.MatchSimulator.Services.Interfaces;
public interface IMatchService
{
    MatchResult SimulateMatch(Fixture match);
}