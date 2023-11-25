using MazeGenerator.Bll.Interfaces;
using MazeGenerator.Bll.Models;

namespace MazeGenerator.Bll.Generators;

public class MazeLineGenerator
{
    private readonly IRandomProvider _randomProvider;

    public MazeLineGenerator(IRandomProvider randomProvider)
    {
        _randomProvider = randomProvider;
    }

    public MazeCell[] GenerateFirstLine(int numberOfCells)
    {
        var cells = Enumerable.Range(0, numberOfCells)
            .Select(c => new MazeCell { Area = c + 1, TopWall = true })
            .ToArray();

        cells[0].LeftWall = true;
        cells[^1].RightWall = true;

        return cells;
    }

    public MazeCell[] BuildRightWalls(MazeCell[] cells)
    {
        for (var i = 1; i < cells.Length; i++)
        {
            var buildWall = _randomProvider.Next(3) == 2;
            // todo: Check & fix
            if (buildWall /*|| cells[i].Area == cells[i + 1].Area*/)
            {
                cells[i - 1].RightWall = true;
            }
            else
            {
                var areaToChange = cells[i].Area;
                var areaToSet = cells[i - 1].Area;
                for (int j = 0; j <= i; j++)
                {
                    if (areaToChange == cells[j].Area)
                    {
                        cells[j].Area = areaToSet;
                    }
                }
            }
        }

        cells[0].LeftWall = true;
        cells[^1].RightWall = true;
        
        return cells;
    }

    public MazeCell[] BuildBottomWalls(MazeCell[] cells)
    {
        var index = 0;
        var previousArea = -1;
        var wasExitToBottom = false;

        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].Area != previousArea)
            {
                if (HasPreviousAreaBottomExit(i, wasExitToBottom))
                {
                    cells[i - 1].BottomWall = false;
                }
                
                previousArea = cells[i].Area;
                wasExitToBottom = false;
            }
            
            var buildWall = _randomProvider.Next(3) == 2;
            cells[i].BottomWall = buildWall;
            if (!buildWall)
            {
                wasExitToBottom = true;
            }
        }
        return cells;
    }

    private static bool HasPreviousAreaBottomExit(int i, bool wasExitToBottom)
    {
        return i > 0 && !wasExitToBottom;
    }
}
