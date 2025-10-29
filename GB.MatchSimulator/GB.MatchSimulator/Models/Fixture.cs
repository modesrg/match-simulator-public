namespace GB.MatchSimulator.Models;

public record Fixture
{
    public required string Home { get; set; }
    public required string Away { get; set; }
}
