using System.Diagnostics;
using System.Windows;
using Gemini.Framework.Controls;
using TestInteropWpf.Data;

namespace TestInteropWpf;

public sealed class ImmediateControl : HwndWrapper
{
    private ImmediateAPI api;

    private TimeSpan totalTime;
    private TimeSpan deltaTime;

    private readonly Stopwatch stopwatch = Stopwatch.StartNew();

    public float DeltaTime => (float)deltaTime.TotalSeconds;

    public event Action<float> Update;

    protected override void Render(IntPtr windowHandle)
    {
        var newTime = stopwatch.Elapsed;
        deltaTime = newTime - totalTime;
        totalTime = newTime;

        if (!api.IsInitialized)
        {
            api = new(windowHandle);
        }

        Update?.Invoke(DeltaTime);

        api.BeginFrame();

        var rnd = Random.Shared;

        int width = (int)ActualWidth;
        int height = (int)ActualHeight;

        // for (int i = 0; i < 100_000; ++i)
        // {
        //     var circle = new Circle
        //     {
        //         X = rnd.NextSingle() * width,
        //         Y = rnd.NextSingle() * height,
        //         Radius = rnd.NextSingle() * 20 + 5,
        //         Thickness = rnd.NextSingle() * 4 + 1,
        //         Color = 0xFF000000 | (uint)rnd.Next(),
        //     };
        //
        //     api.DrawCircle(circle);
        // }
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

        api.EndFrame();
    }

    protected override void Dispose(bool disposing)
    {
        if (api.IsInitialized)
        {
            api.Dispose();
        }

        base.Dispose(disposing);
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);

        if (api.IsInitialized)
        {
            api.Resize((int)sizeInfo.NewSize.Width, (int)sizeInfo.NewSize.Height);
        }
    }
}