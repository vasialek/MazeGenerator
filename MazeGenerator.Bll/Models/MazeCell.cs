using System.Text;

namespace MazeGenerator.Bll.Models;

public class MazeCell
{
    public int Area { get; set; }

    public bool LeftWall { get; set; }
    
    public bool RightWall { get; set; }
    
    public bool TopWall { get; set; }
    
    public bool BottomWall { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder("[");
        sb.Append(LeftWall ? "|" : "")
            .Append(TopWall ? "^" : "")
            .Append(Area)
            .Append(BottomWall ? "v" : "")
            .Append(RightWall ? "|" : "")
            .Append(']');
        return sb.ToString();
    }
}
