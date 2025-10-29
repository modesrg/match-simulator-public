namespace GB.MatchSimulator.Mapping;

using GB.MatchSimulator.DAL.Entities;
using GB.MatchSimulator.Models;
using System.Linq;

public static class TeamMappings
{
    public static TeamInfo ToMatch(this TeamEntity team)
    {
        if (team == null)
            throw new ArgumentNullException(nameof(team));

        var roster = team.Players
            .Select(p => new MatchPlayer
            {
                Id = p.Id,
                Name = p.Name,
                Offense = p.Offense,
                Defense = p.Defense
            })
            .ToList();

        var avgOffense = roster.Any() ? roster.Average(p => p.Offense) : 0;
        var avgDefense = roster.Any() ? roster.Average(p => p.Defense) : 0;

        return new TeamInfo
        {
            Name = team.Name,
            Offense = avgOffense,
            Defense = avgDefense,
            Roster = roster
        };
    }
}

