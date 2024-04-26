namespace Lab2;

public class MazeBuilder
{
    private readonly int[,] _maze;
    private readonly int _width;
    private readonly int _height;

    public MazeBuilder(int height, int width)
    {
        _width = width;
        _height = height;
        _maze = new int[height, width];
        InitializeMaze();
    }

    public void AddStartAndEnd(Tuple<int, int> start, Tuple<int, int> end)
    {
        if (_maze[start.Item2, start.Item1] != 1 && _maze[end.Item2, end.Item1] != 1)
        {
            _maze[start.Item2, start.Item1] = 3;
            _maze[end.Item2, end.Item1] = 4;
        }
        else
        {
            throw new Exception("start or end point on the wall");
        }
    }

    public int[,] GetMaze()
    {
        return _maze;
    }

    private void InitializeMaze()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _maze[y, x] = 1;
            }
        }
    }

    public void PrintMaze()
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (_maze[y, x] == 0 || _maze[y, x] == 1)
                {
                    Console.Write((_maze[y, x] == 0) ? " " : "█");
                }
                else if (_maze[y, x] == 3)
                {
                    Console.Write("S");
                }
                else
                {
                    Console.Write("E");
                }
            }
            Console.WriteLine();
        }
    }

    public void GenerateMaze()
    {
        Random random = new Random();
        int seed = random.Next(); 
        random = new Random(seed); 
        MazeGeneration(random, 0, 0);
    }

    private void MazeGeneration(Random random, int x, int y)
    {
        _maze[y, x] = 0;

        int[] directions = { 1, 2, 3, 4 };
        Shuffle(directions, random);

        foreach (int direction in directions)
        {
            int newX = x;
            int newY = y;

            switch (direction)
            {
                case 1: 
                    newY -= 2;
                    break;
                case 2: 
                    newX += 2;
                    break;
                case 3: 
                    newY += 2;
                    break;
                case 4: 
                    newX -= 2;
                    break;
            }

            if (IsInBounds(newX, newY) && _maze[newY, newX] == 1)
            {
                int wallX = x + (newX - x) / 2;
                int wallY = y + (newY - y) / 2;
                _maze[wallY, wallX] = 0;
                MazeGeneration(random, newX, newY);
            }
        }
    }

    private bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < _width && y >= 0 && y < _height;
    }

    private void Shuffle(int[] array, Random random)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
}