namespace Lab2;

public class RbfsAlgorithm: ISolver
{
    private readonly int[,] _maze;
    private char[,]? _solution;
    private readonly int _height, _width;
    private readonly Tuple<int, int> _start, _end;

    public RbfsAlgorithm(int[,] maze, int width, int height, Tuple<int, int> start, Tuple<int, int> end)
    {
        _maze = maze;
        _height = height;
        _width = width;
        _start = start;
        _end = end;
        InitSolution();
    }

    private void InitSolution()
    {
        _solution = new char[_height, _width];
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                _solution[i, j] = ' ';
            }
        }
    }

    public bool SolveMaze()
    {
        bool[,] visited = new bool[_height, _width];
        return RecursiveBestFirstSearch(_start.Item2, _start.Item1, _end.Item2, _end.Item1, visited);
    }

    private bool RecursiveBestFirstSearch(int x, int y, int targetX, int targetY, bool[,] visited)
    {
        if (x == targetX && y == targetY)
        {
            Console.WriteLine($"Found the exit at ({y}, {x}).");
            PrintPath();
            return true;
        }

        if (IsOutOfBounds(x, y) || visited[x, y] || _maze[x, y] == 1)
        {
            return false;
        }

        visited[x, y] = true;
        _solution[x, y] = '+';
        
        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            int nextX = x + dx[i];
            int nextY = y + dy[i];

            if (RecursiveBestFirstSearch(nextX, nextY, targetX, targetY, visited))
            {
                return true;
            }
        }

        visited[x, y] = false;
        _solution[x, y] = ' ';
        return false;
    }

    private bool IsOutOfBounds(int x, int y)
    {
        return x < 0 || x >= _height || y < 0 || y >= _width;
    }
    
    private void PrintPath()
    {
        Console.WriteLine("Solution:");

        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                switch (_maze[i, j])
                {
                    case 1:
                        Console.Write('█'); 
                        break;
                    case 3:
                        Console.Write('S');
                        break;
                    case 4:
                        Console.Write('E');
                        break;
                    default:
                        Console.Write(_solution[i, j]);
                        break;
                }
            }
            Console.WriteLine();
        }
    }
}