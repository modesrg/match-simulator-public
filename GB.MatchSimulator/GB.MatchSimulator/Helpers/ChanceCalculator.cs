namespace GB.MatchSimulator.Helpers;

public static class ChanceCalculator
{
    private static readonly Random _rnd = new();

    public static int PoissonScore(double lambda)
    {
        if (lambda <= 0) return 0;

        double L = Math.Exp(-lambda);
        double p = 1.0;
        int k = 0;

        do
        {
            k++;
            p *= _rnd.NextDouble();
        } while (p > L);

        return k - 1;
    }

}
