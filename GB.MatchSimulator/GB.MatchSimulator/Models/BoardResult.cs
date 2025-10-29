namespace GB.MatchSimulator.Models;

public record BoardResult
{
    public required string Name { get; set; }
    public int Played { get; set; } = 0;
    public int Points { get; set; } = 0;
    public int Win { get; set; } = 0;
    public int Draw { get; set; } = 0;
    public int Loss { get; set; } = 0;
    public int For { get; set; } = 0;
    public int Against { get; set; } = 0;
    public int GD { get; set; } = 0;
    public int HeadToHead { get; set; } = 0;
}

