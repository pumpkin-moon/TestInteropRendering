using System.Windows;
using System.Windows.Interop;

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

            var helper = new WindowInteropHelper(this);
            IntPtr hWnd = helper.EnsureHandle();

            var control = new ImmediateControl(hWnd);
            Root.Children.Add(control);
        }
    }
}