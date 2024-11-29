using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace tetris_wpf
{
    public partial class LeaderboardWindow : Window
    {
        public class ScoreEntry
        {
            public int Rank { get; set; }
            public string PlayerName { get; set; }
            public int Score { get; set; }
            public string Difficulty { get; set; }
            public DateTime Date { get; set; }
        }

        public LeaderboardWindow()
        {
            InitializeComponent();
            LoadScores();
        }

        private void LoadScores()
        {
            try
            {
                string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string projectRoot = Path.GetFullPath(Path.Combine(exeDirectory, "../../.."));
                string filePath = Path.Combine(projectRoot, "Resources", "game_logs.txt");

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("No scores found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var scores = new List<ScoreEntry>();
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    var parts = line.Split('|').Select(p => p.Trim()).ToArray();
                    if (parts.Length >= 4)
                    {
                        var scoreEntry = new ScoreEntry
                        {
                            PlayerName = parts[0],
                            Score = int.Parse(parts[1].Replace("score: ", "")),
                            Difficulty = parts[2],
                            Date = DateTime.Parse(parts[3])
                        };
                        scores.Add(scoreEntry);
                    }
                }

                // Get top 10 scores and add rank
                var topScores = scores
                    .OrderByDescending(s => s.Score)
                    .Take(10)
                    .Select((score, index) =>
                    {
                        score.Rank = index + 1;
                        return score;
                    })
                    .ToList();

                scoresListView.ItemsSource = topScores;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading scores: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}