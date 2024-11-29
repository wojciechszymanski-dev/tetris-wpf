using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace tetris_wpf
{
    public partial class GameWindow : Window
    {
        private readonly int rows = 15;
        private readonly int cols = 10;
        private readonly int cellSize = 30;
        private readonly double gameSpeed;
        private Rectangle[,] grid;
        private bool[,] gameState;
        private GameBlock currentBlock;
        private GameBlock nextBlock;
        private DispatcherTimer gameTimer;
        private DispatcherTimer timeUpdateTimer;
        private int score = 0;
        private string nickname;
        private DateTime gameStartTime;
        private TimeSpan elapsedTime;
        private bool scoreHasBeenSaved = false;
        private readonly SettingsWindow.Difficulty currentDifficulty;

        public GameWindow(SettingsWindow.Difficulty difficulty)
        {
            InitializeComponent();
            currentDifficulty = difficulty;  
            gameSpeed = difficulty switch
            {
                SettingsWindow.Difficulty.Easy => 500,
                SettingsWindow.Difficulty.Medium => 250,
                SettingsWindow.Difficulty.Hard => 100,
                _ => 500
            };
            this.Closing += GameWindow_Closing;
            GetNickname();
            InitializeGame();
        }

        private void GetNickname()
        {
            var dialog = new InputDialog("Enter your nickname:");
            if (dialog.ShowDialog() == true)
            {
                nickname = string.IsNullOrWhiteSpace(dialog.Answer) ? "Anon" : dialog.Answer;
            }
            else
            {
                nickname = "Anon";
            }
            gameStartTime = DateTime.Now;
        }

        private void InitializeGame()
        {
            InitializeGameGrid();
            gameState = new bool[rows, cols];
            SpawnNewBlock();
            elapsedTime = TimeSpan.Zero;

            gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(gameSpeed)
            };
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            timeUpdateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timeUpdateTimer.Tick += TimeUpdateTimer_Tick;
            timeUpdateTimer.Start();

            this.KeyDown += GameWindow_KeyDown;
            this.Focus();

            UpdateTimeDisplay();
        }

        private void TimeUpdateTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
            UpdateTimeDisplay();
        }

        private void UpdateTimeDisplay()
        {
            timeLabel.Content = $"{(int)elapsedTime.TotalMinutes}:{elapsedTime.Seconds:D2}";
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

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (currentBlock.CanMoveDown(gameState, rows, cols))
            {
                currentBlock.Y++;
                UpdateDisplay();
            }
            else
            {
                LockBlock();
                ClearFullRows();
                SpawnNewBlock();
                if (!currentBlock.CanMoveDown(gameState, rows, cols))
                {
                    GameOver();
                }
            }
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left when currentBlock.CanMoveLeft(gameState, rows, cols):
                    currentBlock.X--;
                    break;
                case Key.Right when currentBlock.CanMoveRight(gameState, rows, cols):
                    currentBlock.X++;
                    break;
                case Key.Down when currentBlock.CanMoveDown(gameState, rows, cols):
                    currentBlock.Y++;
                    break;
                case Key.R when currentBlock.CanMoveDown(gameState, rows, cols):
                    currentBlock.TryRotate(gameState, rows, cols);
                    break;
                case Key.Space when currentBlock.CanMoveDown(gameState, rows, cols):
                    for (int i = currentBlock.X; i < rows; i++)
                    {
                        if (currentBlock.CanMoveDown(gameState, rows, cols))
                        {
                            currentBlock.Y++;
                        }
                    }
                    break;
            }
            UpdateDisplay();
        }

        private void SpawnNewBlock()
        {
            if (nextBlock == null)
            {
                nextBlock = new GameBlock();
            }

            currentBlock = nextBlock;
            currentBlock.X = cols / 2 - 1;
            currentBlock.Y = 0;

            nextBlock = new GameBlock();
            UpdateDisplay();
            UpdateNextBlockPreview();
        }

        private void UpdateNextBlockPreview()
        {
            // Clear the preview canvas
            nextBlockCanvas.Children.Clear();

            // Calculate center position for the piece
            double cellSize = 15;
            double offsetX = (nextBlockCanvas.Width - (nextBlock.Shape[0].Length * cellSize)) / 2;
            double offsetY = (nextBlockCanvas.Height - (nextBlock.Shape.Length * cellSize)) / 2;

            // Draw only the blocks that make up the piece
            for (int r = 0; r < nextBlock.Shape.Length; r++)
            {
                for (int c = 0; c < nextBlock.Shape[r].Length; c++)
                {
                    if (nextBlock.Shape[r][c] == 1)
                    {
                        var rectangle = new Rectangle
                        {
                            Width = cellSize,
                            Height = cellSize,
                            Fill = nextBlock.Color,
                            Stroke = Brushes.Black,
                            StrokeThickness = 1
                        };
                        Canvas.SetTop(rectangle, offsetY + (r * cellSize));
                        Canvas.SetLeft(rectangle, offsetX + (c * cellSize));
                        nextBlockCanvas.Children.Add(rectangle);
                    }
                }
            }
        }

        private void UpdateDisplay()
        {
            // Clear display
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    grid[r, c].Fill = gameState[r, c] ? Brushes.Blue : Brushes.White;
                }
            }

            // Draw current block
            for (int r = 0; r < currentBlock.Shape.Length; r++)
            {
                for (int c = 0; c < currentBlock.Shape[r].Length; c++)
                {
                    if (currentBlock.Shape[r][c] == 1)
                    {
                        int gridR = currentBlock.Y + r;
                        int gridC = currentBlock.X + c;
                        if (gridR >= 0 && gridR < rows && gridC >= 0 && gridC < cols)
                        {
                            grid[gridR, gridC].Fill = currentBlock.Color;
                        }
                    }
                }
            }
        }

        private void LockBlock()
        {
            for (int r = 0; r < currentBlock.Shape.Length; r++)
            {
                for (int c = 0; c < currentBlock.Shape[r].Length; c++)
                {
                    if (currentBlock.Shape[r][c] == 1)
                    {
                        int gridR = currentBlock.Y + r;
                        int gridC = currentBlock.X + c;
                        if (gridR >= 0 && gridR < rows && gridC >= 0 && gridC < cols)
                        {
                            gameState[gridR, gridC] = true;
                        }
                    }
                }
            }
            score += 10;
        }

        private void ClearFullRows()
        {
            for (int r = rows - 1; r >= 0; r--)
            {
                bool rowFull = true;
                for (int c = 0; c < cols; c++)
                {
                    if (!gameState[r, c])
                    {
                        rowFull = false;
                        break;
                    }
                }

                if (rowFull)
                {
                    for (int moveR = r; moveR > 0; moveR--)
                    {
                        for (int c = 0; c < cols; c++)
                        {
                            gameState[moveR, c] = gameState[moveR - 1, c];
                        }
                    }
                    score += 100;
                    r++;
                }
            }
        }

        private void GameOver()
        {
            gameTimer.Stop();
            timeUpdateTimer.Stop();
            SaveScore();
            MessageBox.Show($"Game Over!\nScore: {score}\nTime: {timeLabel.Content}", "Game Over", MessageBoxButton.OK);
            Close();
        }

        private void GameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!scoreHasBeenSaved)
            {
                SaveScore();
            }
            if (gameTimer != null) gameTimer.Stop();
            if (timeUpdateTimer != null) timeUpdateTimer.Stop();
        }

        private void SaveScore()
        {
            if (scoreHasBeenSaved) return;

            try
            {
                // Set default name if empty
                if (string.IsNullOrWhiteSpace(nickname))
                {
                    nickname = "Anon";
                }

                // Get the execution directory
                string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Navigate up to reach the project root
                string projectRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(exeDirectory, "../../.."));

                string resourcesDirectory = System.IO.Path.Combine(projectRoot, "Resources");
                if (!Directory.Exists(resourcesDirectory))
                {
                    Directory.CreateDirectory(resourcesDirectory);
                }

                string filePath = System.IO.Path.Combine(resourcesDirectory, "game_logs.txt");
                string scoreData = $"{nickname} | score: {score} | {currentDifficulty} | {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                File.AppendAllText(filePath, scoreData + Environment.NewLine);
                scoreHasBeenSaved = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving score: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class InputDialog : Window
    {
        private TextBox textBox;
        public string Answer => textBox.Text;

        public InputDialog(string question)
        {
            Width = 300;
            Height = 200;
            Title = "Input";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var grid = new Grid();
            Content = grid;

            var label = new Label { Content = question, Margin = new Thickness(10) };
            textBox = new TextBox { Margin = new Thickness(10) };
            var button = new Button { Content = "OK", Width = 60, Height = 25, Margin = new Thickness(10) };

            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            Grid.SetRow(label, 0);
            Grid.SetRow(textBox, 1);
            Grid.SetRow(button, 2);

            grid.Children.Add(label);
            grid.Children.Add(textBox);
            grid.Children.Add(button);

            button.Click += (s, e) => { DialogResult = true; };
            textBox.KeyDown += (s, e) => { if (e.Key == Key.Enter) DialogResult = true; };
        }
    }
}