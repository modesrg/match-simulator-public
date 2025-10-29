namespace GB.MatchSimulator.Models;

public record RoundResult
{
    public required string Name { get; set; }
    public required List<MatchResult> Results { get; set; }
}
