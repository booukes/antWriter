using MahApps.Metro.Controls;
using Serilog;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace antWriter
{
    public partial class SettingsWindow : MetroWindow
    {
        public SettingsWindow()
        {
            InitializeComponent();
            FontBox.ItemsSource = Fonts.SystemFontFamilies;
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
            else if ((string)Application.Current.Resources["AppChosenLogo"] == "/antWriterFinalGreenRed.png")
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

        public void Name_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            ConfigManager.Config.Editor.Username = name;
            ConfigManager.Save();
            Application.Current.Resources["Username"] = name;
        }

        public void Logo_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)normal.IsChecked)
            {
                ConfigManager.Config.Editor.Logo = "/antWriterFinalGreen.png";
                Application.Current.Resources["AppChosenLogo"] = "/antWriterFinalGreen.png";
                Log.Information("Green logo selected.");
                Log.Information((string)Application.Current.Resources["AppChosenLogo"]);
            }
            else if ((bool)edgy.IsChecked)
            {
                ConfigManager.Config.Editor.Logo = "/antWriterFinalGreenRed.png";
                Application.Current.Resources["AppChosenLogo"] = "/antWriterFinalGreenRed.png";
                Log.Information("Red logo selected.");
                Log.Information((string)Application.Current.Resources["AppChosenLogo"]);
            }
            ConfigManager.Save();
            
        }
        public void FontSize_Click(object sender, RoutedEventArgs e)
        {
            if (fontSizeSelect.Value.HasValue)
            {
                double fontSize = (double)fontSizeSelect.Value;
                ConfigManager.Config.Font.Size = fontSize;
                ConfigManager.Save();
                Application.Current.Resources["FontSize"] = fontSize;
            }
        }
        public void FontFamily_Click(object sender, SelectionChangedEventArgs e)
        {
            if (FontBox.SelectedItem is FontFamily selectedFont)
            {
                ConfigManager.Config.Font.Family = Convert.ToString(selectedFont);
                ConfigManager.Save();
                Application.Current.Resources["AppEditorFont"] = selectedFont;
            }
        }

        public void Github_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).Github_Event();
        }
        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            this.Close();
        }
    }
}
