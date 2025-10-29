namespace GB.MatchSimulator.Models;

public record MatchPlayer
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public int Offense { get; set; }
    public int Defense { get; set; }
}

