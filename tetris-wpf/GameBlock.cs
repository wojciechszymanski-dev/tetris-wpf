using System.Windows.Media;

public class GameBlock
{
    public int[][] Shape { get; private set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Brush Color { get; private set; }

    private static readonly int[][][] Shapes = new int[][][]
    {
        // I-shape
        new int[][]
        {
            new int[] { 1 },
            new int[] { 1 },
            new int[] { 1 },
            new int[] { 1 }
        },
        // L-shape
        new int[][]
        {
            new int[] { 1, 0 },
            new int[] { 1, 0 },
            new int[] { 1, 1 }
        },
        // Square
        new int[][]
        {
            new int[] { 1, 1 },
            new int[] { 1, 1 }
        },
        // T-shape
        new int[][]
        {
            new int[] { 1, 1, 1 },
            new int[] { 0, 1, 0 }
        },
        // Z-shape
        new int[][]
        {
            new int[] { 1, 1, 0 },
            new int[] { 0, 1, 1 }
        }
    };

    private static readonly Brush[] Colors = new Brush[]
    {
        Brushes.Cyan,      // I-shape
        Brushes.Orange,    // L-shape
        Brushes.Yellow,    // Square
        Brushes.Purple,    // T-shape
        Brushes.Red        // Z-shape
    };

    public GameBlock()
    {
        Random random = new Random();
        int blockType = random.Next(Shapes.Length);
        Shape = Shapes[blockType];
        Color = Colors[blockType];
    }

    public bool CanMoveDown(bool[,] gameState, int rows, int cols)
    {
        for (int r = 0; r < Shape.Length; r++)
        {
            for (int c = 0; c < Shape[r].Length; c++)
            {
                if (Shape[r][c] == 1)
                {
                    int newY = Y + r + 1;
                    int newX = X + c;

                    if (newY >= rows || gameState[newY, newX])
                        return false;
                }
            }
        }
        return true;
    }

    public bool CanMoveLeft(bool[,] gameState, int rows, int cols)
    {
        for (int r = 0; r < Shape.Length; r++)
        {
            for (int c = 0; c < Shape[r].Length; c++)
            {
                if (Shape[r][c] == 1)
                {
                    int newY = Y + r;
                    int newX = X + c - 1;

                    if (newX < 0 || gameState[newY, newX])
                        return false;
                }
            }
        }
        return true;
    }

    public bool CanMoveRight(bool[,] gameState, int rows, int cols)
    {
        for (int r = 0; r < Shape.Length; r++)
        {
            for (int c = 0; c < Shape[r].Length; c++)
            {
                if (Shape[r][c] == 1)
                {
                    int newY = Y + r;
                    int newX = X + c + 1;

                    if (newX >= cols || gameState[newY, newX])
                        return false;
                }
            }
        }
        return true;
    }
}