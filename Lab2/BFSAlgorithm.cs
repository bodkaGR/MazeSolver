namespace Lab2;

public class BfsAlgorithm: ISolver
{
    private readonly int _width, _height;
    private readonly int[,] _maze;
    private readonly bool[,] _visited;
    private readonly Tuple<int, int> _start;
    private readonly Tuple<int, int> _end;
    private readonly Dictionary<Tuple<int, int>, Tuple<int, int>> _previous;

    public BfsAlgorithm(int[,] maze, int width, int height, Tuple<int, int> start, Tuple<int, int> end)
    {
        _width = width;
        _height = height;
        _maze = maze;
        _start = start;
        _end = end;
        _visited = new bool[height, width];
        _previous = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
    }

    public bool SolveMaze()
    {
        Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
        queue.Enqueue(_start);

        while (queue.Count > 0)
        {
            var cell = queue.Dequeue();
            int x = cell.Item1;
            int y = cell.Item2;

            if (x == _end.Item1 && y == _end.Item2)
            {
                Console.WriteLine($"Found the exit at ({x}, {y}).");
                PrintPath();
                return true;
            }

            if (x < 0 || y < 0 || x >= _width || y >= _height || _visited[y, x] || _maze[y, x] == 1)
                continue;

            _visited[y, x] = true;

            var neighbors = new List<Tuple<int, int>>
            {
                Tuple.Create(x - 1, y),
                Tuple.Create(x + 1, y),
                Tuple.Create(x, y - 1),
                Tuple.Create(x, y + 1)
            };

            foreach (var neighbor in neighbors)
            {
                _previous.TryAdd(neighbor, cell);
                queue.Enqueue(neighbor);
            }
        }
        Console.WriteLine("No path to the exit was found");
        return false;
    }
    private void PrintPath()
    {
        Console.WriteLine("Solution:");
        
        var path = new List<Tuple<int, int>>();
        Tuple<int, int> current = _end;

        while (current != null)
        {
            path.Add(current);
            _previous.TryGetValue(current, out current);
            if (current.Item1 == _start.Item1 && current.Item2 == _start.Item2)
            {
                break;
            }
        }

        path.Reverse();
        
        char[,] pathMaze = new char[_height, _width];
        CreateMazeWithPlus(ref pathMaze);

        foreach (var step in path)
        {
            int x = step.Item1;
            int y = step.Item2;
            if (pathMaze[y, x] == 'E')
                continue;
            
            pathMaze[y, x] = '+';
        }
        
        OutputMazeWithPlus(pathMaze);
    }

    private void OutputMazeWithPlus(char[,] pathMaze)
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Console.Write(pathMaze[y, x]);
            }
            Console.WriteLine();
        }
    }

    private void CreateMazeWithPlus(ref char[,] pathMaze)
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                switch (_maze[y, x])
                {
                    case 1:
                        pathMaze[y, x] = '█';
                        break;
                    case 0:
                        pathMaze[y, x] = ' ';
                        break;
                    case 3:
                        pathMaze[y, x] = 'S';
                        break;
                    case 4:
                        pathMaze[y, x] = 'E';
                        break;
                    default:
                        throw new Exception("Error in maze number");
                }
            }
        }
    }
}