namespace GB.MatchSimulator.DAL.Local;

public static class TeamSeeder
{
    public static void SeedIfEmpty(SimulatorDbContext dbContext)
    {
        if (!dbContext.Teams.Any())
        {
            dbContext.Teams.AddRange(DummyTeams.GetDummyTeams());
            dbContext.SaveChanges();
        }
    }
}
