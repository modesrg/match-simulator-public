using GB.MatchSimulator.Models;

namespace GB.MatchSimulator.Helpers;

public class HeadToHeadCalculator
{
    public static int Calculate(string teamA, string teamB, List<MatchResult> matchResults)
    {
        var teamsMatches = matchResults
            .Where(m =>
                (m.Home.Name == teamA && m.Away.Name == teamB) ||
                (m.Home.Name == teamB && m.Away.Name == teamA));

        var points = teamsMatches
            .Sum(m =>
            {
                return m.Home.Score == m.Away.Score
                ? 1
                : (m.Home.Name == teamA && m.Home.Score > m.Away.Score)
                || (m.Away.Name == teamA && m.Away.Score > m.Home.Score)
                ? 3
                : 0;
            });
        return points;
    }
}