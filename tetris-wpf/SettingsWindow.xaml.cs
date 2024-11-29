using System.Windows;
using System.Windows.Controls;

namespace tetris_wpf
{
    public partial class SettingsWindow : Window
    {
        public enum Difficulty { Easy, Medium, Hard }
        public Difficulty SelectedDifficulty { get; private set; } = Difficulty.Easy;

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb)
            {
                SelectedDifficulty = rb.Content.ToString() switch
                {
                    "Easy" => Difficulty.Easy,
                    "Medium" => Difficulty.Medium,
                    "Hard" => Difficulty.Hard,
                    _ => Difficulty.Easy
                };
            }
        }
    }
}