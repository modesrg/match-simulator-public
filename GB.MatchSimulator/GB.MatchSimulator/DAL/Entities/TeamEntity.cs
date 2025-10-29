using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GB.MatchSimulator.DAL.Entities;

public class TeamEntity
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public ICollection<PlayerEntity> Players { get; set; } = new HashSet<PlayerEntity>();
}

[Owned]
public class PlayerEntity
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    [Range(1, 10)]
    public int Offense { get; set; }
    [Range(1, 10)]
    public int Defense { get; set; }
}