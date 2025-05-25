using MahApps.Metro.Controls;
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
            if ((string)Application.Current.Resources["AppChosenLogo"] == "/antWriterFinalGreen.png")
            {
                Image img = new Image
                {
                    Source = new BitmapImage(new Uri("/antWriterFinalGreen.png", UriKind.Relative))
                };
                Logo.Child = img;
            }
            else if((string)Application.Current.Resources["AppChosenLogo"] == "/antWriterFinalGreenRed.png")
            {
                Image img = new Image
                {
                    Source = new BitmapImage(new Uri("/antWriterFinalGreenRed.png", UriKind.Relative))
                };
                Logo.Child = img;
            }
            else
            {
                MessageBox.Show("No logo found.");
            }
        }
        public void Github_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).Github_Event();
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
