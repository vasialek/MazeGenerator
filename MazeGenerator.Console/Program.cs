// See https://aka.ms/new-console-template for more information

using MazeGenerator.Bll.Generators;
using MazeGenerator.Bll.Models;
using MazeGenerator.Bll.Providers;

internal class Program
{
    private const int Height = 15;
    private const int Width = 15;

    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var randomProvider = new RandomProvider();
        var mazeLineGenerator = new MazeLineGenerator(randomProvider);
        var cells = new MazeGenerator.Bll.MazeGenerator(mazeLineGenerator).Generate(Height, Width);

        // for (int x = 0; x < Width; x++)
        // {
        //     Console.Write("##");
        // }
        // Console.WriteLine();

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                DisplayCell(cells[y][x], x);
            }
            Console.WriteLine();
            
            for (int x = 0; x < Width; x++)
            {
                DisplayBottomCell(cells[y][x], x);
            }
            Console.WriteLine();
        }
    }

    private static void DisplayCell(MazeCell cell, int x)
    {
        // if (x == 0)
        // {
        //     Console.Write("#");
        // }
        // else
        // {
        //     Console.Write(cell.LeftWall ? '#' : ' ');
        // }
        
        // Console.Write(' ');
        
        // if (x == Width - 1)
        // {
        //     Console.Write(" #");
        //     return;
        // }
        Console.Write(cell.RightWall ? " #" : "  ");
    }
    private static void DisplayBottomCell(MazeCell cell, int x)
    {
        // Console.Write(x == 0 ? '#' : ' ');
        Console.Write(cell.BottomWall ? "##" : " #");
    }
}