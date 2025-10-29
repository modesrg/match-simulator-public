namespace GB.MatchSimulator.Extensions;
public static class ListExtensions
{
    private static readonly Random _rnd = new();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = _rnd.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

