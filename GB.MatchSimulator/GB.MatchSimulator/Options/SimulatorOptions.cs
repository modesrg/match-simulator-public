namespace GB.MatchSimulator.Options;

public class SimulatorOptions
{
    public const string Simulator = "Simulator";
    public bool SecondStage { get; set; } = false;
    public bool RandomFixtures { get; set; } = false;
    public double AverageScorePerMatch { get; set; } = 2.8;
}
