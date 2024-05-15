using System.Windows;
using TestInteropWpf.Shapes;

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

            var control = new ImmediateControl(new(0, 0, 0));
            control.Update += (api, dt) =>
            {
                var rnd = Random.Shared;

                int width = (int)control.ActualWidth;
                int height = (int)control.ActualHeight;

                for (int i = 0; i < 400_000; ++i)
                {
                    var line = new Line
                    {
                        X0 = rnd.NextSingle() * width,
                        Y0 = rnd.NextSingle() * height,
                        X1 = rnd.NextSingle() * width,
                        Y1 = rnd.NextSingle() * height,
                        Thickness = 1,
                        Color = 0xFF000000 | (uint)rnd.Next()
                    };

                    api.DrawLine(line);
                }

                FPS.Content = (1 / dt).ToString("F2");
            };

            control.Width = 900;
            Root.Children.Add(control);
        }
    }
}