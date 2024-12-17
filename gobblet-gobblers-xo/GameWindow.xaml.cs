using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace gobblet_gobblers_xo
{
    public partial class GameWindow : Window
    {
        private readonly int boardSize;
        private Button[,] boardButtons;
        private GamePiece selectedPiece;
        private bool isBluePlayerTurn = true;

        private DispatcherTimer gameTimer;
        private TimeSpan gameTime;

        // Piece counts (unlimited for farmers)
        private Dictionary<GamePiece, int> pieceRemaining = new Dictionary<GamePiece, int>();

        // Pieces for both players
        public GamePiece BlueFarmer { get; } = new GamePiece("/Resources/farmer-figure-blue.png", 1, true);
        public GamePiece BlueHorseman { get; } = new GamePiece("/Resources/horseman-figure-blue.png", 2, true);
        public GamePiece BlueKnight { get; } = new GamePiece("/Resources/knight-figure-blue.png", 3, true);
        public GamePiece BlueKing { get; } = new GamePiece("/Resources/king-figure-blue.png", 4, true);

        public GamePiece RedFarmer { get; } = new GamePiece("/Resources/farmer-figure-2-red.png", 1, false);
        public GamePiece RedHorseman { get; } = new GamePiece("/Resources/horseman-figure-red.png", 2, false);
        public GamePiece RedKnight { get; } = new GamePiece("/Resources/knight-figure-red.png", 3, false);
        public GamePiece RedKing { get; } = new GamePiece("/Resources/king-figure-red.png", 4, false);

        public GameWindow()
        {
            InitializeComponent();
            InitializeTimer();
            boardSize = SettingsWindow.BoardSize;
            InitializeBoard();
            InitializePieceCounts();
            this.DataContext = this;
            this.Closed += (s, e) => gameTimer.Stop();
        }

        private void InitializeTimer()
        {
            gameTime = TimeSpan.Zero;
            gameTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                gameTime = gameTime.Add(TimeSpan.FromSeconds(1));
                GameTimeText.Text = $"Time: {gameTime.ToString(@"hh\:mm\:ss")}";
            }, Application.Current.Dispatcher);
            gameTimer.Start();
        }

        private void InitializeBoard()
        {
            // Set up the grid with equal sized cells
            for (int i = 0; i < boardSize; i++)
            {
                GameBoard.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                GameBoard.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            GameBoard.HorizontalAlignment = HorizontalAlignment.Center;

            // Calculate the size for the game board (60% of the smaller screen dimension)
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double gameBoardSize = Math.Min(screenHeight, screenWidth) * 0.6;

            GameBoard.Width = gameBoardSize;
            GameBoard.Height = gameBoardSize;

            // Create the buttons
            boardButtons = new Button[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    var button = new Button
                    {
                        Background = Brushes.Transparent,
                        BorderThickness = new Thickness(1),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(139, 69, 19))  // Brown border
                    };
                    button.MouseEnter += OnBoardCellHover;
                    button.MouseLeave += OnBoardCellLeave;
                    button.Click += OnBoardCellClick;
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    GameBoard.Children.Add(button);
                    boardButtons[i, j] = button;
                }
            }
        }

        private void OnBoardCellHover(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is Button button && selectedPiece != null)
            {
                var currentPiece = button.Tag as GamePiece;
                bool isLegalMove = currentPiece == null || selectedPiece.Weight > currentPiece.Weight;

                button.BorderBrush = new SolidColorBrush(
                    isLegalMove ? Colors.LightGreen : Colors.Red);
                button.BorderThickness = new Thickness(2);
            }
        }

        private void OnBoardCellLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.BorderBrush = new SolidColorBrush(Color.FromRgb(139, 69, 19));
                button.BorderThickness = new Thickness(1);
            }
        }

        private void InitializePieceCounts()
        {
            // Initialize piece counts based on their weights
            pieceRemaining[BlueFarmer] = int.MaxValue;  // Unlimited
            pieceRemaining[BlueHorseman] = boardSize;
            pieceRemaining[BlueKnight] = boardSize / 2;
            pieceRemaining[BlueKing] = boardSize / 3;

            pieceRemaining[RedFarmer] = int.MaxValue;   // Unlimited
            pieceRemaining[RedHorseman] = boardSize;
            pieceRemaining[RedKnight] = boardSize / 2;
            pieceRemaining[RedKing] = boardSize / 3;

            // Update visual state of all pieces
            UpdatePieceButtonStates();
        }

        private void UpdatePieceButtonStates()
        {
            foreach (var button in FindVisualChildren<Button>(this))
            {
                if (button.Tag is GamePiece piece && button.Parent is StackPanel)  // Only affect side panel buttons
                {
                    if (pieceRemaining[piece] <= 0)
                    {
                        button.Opacity = 0.3;
                        button.IsEnabled = false;
                    }
                    else
                    {
                        button.Opacity = 1.0;
                        button.IsEnabled = true;
                    }
                }
            }

            // Update count displays
            BlueHorsemanCount.Text = pieceRemaining[BlueHorseman].ToString();
            BlueKnightCount.Text = pieceRemaining[BlueKnight].ToString();
            BlueKingCount.Text = pieceRemaining[BlueKing].ToString();

            RedHorsemanCount.Text = pieceRemaining[RedHorseman].ToString();
            RedKnightCount.Text = pieceRemaining[RedKnight].ToString();
            RedKingCount.Text = pieceRemaining[RedKing].ToString();
        }

        public void OnPieceClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is GamePiece piece)
            {
                if (piece.IsBlue != isBluePlayerTurn)
                {
                    MessageBox.Show("It's not your turn!", "Invalid Move",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (pieceRemaining[piece] <= 0 && piece.Weight > 1)
                {
                    MessageBox.Show("No more pieces of this type available!", "Invalid Move",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                selectedPiece = piece;
            }
        }

        private void OnBoardCellClick(object sender, RoutedEventArgs e)
        {
            if (selectedPiece == null)
                return;

            if (sender is Button cellButton)
            {
                if (TryPlacePiece(cellButton, selectedPiece))
                {
                    // Decrease piece count if not a farmer
                    if (selectedPiece.Weight > 1)
                    {
                        pieceRemaining[selectedPiece]--;
                        UpdatePieceButtonStates();
                    }

                    // Switch turns
                    isBluePlayerTurn = !isBluePlayerTurn;
                    CurrentPlayerText.Text = $"Current Player: {(isBluePlayerTurn ? "Blue" : "Red")}";
                    selectedPiece = null;

                    // Check for win condition
                    CheckWinCondition();
                }
            }
        }

        private bool TryPlacePiece(Button cell, GamePiece newPiece)
        {
            var currentPiece = cell.Tag as GamePiece;

            // If cell is empty or new piece is larger
            if (currentPiece == null || newPiece.Weight > currentPiece.Weight)
            {
                cell.Tag = newPiece;
                var image = new Image
                {
                    Source = new BitmapImage(new Uri(newPiece.ImageSource, UriKind.Relative)),
                    Width = cell.ActualWidth * 0.8,
                    Height = cell.ActualHeight * 0.8,
                    Opacity = 1.0 
                };
                cell.Content = image;
                cell.Opacity = 1.0;
                return true;
            }

            MessageBox.Show("Invalid move! You can only place a larger piece on top of a smaller one.",
                          "Invalid Move", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        private void CheckWinCondition()
        {
            // Check rows, columns and diagonals
            var winningLines = new List<GamePiece[]>();

            // Add rows
            for (int i = 0; i < boardSize; i++)
                winningLines.Add(GetRow(i));

            // Add columns
            for (int i = 0; i < boardSize; i++)
                winningLines.Add(GetColumn(i));

            // Add diagonals
            winningLines.Add(GetDiagonal(true));
            winningLines.Add(GetDiagonal(false));

            foreach (var line in winningLines)
            {
                if (CheckWinInLine(line))
                    return;
            }
        }

        private GamePiece[] GetRow(int row)
        {
            GamePiece[] pieces = new GamePiece[boardSize];
            for (int i = 0; i < boardSize; i++)
                pieces[i] = boardButtons[row, i].Tag as GamePiece;
            return pieces;
        }

        private GamePiece[] GetColumn(int col)
        {
            GamePiece[] pieces = new GamePiece[boardSize];
            for (int i = 0; i < boardSize; i++)
                pieces[i] = boardButtons[i, col].Tag as GamePiece;
            return pieces;
        }

        private GamePiece[] GetDiagonal(bool mainDiagonal)
        {
            GamePiece[] pieces = new GamePiece[boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                int j = mainDiagonal ? i : boardSize - 1 - i;
                pieces[i] = boardButtons[i, j].Tag as GamePiece;
            }
            return pieces;
        }

        private bool CheckWinInLine(GamePiece[] pieces)
        {
            if (!pieces.Any() || pieces.Any(p => p == null)) return false;

            bool allBlue = pieces.All(p => p.IsBlue);
            bool allRed = pieces.All(p => !p.IsBlue);

            if (allBlue || allRed)
            {
                gameTimer.Stop();  // Stop timer when game ends

                string winner = allBlue ? "Blue" : "Red";
                MessageBox.Show($"Player {winner} wins!\nGame Time: {gameTime.ToString(@"hh\:mm\:ss")}", "Game Over",
                              MessageBoxButton.OK, MessageBoxImage.Information);

                var result = MessageBox.Show("Would you like to play again?", "New Game",
                                           MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var newGame = new GameWindow();
                    newGame.Show();
                    this.Close();
                }
                else
                {
                    var mainMenu = new MainWindow();
                    mainMenu.Show();
                    this.Close();
                }
                return true;
            }

            return false;
        }

        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to return to the menu? Current game progress will be lost.",
                                       "Return to Menu", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                gameTimer.Stop();  // Stop timer when returning to menu
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        // Helper method to find all visual children of a type (chatgpt helped with this one)
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                if (child != null && child is T t)
                    yield return t;

                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }
    }
}