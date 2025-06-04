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
            InputDefaulter();
            Generate_Logo();
        }

        private void InputDefaulter() 
        {
            switch ((string)Application.Current.Resources["AppNavbarTheme"])
            {
                case "kitty": kittyNav.IsChecked = true; break;
                case "seamless": seamlessNav.IsChecked = true; break;
                case "pig": pigNav.IsChecked = true; break;
                case "normal": normalNav.IsChecked = true; break;
            }
            ((string)Application.Current.Resources["AppChosenLogo"] == "/antWriterFinalGreen.png" ? normal : edgy).IsChecked = true;
            Name.Text = (string)Application.Current.Resources["Username"];
            fontSizeSelect.Value = (double)Application.Current.Resources["FontSize"];
            FontBox.SelectedItem = (FontFamily)Application.Current.Resources["AppEditorFont"];
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
                Log.Warning("No logo found.");
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

        public void NavClick(object sender, RoutedEventArgs e)
        {
            if ((bool)kittyNav.IsChecked)
            {
                ConfigManager.Config.Editor.Navbar = "kitty";
                Application.Current.Resources["AppNavbarTheme"] = "kitty";
                Log.Information("kitty mode!");
                Log.Information((string)Application.Current.Resources["AppNavbarTheme"]);
            }
            else if ((bool)seamlessNav.IsChecked) 
            {
                ConfigManager.Config.Editor.Navbar = "seamless";
                Application.Current.Resources["AppNavbarTheme"] = "seamless";
                Log.Information("seamless mode!");
                Log.Information((string)Application.Current.Resources["AppNavbarTheme"]);
            }
            else if ((bool)pigNav.IsChecked)
            {
                ConfigManager.Config.Editor.Navbar = "pig";
                Application.Current.Resources["AppNavbarTheme"] = "pig";
                Log.Information("pig mode!");
                Log.Information((string)Application.Current.Resources["AppNavbarTheme"]);
            }
            else if ((bool)normalNav.IsChecked)
            {
                ConfigManager.Config.Editor.Navbar = "normal";
                Application.Current.Resources["AppNavbarTheme"] = "normal";
                Log.Information("normal mode!");
                Log.Information((string)Application.Current.Resources["AppNavbarTheme"]);
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
        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            this.Close();
        }
    }
}
