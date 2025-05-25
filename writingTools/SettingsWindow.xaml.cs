using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace antWriter
{
    public partial class SettingsWindow : MetroWindow
    {
        public SettingsWindow(EditorWindow editorWindow)
        {
            InitializeComponent();
            FontBox.ItemsSource = Fonts.SystemFontFamilies;
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
                Application.Current.Resources["JournalFont"] = selectedFont;
            }
        }

        //Usage handlers

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
