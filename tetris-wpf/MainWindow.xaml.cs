using System;
using System.Windows;

namespace tetris_wpf
{
    public partial class MainWindow : Window
    {
        private SettingsWindow.Difficulty currentDifficulty = SettingsWindow.Difficulty.Easy;

        public MainWindow()
        {
            InitializeComponent();
            startButton.Click += StartGame;
            settingsButton.Click += ShowSettings;
            LeaderboardButton.Click += ShowLeaderboard;
        }

        private void StartGame(object sender, EventArgs e)
        {
            GameWindow gameWindow = new GameWindow(currentDifficulty);
            gameWindow.Closed += (s, args) => this.Show(); // Show main window when game window closes
            gameWindow.Show();
            this.Hide();
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Closed += (s, args) =>
            {
                currentDifficulty = settingsWindow.SelectedDifficulty;
            };
            settingsWindow.Show();
        }

        private void ShowLeaderboard(object sender, EventArgs e)
        {
            LeaderboardWindow leaderboardWindow = new LeaderboardWindow();
            leaderboardWindow.Show();

        }
    }
}