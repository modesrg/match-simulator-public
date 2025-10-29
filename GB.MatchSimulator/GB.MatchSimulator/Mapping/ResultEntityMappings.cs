namespace GB.MatchSimulator.Mapping;

using GB.MatchSimulator.DAL.Entities;
using GB.MatchSimulator.Models;
using System.Linq;

public static class ResultEntityMappings
{
    public static TournamentResult ToModel(this TournamentResultEntity source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return new TournamentResult
        {
            Id = source.Id,
            Rounds = source.Rounds?
                .Select(r => r.ToModel())
                .ToList() ?? new List<RoundResult>(),

            ScoreBoard = source.ScoreBoard?
                .Select(b => b.ToModel())
                .ToList() ?? new List<BoardResult>()
        };
    }

    public static RoundResult ToModel(this RoundResultEntity source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return new RoundResult
        {
            Name = source.Name,
            Results = source.Results?
                .Select(m => m.ToModel())
                .ToList() ?? new List<MatchResult>()
        };
    }

    public static MatchResult ToModel(this MatchResultEntity source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return new MatchResult
        {
            Home = source.Home.ToModel(),
            Away = source.Away.ToModel()
        };
    }

    public static TeamResult ToModel(this TeamResultEntity source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return new TeamResult
        {
            Name = source.Name,
            Score = source.Score
        };
    }

    public static BoardResult ToModel(this BoardResultEntity source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return new BoardResult
        {
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
