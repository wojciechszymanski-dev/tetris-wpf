using System.Windows.Media;

public class GameState
{
    public bool[,] Occupied { get; private set; }
    public Brush[,] Colors { get; private set; }
    private readonly int rows;
    private readonly int cols;

    public GameState(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        Occupied = new bool[rows, cols];
        Colors = new Brush[rows, cols];
    }

    public void LockBlock(GameBlock block)
    {
        for (int r = 0; r < block.Shape.Length; r++)
        {
            for (int c = 0; c < block.Shape[r].Length; c++)
            {
                if (block.Shape[r][c] == 1)
                {
                    int gridR = block.Y + r;
                    int gridC = block.X + c;
                    if (gridR >= 0 && gridR < rows && gridC >= 0 && gridC < cols)
                    {
                        Occupied[gridR, gridC] = true;
                        Colors[gridR, gridC] = block.Color;
                    }
                }
            }
        }
    }

    public void ClearRows()
    {
        for (int r = rows - 1; r >= 0; r--)
        {
            if (IsRowFull(r))
            {
                ClearRow(r);
                MoveRowsDown(r);
                r++; // Check the same row again
            }
        }
    }

    private bool IsRowFull(int row)
    {
        for (int c = 0; c < cols; c++)
        {
            if (!Occupied[row, c]) return false;
        }
        return true;
    }

    private void ClearRow(int row)
    {
        for (int c = 0; c < cols; c++)
        {
            Occupied[row, c] = false;
            Colors[row, c] = null;
        }
    }

    private void MoveRowsDown(int clearedRow)
    {
        for (int r = clearedRow; r > 0; r--)
        {
            for (int c = 0; c < cols; c++)
            {
                Occupied[r, c] = Occupied[r - 1, c];
                Colors[r, c] = Colors[r - 1, c];
            }
        }
    }
}