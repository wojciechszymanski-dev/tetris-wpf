using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace gobblet_gobblers_xo
{
    public partial class SettingsWindow : Window
    {
        public static int BoardSize { get; private set; } = 3; // Default to 3x3

        public SettingsWindow()
        {
            InitializeComponent();
            LoadCurrentSettings();
        }

        private void LoadCurrentSettings()
        {
            // Select the appropriate radio button based on current board size
            var radioButtons = this.FindChildren<RadioButton>();
            foreach (var rb in radioButtons)
            {
                if (rb.Tag != null && rb.Tag.ToString() == BoardSize.ToString())
                {
                    rb.IsChecked = true;
                    break;
                }
            }
        }

        private void BoardSize_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.Tag != null)
            {
                BoardSize = int.Parse(radioButton.Tag.ToString());
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings saved successfully!", "Settings Saved",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }
    }

    // Helper extension method
    public static class ControlExtensions
    {
        public static IEnumerable<T> FindChildren<T>(this DependencyObject parent) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T childType)
                {
                    yield return childType;
                }

                foreach (var descendant in FindChildren<T>(child))
                {
                    yield return descendant;
                }
            }
        }
    }
}