using MahApps.Metro.Controls;
using Serilog;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace antWriter
{
    public partial class MenuWindow : MetroWindow
    {   
        private EditorWindow editorWindow;

        public string Username { get; set; } = (string)Application.Current.Resources["Username"];
        public MenuWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Generate_Logo();
            
        }
        public void Generate_Logo()
        {
            if ((string)Application.Current.Resources["AppChosenLogo"] == "/greenLogo.png")
            {
                Image img = new Image
                {
                    Source = new BitmapImage(new Uri("/greenLogo.png", UriKind.Relative))
                };
                Logo.Child = img;
            }
            else if((string)Application.Current.Resources["AppChosenLogo"] == "/fallbackLogo.png")
            {
                Image img = new Image
                {
                    Source = new BitmapImage(new Uri("/fallbackLogo.png", UriKind.Relative))
                };
                Logo.Child = img;
            }
            else
            {
                Log.Error("No logo found.");
            }
        }
        public void EditorWindow_Click(object sender, RoutedEventArgs e)
        {
            EditorWindow editorWindow = new EditorWindow();
            editorWindow.Show();
            this.Close();
        }

        public void SettingsWindow_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            this.Close();
        }

        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
