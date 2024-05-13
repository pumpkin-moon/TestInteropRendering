using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace TestInteropWpf;

public sealed class ImmediateControl : Grid
{
    private readonly D3DImage image = new D3DImage();

    private readonly IntPtr hWnd;
    private ImmediateAPI api;

    private TimeSpan totalTime;

    public ImmediateControl(IntPtr hWnd)
    {
        this.hWnd = hWnd;
        image.IsFrontBufferAvailableChanged += OnIsFrontBufferAvailableChanged;
        Background = new ImageBrush(image);

        BeginRender();
    }

    private void OnIsFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (image.IsFrontBufferAvailable)
        {
            BeginRender();
        }
        else
        {
            EndRender();
        }
    }

    private void BeginRender()
    {
        if (!image.IsFrontBufferAvailable)
        {
            return;
        }

        api = new(hWnd);

        // image.Lock();
        // image.SetBackBuffer(D3DResourceType.IDirect3DSurface9, api.GetBackBuffer());
        // image.Unlock();

        CompositionTarget.Rendering += OnRendering;
    }

    private void EndRender()
    {
        CompositionTarget.Rendering -= OnRendering;
        api.Dispose();
    }

    private void OnRendering(object sender, EventArgs e)
    {
        var args = (RenderingEventArgs)e;

        if (totalTime == args.RenderingTime)
        {
            return;
        }

        totalTime = args.RenderingTime;
        Render();
    }

    private void Render()
    {
        if (!image.IsFrontBufferAvailable)
        {
            return;
        }

        // image.Lock();
        api.Render();
        // image.Unlock();
    }
}