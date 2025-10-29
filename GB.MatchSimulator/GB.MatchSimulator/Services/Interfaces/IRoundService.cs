using GB.MatchSimulator.Models;

namespace GB.MatchSimulator.Services.Interfaces;
public interface IRoundService
{
    List<Round> GenerateRoundRobin(List<string> teamList);
    RoundResult SimulateRound(Round round);
}