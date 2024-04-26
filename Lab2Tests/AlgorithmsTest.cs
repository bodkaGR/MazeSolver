using Lab2;
using NUnit.Framework;
namespace Lab2Tests;

public class AlgorithmsTest
{
    private BfsAlgorithm bfsSolver;
    private RbfsAlgorithm rbfsSolver;
    private readonly int _width = 10;
    private readonly int _height = 5;
    private int[,] maze;
    
    [SetUp]
    public void Setup()
    {
        maze = new int[5, 10]
        {
            {0, 1, 0, 0, 0, 1, 0, 0, 0, 1},
            {0, 1, 0, 1, 0, 1, 1, 1, 0, 1},
            {0, 0, 0, 1, 0, 0, 0, 1, 0, 1},
            {1, 1, 1, 1, 1, 1, 0, 1, 0, 1},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 1}

        };
        bfsSolver = new BfsAlgorithm(maze, _width, _height, new Tuple<int, int>(0, 0), new Tuple<int, int>(6, 0));
        rbfsSolver = new RbfsAlgorithm(maze, _width, _height, new Tuple<int, int>(0, 0), new Tuple<int, int>(6, 0));
    }

    [Test]
    public void SolveBfs()
    {
        //arrange
        bool expected = true;
        
        //act
        bool actual = bfsSolver.SolveMaze();
        
        //assert
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void SolveRbfs()
    {
        //arrange
        bool expected = true;
        
        //act
        bool actual = rbfsSolver.SolveMaze();
        
        //assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}