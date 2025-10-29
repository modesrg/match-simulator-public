using Microsoft.EntityFrameworkCore;

namespace GB.MatchSimulator.DAL.Entities;

public class TournamentResultEntity
{
    public required string Id { get; set; }
    public ICollection<RoundResultEntity> Rounds { get; set; } = new HashSet<RoundResultEntity>();
    public ICollection<BoardResultEntity> ScoreBoard { get; set; } = new HashSet<BoardResultEntity>();
}

[Owned]
public class RoundResultEntity
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public ICollection<MatchResultEntity> Results { get; set; } = new HashSet<MatchResultEntity>();
}

public class MatchResultEntity
{
    public required TeamResultEntity Home { get; set; }
    public required TeamResultEntity Away { get; set; }
}

public class TeamResultEntity
{
    public required string Name { get; set; }
    public int Score { get; set; }
}

[Owned]
public class BoardResultEntity
{
    public required string Id { get; set; }
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

