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
            // control.Width = 400;
            Root.Children.Add(control);
        }
    }
}