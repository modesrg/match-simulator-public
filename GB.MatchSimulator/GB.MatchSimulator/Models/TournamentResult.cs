namespace GB.MatchSimulator.Models;

public record TournamentResult
{
    public string? Id { get; set; }
    public List<RoundResult> Rounds { get; set; } = new();
    public List<BoardResult> ScoreBoard { get; set; } = new();
}

