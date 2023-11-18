namespace MazeGenerator.UnitTests;

public class MazeGeneratorTests
{
    private readonly Bll.MazeGenerator _generator = new Bll.MazeGenerator();

    [Fact]
    public void CanGenerate()
    {
        _generator.Generate(10, 10);
    }
}