using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace antWriter
{
    public partial class EditorWindow : MetroWindow
    {
        List<string> recentFiles = new List<string>();
        private protected string currentFile;
        TextBlock noRecentFiles = new TextBlock
        {
            Text = "no recently used files to be shown here...",
            FontSize = 20,
            TextWrapping = TextWrapping.Wrap,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(5),
            Foreground = (SolidColorBrush)Application.Current.Resources["AppFontColor"],
            FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "{StaticResource AppMenusFont}")
        };
        public EditorWindow()
        {
            InitializeComponent();
            ShowFileName();
            RecentFiles.Children.Add(noRecentFiles);
            New.IsHitTestVisible = false;
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

        public void Github_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).Github_Event();
        }

        void HighlightActiveFileButton()
        {
            foreach (var child in RecentFiles.Children)
            {
                if (child is Button btn)
                {
                    string btnPath = btn.Tag as string;

                    if (btnPath != null && btnPath == currentFile)
                    {
                        btn.Background = (SolidColorBrush)Application.Current.Resources["AppCompliementaryBrush"];
                        btn.IsHitTestVisible = false;
                    }
                    else
                    {
                        btn.Background = Brushes.Transparent;
                        btn.IsHitTestVisible = true;
                    }
                }
            }
        }

        void AddRecentFile(string filePath)
        {
            filePath = System.IO.Path.GetFullPath(filePath);

            recentFiles.Insert(0, filePath);

            recentFiles = recentFiles.Distinct().Take(5).ToList();

            RecentFiles.Children.Clear();

            foreach (string file in recentFiles)
            {
                Button btn = new Button
                {
                    Content = System.IO.Path.GetFileName(file),
                    FontFamily = (FontFamily)Application.Current.Resources["AppMenusFont"],
                    FontSize = 30,
                    Foreground = (SolidColorBrush)Application.Current.Resources["AppFontColor"],
                    Margin = new Thickness(0, -1, 0, -1),
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.Black,
                    Height = 60,
                    BorderThickness = new Thickness(0, 1, 0, 1),
                    Tag = file
                };

                btn.Click += LoadRecent_Click;

                RecentFiles.Children.Add(btn);
            }
        }
        private void ShowFileName()
        {
            TextBlock tb = new TextBlock();

            if (currentFile == null)
            {
                tb.Text = "working in volatile state. use 'save as' to persist changes.";
                tb.FontFamily = (FontFamily)Application.Current.Resources["AppMenusItalicFont"];
            }
            else
            {
                tb.Text = currentFile;
                tb.FontFamily = (FontFamily)Application.Current.Resources["AppMenusFont"];
            }
                tb.Margin = new Thickness(2);
            
            tb.Foreground = (SolidColorBrush)Application.Current.Resources["AppFontColor"];

            tb.HorizontalAlignment = HorizontalAlignment.Left;
            tb.VerticalAlignment = VerticalAlignment.Center;

            fileNameHeader.Child = tb;
        }

        private void LoadRecent_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedBtn)
            {
                string filePath = clickedBtn.Tag as string;

                if (File.Exists(filePath))
                {
                    try
                    {
                        string content = File.ReadAllText(filePath);
                        InternalSave(currentFile);
                        EditingBoard.Text = content;
                        currentFile = filePath;
                        ShowFileName();
                        HighlightActiveFileButton();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error reading file:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"File not found:\n{filePath}", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void CreateNewFile_Click(object sender, RoutedEventArgs e)
        {
            InternalSave(currentFile);
            EditingBoard.Text = "";
            currentFile = null;
            ShowFileName();
        }
        public void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                InternalSave(currentFile);
                currentFile = openFileDialog.FileName;
                AddRecentFile(currentFile);
                recentFiles.Add(currentFile);
                EditingBoard.Text = File.ReadAllText(currentFile);
                ShowFileName();
                HighlightActiveFileButton();
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            New.IsHitTestVisible = true;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save As";
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, EditingBoard.Text);
                currentFile = filePath;
                AddRecentFile(currentFile);
                ShowFileName();
            }
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile != null)
            {
                string text = EditingBoard.Text;
                File.WriteAllText(currentFile, text);
            }
            else 
            {
                MessageBox.Show("Please use Save As first!");
            }
        }
        public void InternalSave(string filePath)
        {
            New.IsHitTestVisible = true;
            if (!File.Exists(filePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Save As";
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == true)
                {
                    filePath = saveFileDialog.FileName;
                    File.WriteAllText(filePath, EditingBoard.Text);
                    currentFile = filePath;
                    AddRecentFile(currentFile);
                    ShowFileName();
                }
            }
            else
            {
                File.WriteAllText(filePath, EditingBoard.Text);
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
