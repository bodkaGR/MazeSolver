namespace Lab2;

public class Menu
{
    public void Start()
    {
        Console.WriteLine("<---Welcome to program--->" + Environment.NewLine);

        try
        {
            MazeBuilder? mazeBuilder = null;

            Console.WriteLine("Enter the height and width in the range (2 - 100)");
            
            int width, height;
            height = EnterDimensionality("Enter height: ");
            width = EnterDimensionality("Enter width: ");

            int choice;
            do
            {
                Console.WriteLine("1 - Generate maze");
                Console.WriteLine("2 - Solve maze by BFS");
                Console.WriteLine("3 - Solve maze by RBFS");
                Console.WriteLine("0 - Exit");
                Console.Write("Enter your choice: ");
                
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("Exit from the program.");
                            break;
                        case 1:
                            mazeBuilder = MazeBuild(mazeBuilder, height, width);
                            break;
                        case 2:
                            SolvingMaze(mazeBuilder, width, height, choice);
                            break;
                        case 3:
                            SolvingMaze(mazeBuilder, width, height, choice);
                            break;
                        default:
                            Console.WriteLine("Invalid choice try again");
                            break;
                    }
                }
                else
                {
                    throw new Exception("Invalid choice");
                }
                Console.WriteLine("====================================================");
            } while (choice != 0);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    private void SolvingMaze(MazeBuilder? mazeBuilder, int width, int height, int choice)
    {
        if (mazeBuilder != null)
        {
            int[,] maze = mazeBuilder.GetMaze();
            AddCount(height, width, maze);
            
            Tuple<int, int> start = EnteringCoord("Enter start coordinates: ");
            Tuple<int, int> end = EnteringCoord("Enter end coordinates: ");
                                
            mazeBuilder.AddStartAndEnd(start, end);
            mazeBuilder.PrintMaze();
            
            ISolver solver = (choice == 2) ? new BfsAlgorithm(maze, width, height, start, end) : new RbfsAlgorithm(maze, width, height, start, end);
            solver.SolveMaze();
        }
        else
        {
            throw new Exception("Maze is empty");
        }
    }
    
    private Tuple<int, int> EnteringCoord(string coordinateData)
    {
        Console.WriteLine(coordinateData);
        int coordX = Int32.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
        int coordY = Int32.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
        return new Tuple<int, int>(coordX, coordY);
    }

    private MazeBuilder MazeBuild(MazeBuilder? mazeBuilder, int height, int width)
    {
        mazeBuilder = new MazeBuilder(height, width);
        mazeBuilder.GenerateMaze();
        mazeBuilder.PrintMaze();
        return mazeBuilder;
    }

    private int EnterDimensionality(string outputData)
    {
        int dimensionality;
        do
        {
            Console.Write(outputData);
            dimensionality = Int32.Parse(Console.ReadLine()!);
        } while (Checker.IfInLimit(dimensionality));
        return dimensionality;
    }

    private void AddCount(int height, int width, int[,] maze)
    {
        Console.Write(" ");
        for (int i = 0; i < width; i++)
        {
            Console.Write(i);
        }
        Console.WriteLine();

        for (int i = 0; i < height; i++)
        {
            Console.Write(i);

            for (int j = 0; j < width; j++)
            {
                Console.Write((maze[i, j] == 1 ? "█" : " "));
            }
            Console.WriteLine();
        }
    }
}