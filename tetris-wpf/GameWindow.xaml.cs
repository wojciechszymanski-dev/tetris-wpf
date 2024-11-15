using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace tetris_wpf
{
    public partial class GameWindow : Window
    {
        private readonly int rows = 15;
        private readonly int cols = 10;
        private readonly int cellSize = 30;
        private Rectangle[,] grid;

        public GameWindow()
        {
            InitializeComponent();
            this.Closing += (s, e) => { };
            InitializeGameGrid();
        }

        private void InitializeGameGrid()
        {
            grid = new Rectangle[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    var rectangle = new Rectangle
                    {
                        Width = cellSize,
                        Height = cellSize,
                        Fill = Brushes.White,
                        Stroke = Brushes.Gray,
                        StrokeThickness = 1
                    };

                    Canvas.SetTop(rectangle, r * cellSize);
                    Canvas.SetLeft(rectangle, c * cellSize);
                    gameCanvas.Children.Add(rectangle);
                    grid[r, c] = rectangle;
                }
            }
        }
    }
}