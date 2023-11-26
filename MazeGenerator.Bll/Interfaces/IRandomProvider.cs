namespace MazeGenerator.Bll.Interfaces;

public interface IRandomProvider
{
    int Next(int maxValue);

    bool BuildRightWall();
    
    bool BuildBottomWall();
}
