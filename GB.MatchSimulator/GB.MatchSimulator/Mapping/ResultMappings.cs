namespace GB.MatchSimulator.Mapping;

using GB.MatchSimulator.DAL.Entities;
using GB.MatchSimulator.Helpers;
using GB.MatchSimulator.Models;
using System.Linq;

public static class ResultMappings
{
    public static TournamentResultEntity ToNewEntity(this TournamentResult source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return new TournamentResultEntity
        {
            Id = IdGenerator.Generate("to"),
            Rounds = source.Rounds?
                .Select(r => r.ToNewEntity())
                .ToHashSet() ?? new HashSet<RoundResultEntity>(),

            ScoreBoard = source.ScoreBoard?
                .Select(b => b.ToNewEntity())
                .ToHashSet() ?? new HashSet<BoardResultEntity>()
        };
    }

    public static RoundResultEntity ToNewEntity(this RoundResult source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return new RoundResultEntity
        {
            Id = IdGenerator.Generate("ro"),
            Name = source.Name,
            Results = source.Results?
                .Select(m => m.ToEntity())
                .ToHashSet() ?? new HashSet<MatchResultEntity>()
        };
    }

    public static MatchResultEntity ToEntity(this MatchResult source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return new MatchResultEntity
        {
            Home = source.Home.ToEntity(),
            Away = source.Away.ToEntity()
        };
    }

    public static TeamResultEntity ToEntity(this TeamResult source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return new TeamResultEntity
        {
            Name = source.Name,
            Score = source.Score
        };
    }

    public static BoardResultEntity ToNewEntity(this BoardResult source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return new BoardResultEntity
        {
            Id = IdGenerator.Generate("bo"),
            Name = source.Name,
            Played = source.Played,
            Points = source.Points,
            Win = source.Win,
            Draw = source.Draw,
            Loss = source.Loss,
            For = source.For,
            Against = source.Against,
            GD = source.GD,
            HeadToHead = source.HeadToHead
        };
    }
}

