using GB.MatchSimulator.Services.Interfaces;

namespace GB.MatchSimulator.Services;

public class ChanceService : IChanceService
{
    public int PoissonScore(double lambda)
    {
        var random = Random.Shared;

        if (lambda <= 0) return 0;

        double L = Math.Exp(-lambda);
        double p = 1.0;
        int k = 0;

        do
        {
            k++;
            p *= random.NextDouble();
        } while (p > L);

        return k - 1;
    }

}
