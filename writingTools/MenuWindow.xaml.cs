using System.Windows;

namespace antWriter
{
    public partial class MenuWindow : Window
    {
        SettingsWindow settingsWindow = new SettingsWindow();
        EditorWindow editorWindow = new EditorWindow();
        public string Username { get; set; } = (string)Application.Current.Resources["Username"];
        public MenuWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            
        }
        public void EditorWindow_Click(object sender, RoutedEventArgs e)
        {
            editorWindow.Show();
            this.Hide();
        }

        public void SettingsWindow_Click(object sender, RoutedEventArgs e)
        {
            settingsWindow.Show();
            this.Hide();
        }

        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
