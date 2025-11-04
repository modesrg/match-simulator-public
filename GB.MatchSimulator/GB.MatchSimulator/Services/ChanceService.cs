using GB.MatchSimulator.Services.Interfaces;

namespace GB.MatchSimulator.Services;

public class ChanceService : IChanceService
{
    public int PoissonScore(double lambda)
    {
        if (lambda <= 0) return 0;

        double L = Math.Exp(-lambda);
        double p = 1.0;
        int k = 0;

        do
        {
            k++;
            p *= Random.Shared.NextDouble();
        } while (p > L);

        return k - 1;
    }
}
