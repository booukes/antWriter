using Serilog;
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
    /// <summary>
    /// Interaction logic for AIWindow.xaml
    /// </summary>
    public partial class AIWindow : Window
    {
        public AIWindow()
        {
            InitializeComponent();
        }

        public async void RunAI_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Log.Information("Running AI with prompt: {Prompt}", prompt.Text);
                string airesult = await ((App)Application.Current).RunOllamaPromptAsync(
                    prompt.Text
                );
                Log.Information("AI result: {Result}", airesult);
                TextBlock tb = new TextBlock();

                tb.TextWrapping = TextWrapping.Wrap;
                tb.Margin = new Thickness(2);
                tb.Foreground = (SolidColorBrush)Application.Current.Resources["AppFontColor"];
                tb.HorizontalAlignment = HorizontalAlignment.Left;
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.Text = airesult;

                result.Content = tb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

    }
}
