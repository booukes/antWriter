using MahApps.Metro.Controls;
using Microsoft.Win32;
using Serilog;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
namespace antWriter
{
    public partial class EditorWindow : MetroWindow
    {
        // --- Fields ---
        private bool ASEvent = false;             // Flag to control asynchronous file IO flow
        private readonly DispatcherTimer timer;   // Timer to trigger periodic autosave
        private bool _hasUnsavedChanges = false;  // Track if document is unsaved
        private string currentFile = null;         // Currently loaded file path
        private bool _isLoading = false;           // Loading state flag to prevent re-entry

        private List<string> recentFiles = new List<string>(); // Recent file list


        TextBlock noRecentFiles = new TextBlock
        {
            Text = "no recently used files to be shown here...",
            FontSize = 20,
            TextWrapping = TextWrapping.Wrap,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(5),
            Foreground = (SolidColorBrush)Application.Current.Resources["AppFontColor"],
            FontFamily = (FontFamily)Application.Current.Resources["AppMenusItalicFont"]
        };

        TextBlock useSaveAs = new TextBlock
        {
            Text = CONST.UI.NO_SAVE_FILE,
            FontSize = 20,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(5),
            Foreground = (SolidColorBrush)Application.Current.Resources["AppFontColor"],
            FontFamily = (FontFamily)Application.Current.Resources["AppMenusItalicFont"]
        };

        TextBlock loadingPrompt = new TextBlock
        {
            Text = CONST.UI.LOADING_FILE,
            FontSize = 20,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(5),
            Foreground = (SolidColorBrush)Application.Current.Resources["AppFontColor"],
            FontFamily = (FontFamily)Application.Current.Resources["AppMenusItalicFont"]
        };

        public EditorWindow()
        {
            InitializeComponent();

            // Initialize UI components
            ShowFileName();
            Generate_Logo();

            // Setup recent files panel with placeholder
            RecentFiles.Children.Clear();
            RecentFiles.Children.Add(noRecentFiles);

            // Disable New button initially
            New.IsHitTestVisible = false;

            // Register TextChanged event on editing board to detect unsaved changes
            EditingBoard.TextChanged += EditingBoard_Trigger;

            // Initialize and start autosave timer (ticks every second)
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += AsyncServiceTick;
            timer.Start();

            Log.Information("Async worker thread started at:" + DateTime.Now);
        }

        /// <summary>
        /// Loads and sets the logo image from application resources.
        /// </summary>
        public void Generate_Logo()
        {
            string logoPath = (string)Application.Current.Resources["AppChosenLogo"];
            if (!string.IsNullOrEmpty(logoPath))
            {
                Image img = new Image
                {
                    Source = new BitmapImage(new Uri(logoPath, UriKind.Relative))
                };
                Logo.Child = img;
            }
            else
            {
                Log.Error(logoPath + " logo not found. Contact dev team.");
            }
        }

        //-----------------------------ASYNC LOADING----------------------------------//

        /// <summary>
        /// Loads a recent file from the recent files list asynchronously.
        /// </summary>
        /// 
        private async void LoadRecent_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoading) return;

            if (sender is Button clickedBtn)
            {
                string filePath = clickedBtn.Tag as string;

                if (File.Exists(filePath))
                {
                    try
                    {
                        _isLoading = true;
                        SetLoadingUIEnabled(false);

                        if (currentFile != null)
                            await InternalSaveAsync(currentFile);

                        string content = await File.ReadAllTextAsync(filePath);
                        EditingBoard.Text = content;
                        currentFile = filePath;

                        ShowFileName();
                        HighlightActiveFileButton();
                        _hasUnsavedChanges = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An unknown error has occured. See console or logs for full information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Log.Warning("An unknown error has occured. File removed from panel, error info logged.", ex);
                    }
                    finally
                    {
                        _isLoading = false;
                        SetLoadingUIEnabled(true);
                    }
                }
                else
                {
                    MessageBox.Show("An unknown error has occured. See console or logs for full information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Log.Warning("An unknown error has occured.");
                    Log.Error("Error info could not be written.");
                    Log.Information("Core recovered with no issues.");
                }
            }
        }


