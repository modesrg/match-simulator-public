namespace GB.MatchSimulator.Constants;

public class MatchConstants
{
    public struct MatchOutcome
    {
        public const string HomeWins = "home_wins";
        public const string AwayWins = "away_wins";
        public const string Draw = "draw";
    }

    public static string GetMatchOutcome(int homeScore, int awayScore)
    {
        return homeScore == awayScore
            ? MatchOutcome.Draw
            : homeScore > awayScore
            ? MatchOutcome.HomeWins
            : MatchOutcome.AwayWins;
    }
}
