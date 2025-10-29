namespace GB.MatchSimulator.Models;

public record TeamInfo
{
    public required string Name { get; set; }
    public double Offense { get; set; } = 0;
    public double Defense { get; set; } = 0;
    public List<MatchPlayer> Roster { get; set; } = new();
}
