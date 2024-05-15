using System.Windows;

namespace TestInteropWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var control = new ImmediateControl();
            control.Update += dt => FPS.Content = (1 / dt).ToString("F2");

            control.Width = 900;
            Root.Children.Add(control);
        }
    }
}