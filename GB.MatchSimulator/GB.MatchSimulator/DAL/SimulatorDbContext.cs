using GB.MatchSimulator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GB.MatchSimulator.DAL;

public class SimulatorDbContext : DbContext
{
    public SimulatorDbContext(DbContextOptions<SimulatorDbContext> options) : base(options) { }

    public DbSet<TournamentResultEntity> Tournaments { get; set; }
    public DbSet<TeamEntity> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TournamentResultEntity>(builder =>
        {
            builder.HasKey(t => t.Id);

            builder.OwnsMany(t => t.ScoreBoard, sb =>
            {
                sb.HasKey(t => t.Id);
                sb.Property(s => s.Name);
                sb.Property(s => s.Played);
                sb.Property(s => s.Points);
                sb.Property(s => s.Win);
                sb.Property(s => s.Draw);
                sb.Property(s => s.Loss);
                sb.Property(s => s.For);
                sb.Property(s => s.Against);
                sb.Property(s => s.GD);
                sb.Property(s => s.HeadToHead);
            });

            builder.OwnsMany(t => t.Rounds, round =>
            {
                round.HasKey(r => r.Id);
                round.Property(r => r.Name);

                round.OwnsMany(r => r.Results, match =>
                {
                    match.OwnsOne(m => m.Home, h =>
                    {
                        h.Property(t => t.Name);
                        h.Property(t => t.Score);
                    });

                    match.OwnsOne(m => m.Away, a =>
                    {
                        a.Property(t => t.Name);
                        a.Property(t => t.Score);
                    });
                });
            });
        });

        modelBuilder.Entity<TeamEntity>(builder =>
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired();

            builder.OwnsMany(t => t.Players, player =>
            {
                player.HasKey(p => p.Id);
                player.Property(p => p.Name);
                player.Property(p => p.Offense);
                player.Property(p => p.Defense);
            });
        });
    }
}