        /// <summary>
        /// Opens a file dialog and loads the selected file asynchronously.
        /// </summary>
        /// 
        public async void Load_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoading) return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "projects");
            if (Directory.Exists(path))
                openFileDialog.InitialDirectory = path;

            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _isLoading = true;
                    SetLoadingUIEnabled(false);

                    // Save current file before loading new one
                    if (recentFiles.Count > 0 && currentFile != null)
                        await InternalSaveAsync(currentFile);

                    currentFile = openFileDialog.FileName;

                    FileInfo fileInfo = new FileInfo(currentFile);
                    if (fileInfo.Length > 1048576) { Log.Warning($"File is too large: {fileInfo.Length}b (>1mb!)"); return; }//1mb
                    else { Log.Information($"File is loading... Size: {fileInfo.Length}b"); }

                    AddRecentFile(currentFile);

                    // Show loading prompt UI
                    fileNameHeader.Child = loadingPrompt;

                    // Load file content asynchronously
                    string content = await File.ReadAllTextAsync(currentFile);
                    EditingBoard.Text = content;
                    ShowFileName();
                    HighlightActiveFileButton();

                    New.IsHitTestVisible = true;
                    _hasUnsavedChanges = false;
                }
                catch (Exception ex)
                {
                    Log.Error($"Error loading file:\n{ex}");
                    this.Hide();
                    MessageBox.Show($"FATAL ERROR:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    _isLoading = false;
                    SetLoadingUIEnabled(true);
                    Log.Verbose($"Loading finished! Thread work halted.");
                }
            }
        }

        /// <summary>
        /// Enables or disables UI controls during loading/saving operations.
        /// </summary>
        private void SetLoadingUIEnabled(bool enabled)
        {
            Load.IsHitTestVisible = enabled;
            New.IsHitTestVisible = enabled;
            Save.IsHitTestVisible = enabled;
            SaveAs.IsHitTestVisible = enabled;

            foreach (var child in RecentFiles.Children)
            {
                if (child is Button btn)
                    btn.IsHitTestVisible = enabled;
            }
        }

        //------------------------------------ASYNC SAVING------------------------------------//

        /// <summary>
        /// Save the current text as a new file selected by user.
        /// </summary>
        private async void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            New.IsHitTestVisible = true;
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save As",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                await File.WriteAllTextAsync(filePath, EditingBoard.Text);
                currentFile = filePath;
                AddRecentFile(currentFile);
                ShowFileName();
                _hasUnsavedChanges = false;
            }
        }

        /// <summary>
        /// Save the current document, if a file path is known.
        /// </summary>
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile != null)
            {
                New.IsHitTestVisible = true;
                await File.WriteAllTextAsync(currentFile, EditingBoard.Text);
                _hasUnsavedChanges = false;
            }
            else
            {
                fileNameHeader.Child = useSaveAs;
            }
        }

        /// <summary>
        /// Internal save method used by autosave and manual saves.
        /// Will prompt Save As if needed.
        /// </summary>
        private async Task InternalSaveAsync(string filePath)
        {
            New.IsHitTestVisible = true;

            // If no path or empty text, skip saving
            if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrEmpty(EditingBoard.Text))
            {
                Log.Warning("Autosave attempted with null or empty filePath. Skipping.");
                return;
            }

            if (!File.Exists(filePath) && !ASEvent)
            {
                // Prompt Save As dialog if file doesn't exist and not autosave event
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Save As",
                    Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    filePath = saveFileDialog.FileName;
                    await File.WriteAllTextAsync(filePath, EditingBoard.Text);
                    currentFile = filePath;
                    AddRecentFile(currentFile);
                    ShowFileName();
                }
            }
            else if (File.Exists(filePath))
            {
                // Normal save to existing file
                await File.WriteAllTextAsync(filePath, EditingBoard.Text);
            }
            else
            {
                Log.Information("Skipping... AS Event was true");
            }

            ASEvent = false;
            _hasUnsavedChanges = false;
        }

        /// <summary>
        /// Handles the creation of a new file.
        /// If there are unsaved changes and a current file is set, performs an asynchronous autosave before clearing the editor.
        /// Resets editor state, disables the "New" button, clears the text content, and updates the displayed file name.
        /// </summary>
        private async void CreateNewFile_Click(object sender, RoutedEventArgs e)
        {
            if (_hasUnsavedChanges && currentFile != null)
            {
                await Task.Run(() => Dispatcher.Invoke(() =>  InternalSaveAsync(currentFile)));
                _hasUnsavedChanges = false;
            }

            New.IsHitTestVisible = false;
            EditingBoard.Text = "";
            currentFile = null;
            _hasUnsavedChanges = false;  // Reset flag since it's a fresh new document
            ShowFileName();
        }

        //----------------------------------ASYNC TRIGGERS------------------------------------//

        /// <summary>
        /// Periodic timer tick to autosave if there are unsaved changes.
        /// Runs asynchronously.
        /// </summary>
        private async void AsyncServiceTick(object sender, EventArgs e)
        {
            if (this.IsVisible && _hasUnsavedChanges)
            {
                ASEvent = true;
                await InternalSaveAsync(currentFile);
                _hasUnsavedChanges = false;
                Log.Information("Autosaved at " + DateTime.Now);
            }
        }

        /// <summary>
        /// Triggered when user modifies the text in the editor.
        /// Marks document as dirty and updates character count.
        /// </summary>
        private void EditingBoard_Trigger(object sender, TextChangedEventArgs e)
        {
            _hasUnsavedChanges = true;
            charCounter.Text = $"characters: {EditingBoard.Text.Length}";
        }
        private bool _isDistractionFree = false; // Track distraction-free mode state


        //---------------------------------UI SERVICES--------------------------------------//

        /// <summary>
        /// Toggles ZenMode, a mode of distraction-free writing.
        /// </summary>
        private void ZenMode_Click(object sender, RoutedEventArgs e)
        {
            _isDistractionFree = !_isDistractionFree;
            Log.Information(_isDistractionFree ? "Zen Mode: ON" : "Zen Mode: OFF");
            var fadeOutDuration = TimeSpan.FromMilliseconds(300);
            var fadeInDuration = TimeSpan.FromMilliseconds(300);

            UIElement[] elements = new UIElement[]
            {
                Logo, Exit, Save, SaveAs, Load, New, menuBorder,
                BorderFiles, RecentFiles, fileNameHeader, charCounter, borderFileName
            };

            if (_isDistractionFree)
            {
                zenOff.Background = (SolidColorBrush)Application.Current.Resources["AppButtonBrush"];
                var fadeOutStoryboard = new Storyboard();

                foreach (var element in elements)
                {
                    var fadeOut = new DoubleAnimation
                    {
                        From = 1,
                        To = 0,
                        Duration = new Duration(fadeOutDuration),
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                    };

                    Storyboard.SetTarget(fadeOut, element);
                    Storyboard.SetTargetProperty(fadeOut, new PropertyPath(UIElement.OpacityProperty));
                    fadeOutStoryboard.Children.Add(fadeOut);
                }

                fadeOutStoryboard.Completed += (s, _) =>
                {
                    foreach (var element in elements)
                    {
                        element.Visibility = Visibility.Collapsed;
                        element.Opacity = 1;
                    }

                    EditingBoard.FontWeight = FontWeights.Bold;
                    MainGrid.ColumnDefinitions[0].Width = new GridLength(0);
                    MainGrid.RowDefinitions[0].Height = new GridLength(0);
                    MainGrid.RowDefinitions[1].Height = new GridLength(0);
                    MainGrid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);

                    this.WindowStyle = WindowStyle.None;
                    this.WindowState = WindowState.Maximized;
                    this.ResizeMode = ResizeMode.NoResize;
                    this.Topmost = true;
                    
                    Application.Current.Resources["FontSize"] = (double)Application.Current.Resources["FontSize"] * 1.5;
                    if ((string)Application.Current.Resources["AppZenMode"] == "kitty")
                    {
                        EditingBoard.Background = (ImageBrush)Application.Current.Resources["KittyBackgroundBrush"];
                    }
                    else
                    {
                        EditingBoard.Background = (SolidColorBrush)Application.Current.Resources["AppBackgroundBrush"];
                    }
                };

                fadeOutStoryboard.Begin();
            }
            else
            {
                foreach (var element in elements)
                {
                    element.Visibility = Visibility.Visible;
                    element.Opacity = 0;
                }

                EditingBoard.Background = (SolidColorBrush)Application.Current.Resources["AppBackgroundBrush"];
                EditingBoard.FontWeight = FontWeights.Normal;
                MainGrid.ColumnDefinitions[0].Width = new GridLength(200);
                MainGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
                MainGrid.RowDefinitions[1].Height = new GridLength(0.6, GridUnitType.Star);
                MainGrid.RowDefinitions[2].Height = new GridLength(12, GridUnitType.Star);

                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Maximized;
                this.ResizeMode = ResizeMode.CanResize;
                this.Topmost = false;
                zenOff.Background = (SolidColorBrush)Application.Current.Resources["AppBackgroundBrush"];
                Application.Current.Resources["FontSize"] = (double)Application.Current.Resources["FontSize"] / 1.5;

                var fadeInStoryboard = new Storyboard();

                foreach (var element in elements)
                {
                    var fadeIn = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        Duration = new Duration(fadeInDuration),
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
                    };

                    Storyboard.SetTarget(fadeIn, element);
                    Storyboard.SetTargetProperty(fadeIn, new PropertyPath(UIElement.OpacityProperty));
                    fadeInStoryboard.Children.Add(fadeIn);
                }

                fadeInStoryboard.Begin();
            }
        }

        /// <summary>
        /// Updates the file name display label.
        /// </summary>
        private void ShowFileName()
        {
            TextBlock tb = new TextBlock();

            if (string.IsNullOrEmpty(currentFile))
            {
                tb.Text = CONST.UI.NO_FILE_LOADED;
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

        /// <summary>
        /// Updates the recent files UI buttons with the current list.
        /// </summary>
        void AddRecentFile(string filePath)
        {
            filePath = Path.GetFullPath(filePath);
            recentFiles.Insert(0, filePath);

            // Remove duplicates and keep max 5 recent files
            recentFiles = recentFiles.Distinct().Take(5).ToList();

            RecentFiles.Children.Clear();

            if (recentFiles.Count == 0)
            {
                RecentFiles.Children.Add(noRecentFiles);
                return;
            }

            foreach (string file in recentFiles)
            {
                Button btn = new Button
                {
                    Content = Path.GetFileName(file),
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


        /// <summary>
        /// Highlights the button corresponding to the currently active file.
        /// </summary>
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

        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            this.Close();
        }
    }
}
    