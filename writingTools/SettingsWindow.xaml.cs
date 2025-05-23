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

namespace antWriter
{
    public partial class SettingsWindow : Window
    {
        private readonly EditorWindow _editorWindow;

        public SettingsWindow(EditorWindow editorWindow)
        {
            InitializeComponent();
            _editorWindow = editorWindow;

            // Populate FontBox with system fonts
            FontBox.ItemsSource = Fonts.SystemFontFamilies;
        }
        
        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            this.Close();
        }
        public void ButtonSettings(object sender, RoutedEventArgs e)
        {
            if (sender is Button flag)
            {
                switch (flag.Name)
                {
                    case "Test_Settings":
                        MessageBox.Show("Debug Test Settings");
                        break;
                    case "color":
                        Application.Current.Resources["AppBackgroundBrush"]= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CF9FFF"));
                        break;
                    default:
                        MessageBox.Show("Debug Default Settings");
                        break;
                }
            }
        }
        public void ChangeFont(object sender, SelectionChangedEventArgs e)
        {
            if (FontBox.SelectedItem is FontFamily selectedFont)
            {
                Application.Current.Resources["JournalFont"] = selectedFont;
            }
        }

        private void Test_Settings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
