using MazeGenerator.Bll.Interfaces;
using NSubstitute;

namespace MazeGenerator.UnitTests;

public class MazeGeneratorTests
{
    private readonly Bll.MazeGenerator _generator = new Bll.MazeGenerator(MazeLineGenerator);
    private static readonly IMazeLineGenerator MazeLineGenerator = Substitute.For<IMazeLineGenerator>();

    [Fact]
    public void CanGenerate()
    {
        _generator.Generate(10, 10);
    }
}