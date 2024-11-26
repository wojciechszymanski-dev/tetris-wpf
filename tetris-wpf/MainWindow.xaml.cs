using System;
using System.Windows;

namespace tetris_wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            startButton.Click += StartGame;
            settingsButton.Click += ShowSettings;
        }

        private void StartGame(object sender, EventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Closed += (s, args) => this.Show(); // Show main window when game window closes
            gameWindow.Show();
            this.Hide();
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Closed += (s, args) => this.Show(); // Show main window when game window closes
            settingsWindow.Show();
        }
    }
}