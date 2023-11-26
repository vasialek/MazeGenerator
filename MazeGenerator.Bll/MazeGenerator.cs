using MazeGenerator.Bll.Interfaces;
using MazeGenerator.Bll.Models;

namespace MazeGenerator.Bll;

public class MazeGenerator
{
    private readonly IMazeLineGenerator _mazeLineGenerator;

    public MazeGenerator(IMazeLineGenerator mazeLineGenerator)
    {
        _mazeLineGenerator = mazeLineGenerator;
    }

    public MazeCell[][] Generate(int height, int width)
    {
        var list = new List<MazeCell[]>(height);

        var line = _mazeLineGenerator.GenerateFirstLine(width);
        line = _mazeLineGenerator.BuildRightWalls(line);
        line = _mazeLineGenerator.BuildBottomWalls(line);
        list.Add(line);

        var previousCells = line;
        for (int i = 1; i < height; i++)
        {
            var nextLine = _mazeLineGenerator.GenerateNextLine(previousCells);
            nextLine = _mazeLineGenerator.BuildRightWalls(nextLine);
            nextLine = _mazeLineGenerator.BuildBottomWalls(nextLine);
        
            list.Add(nextLine);
            previousCells = nextLine;
        }
        
        return list.ToArray();
    }
}