namespace GB.MatchSimulator.Models;

public record Round
{
    public required string Name { get; set; }
    public required List<Fixture> Matches { get; set; }
}
