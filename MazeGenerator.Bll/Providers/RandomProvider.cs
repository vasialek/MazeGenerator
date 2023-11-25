using MazeGenerator.Bll.Interfaces;

namespace MazeGenerator.Bll.Providers;

public class RandomProvider : IRandomProvider
{
    private static readonly Random Random = new Random(DateTime.Now.Millisecond);

    public int Next(int maxValue)
    {
        return Random.Next(maxValue);
    }
}
