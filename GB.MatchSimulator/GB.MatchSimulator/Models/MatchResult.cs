namespace GB.MatchSimulator.Models;

public record MatchResult
{
    public required TeamResult Home { get; set; }
    public required TeamResult Away { get; set; }
}

public record TeamResult
{
    public required string Name { get; set; }
    public required int Score { get; set; }
}
