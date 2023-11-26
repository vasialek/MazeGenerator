using MazeGenerator.Bll.Models;

namespace MazeGenerator.Bll.Interfaces;

public interface IMazeLineGenerator
{
    MazeCell[] GenerateFirstLine(int numberOfCells);
    MazeCell[] GenerateNextLine(MazeCell[] previousCells);
    MazeCell[] BuildRightWalls(MazeCell[] cells);
    MazeCell[] BuildBottomWalls(MazeCell[] cells);
}
