using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace antWriter
{
    public partial class RespButton : UserControl
    {
        public RespButton() => InitializeComponent();

        //Background
        public static readonly DependencyProperty BtnBgProperty =
            DependencyProperty.Register(nameof(BtnBg), typeof(Brush), typeof(RespButton), new PropertyMetadata(Brushes.Aqua));
        public Brush BtnBg
        {
            get => (Brush)GetValue(BtnBgProperty);
            set => SetValue(BtnBgProperty, value);
        }

        //Content
        public static readonly DependencyProperty BtnTextProperty =
            DependencyProperty.Register(nameof(BtnText), typeof(string), typeof(RespButton), new PropertyMetadata("Placeholder"));
        public string BtnText
        {
            get => (string)GetValue(BtnTextProperty);
            set => SetValue(BtnTextProperty, value);
        }

        //Element Radius
        public static readonly DependencyProperty BtnRadProperty =
            DependencyProperty.Register(nameof(BtnRad), typeof(CornerRadius), typeof(RespButton), new PropertyMetadata(new CornerRadius(10)));

        public CornerRadius BtnRad
        {
            get => (CornerRadius)GetValue(BtnRadProperty);
            set => SetValue(BtnRadProperty, value);
        }
    }
}
