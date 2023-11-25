using FluentAssertions;
using MazeGenerator.Bll.Generators;
using MazeGenerator.Bll.Interfaces;
using MazeGenerator.Bll.Models;
using MazeGenerator.Bll.Providers;
using NSubstitute;
using Xunit.Abstractions;

namespace MazeGenerator.UnitTests.Generators;

public class MazeLineGeneratorTests
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly MazeLineGenerator _generator;
    private readonly IRandomProvider _randomProvider = Substitute.For<IRandomProvider>();

    public MazeLineGeneratorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _generator = new MazeLineGenerator(_randomProvider);
    }

    [Fact]
    public void CanGenerateFirstLine()
    {
        var actual = _generator.GenerateFirstLine(4);

        actual.Should().BeEquivalentTo(new MazeCell[]
        {
            new() { Area = 1, TopWall = true, LeftWall = true },
            new() { Area = 2, TopWall = true },
            new() { Area = 3, TopWall = true },
            new() { Area = 4, TopWall = true, RightWall = true }
        });
    }

    [Fact]
    public void CanBuildRightWalls()
    {
        var cells = new[]
        {
            new MazeCell { Area = 1 },
            new MazeCell { Area = 2 },
            new MazeCell { Area = 3 },
            new MazeCell { Area = 4 },
            new MazeCell { Area = 5 },
            new MazeCell { Area = 6 },
            new MazeCell { Area = 7 },
            new MazeCell { Area = 8 }
        };
        _randomProvider.Next(3).Returns(0, 1, 2, 0, 2, 0, 1, 0);

        var actual = _generator.BuildRightWalls(cells);

        _outputHelper.WriteLine(string.Join(" ", actual.Select(c => c.ToString())));
        actual.Should().BeEquivalentTo(new MazeCell[]
        {
            new() { Area = 1, LeftWall = true },
            new() { Area = 1 },
            new() { Area = 1,  RightWall = true },
            new() { Area = 4 },
            new() { Area = 4, RightWall = true },
            new() { Area = 6 },
            new() { Area = 6 },
            new() { Area = 6, RightWall = true }
        });
    }

    [Fact]
    public void CanBuildBottomWalls()
    {
        var cells = new[]
        {
            new MazeCell { Area = 1 },
            new MazeCell { Area = 1 },
            new MazeCell { Area = 1 },
            new MazeCell { Area = 4 },
            new MazeCell { Area = 4 },
            new MazeCell { Area = 6 },
            new MazeCell { Area = 6 },
            new MazeCell { Area = 6 }
        };
        _randomProvider.Next(3).Returns(1, 2, 2, 2, 0, 2, 1, 0);

        var actual = _generator.BuildBottomWalls(cells);
        
        actual.Should().BeEquivalentTo(new MazeCell[]
        {
            new() { Area = 1 },
            new() { Area = 1, BottomWall = true },
            new() { Area = 1, BottomWall= true },
            new() { Area = 4 },
            new() { Area = 4, BottomWall = true },
            new() { Area = 6 },
            new() { Area = 6 },
            new() { Area = 6, BottomWall = true, }
        });
    }
}
