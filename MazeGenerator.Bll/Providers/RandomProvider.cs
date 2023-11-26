using MazeGenerator.Bll.Interfaces;

namespace MazeGenerator.Bll.Providers;

public class RandomProvider : IRandomProvider
{
    private static readonly Random Random = new Random(DateTime.Now.Millisecond);

    public int Next(int maxValue)
    {
        return new Random(DateTime.Now.Millisecond).Next(maxValue);
        var next = Random.Next(maxValue);
        Console.WriteLine("random is {0}", next);
        return next;
        // return Random.Next(maxValue);
    }

    public bool BuildRightWall()
    {
        return Next(1) == 0;
    }

    public bool BuildBottomWall()
    {
        return Next(1) == 0;
    }
}
