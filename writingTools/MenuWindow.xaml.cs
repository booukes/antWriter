using MahApps.Metro.Controls;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace antWriter
{
    public partial class MenuWindow : MetroWindow
    {   
        private EditorWindow editorWindow;
        public MenuWindow()
        {
            InitializeComponent();
            editorWindow = new EditorWindow();
        }
        public void Github_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).Github_Event();
        }
        public void EditorWindow_Click(object sender, RoutedEventArgs e)
        {
            editorWindow.Show();
            this.Close();
        }

        public void SettingsWindow_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(editorWindow);
            settingsWindow.Show();
            this.Close();
        }

        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
