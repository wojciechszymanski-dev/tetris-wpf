using System.Windows.Media;

public class GameBlock
{
    public int[][] Shape { get; private set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Brush Color { get; private set; }
    private readonly int blockType;

    private static readonly int[][][] Shapes =
    [
        // I-shape
        new int[][]
        {
            new int[] { 0, 0, 0, 0 },
            new int[] { 1, 1, 1, 1 },
            new int[] { 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0 }
        },
        // L-shape
        new int[][]
        {
            new int[] { 1, 0 },
            new int[] { 1, 0 },
            new int[] { 1, 1 }
        },
        // ⅃-shape
        new int[][]
        {
            new int[] { 0, 1 },
            new int[] { 0, 1 },
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
        },
        // S-shape
        new int[][]
        {
            new int[] { 0, 1, 1 },
            new int[] { 1, 1, 0 }
        }
    ];

    public static readonly Brush[] Colors = new Brush[]
    {
        Brushes.Cyan,      // I-shape
        Brushes.Orange,    // L-shape
        Brushes.Gray,      // ⅃-shape
        Brushes.Yellow,    // Square
        Brushes.Purple,    // T-shape
        Brushes.Red,       // Z-shape
        Brushes.LimeGreen  // S-shape
    };

    public GameBlock()
    {
        Random random = new Random();
        blockType = random.Next(Shapes.Length);
        Shape = DeepCopyShape(Shapes[blockType]);
        Color = Colors[blockType];
    }

    private int[][] DeepCopyShape(int[][] source)
    {
        int[][] copy = new int[source.Length][];
        for (int i = 0; i < source.Length; i++)
        {
            copy[i] = new int[source[i].Length];
            Array.Copy(source[i], copy[i], source[i].Length);
        }
        return copy;
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

    public bool TryRotate(bool[,] gameState, int rows, int cols)
    {
        int[][] rotatedShape = GetRotatedShape();

        // Get wall kick offsets based on block type
        (int X, int Y)[] offsets = blockType == 0 ? GetIBlockOffsets() : GetRegularOffsets();

        foreach (var (offsetX, offsetY) in offsets)
        {
            if (CanPlaceShape(rotatedShape, X + offsetX, Y + offsetY, gameState, rows, cols))
            {
                Shape = rotatedShape;
                X += offsetX;
                Y += offsetY;
                return true;
            }
        }

        return false;
    }

    private int[][] GetRotatedShape()
    {
        int rows = Shape.Length;
        int cols = Shape[0].Length;
        int[][] rotated = new int[cols][];

        for (int i = 0; i < cols; i++)
        {
            rotated[i] = new int[rows];
            for (int j = 0; j < rows; j++)
            {
                rotated[i][j] = Shape[rows - 1 - j][i];
            }
        }

        return rotated;
    }

    private bool CanPlaceShape(int[][] shape, int newX, int newY, bool[,] gameState, int rows, int cols)
    {
        for (int r = 0; r < shape.Length; r++)
        {
            for (int c = 0; c < shape[r].Length; c++)
            {
                if (shape[r][c] == 1)
                {
                    int gridY = newY + r;
                    int gridX = newX + c;

                    if (gridX < 0 || gridX >= cols || gridY < 0 || gridY >= rows || gameState[gridY, gridX])
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private (int X, int Y)[] GetIBlockOffsets()
    {
        return new[]
        {
            (0, 0),   // Original position
            (-1, 0),  // Left
            (1, 0),   // Right
            (0, 1),   // Down
            (0, -1),  // Up
            (-2, 0),  // Far left
            (2, 0),   // Far right
        };
    }

    private (int X, int Y)[] GetRegularOffsets()
    {
        return new[]
        {
            (0, 0),   // Original position
            (-1, 0),  // Left
            (1, 0),   // Right
            (0, -1),  // Up
            (0, 1),   // Down
        };
    }
}