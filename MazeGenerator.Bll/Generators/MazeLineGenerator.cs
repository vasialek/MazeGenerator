using System.Security.Cryptography.X509Certificates;
using MazeGenerator.Bll.Interfaces;
using MazeGenerator.Bll.Models;

namespace MazeGenerator.Bll.Generators;

public class MazeLineGenerator : IMazeLineGenerator
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

    public MazeCell[] GenerateNextLine(MazeCell[] previousCells)
    {
        var cells = new MazeCell[previousCells.Length];

        for (var i = 0; i < cells.Length; i++)
        {
            cells[i] = previousCells[i];
            cells[i].RightWall = false;
            if (cells[i].BottomWall)
            {
                cells[i].Area = 0;
            }
        }

        var areaToSet = 1;
        for (var i = 0; i < cells.Length; i++)
        {
            if (cells[i].Area == 0)
            {
                cells[i].Area = areaToSet;
            }

            areaToSet = cells[i].Area + 1;
            cells[i].BottomWall = false;
        }
        
        return cells;
    }

    public MazeCell[] BuildRightWalls(MazeCell[] cells)
    {
        for (var i = 1; i < cells.Length; i++)
        {
            var buildWall = _randomProvider.BuildRightWall();
            // todo: Check & fix
            if (buildWall /*|| cells[i].Area == cells[i + 1].Area*/)
            {
                cells[i - 1].RightWall = true;
            }
            else
            {
                var areaToChange = cells[i].Area;
                var areaToSet = cells[i - 1].Area;
                for (var j = 0; j <= i; j++)
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

        for (var i = 0; i < cells.Length; i++)
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

            var buildWall = _randomProvider.BuildBottomWall();
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
