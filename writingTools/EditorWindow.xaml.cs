using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace antWriter
{
    public partial class EditorWindow : Window
    {
        List<string> recentFiles = new List<string>();
        private protected string currentFile;
        public EditorWindow()
        {
            InitializeComponent();
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
                        btn.Background = (Brush)new BrushConverter().ConvertFromString("#BAC095");;
                        btn.IsHitTestVisible = false;
                    }
                    else
                    {
                        btn.Background = Brushes.Transparent;
                        btn.IsHitTestVisible = true;// Reset others
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
                    Margin = new Thickness(0, -1, 0, -1),
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0, 1, 0, 1),
                    Tag = file
                };

                btn.Click += LoadRecent_Click;

                RecentFiles.Children.Add(btn);
            }
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

        public void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                currentFile = openFileDialog.FileName;
                AddRecentFile(currentFile);
                recentFiles.Add(currentFile);
                EditingBoard.Text = File.ReadAllText(currentFile);
                HighlightActiveFileButton();
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save As";
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, EditingBoard.Text);
                currentFile = filePath;
                MessageBox.Show("File saved successfully!", "Success");
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
            File.WriteAllText(filePath, EditingBoard.Text);
        }

        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            this.Close();
        }
    }
}
