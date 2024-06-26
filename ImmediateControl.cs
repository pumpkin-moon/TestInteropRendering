﻿using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace TestInteropWpf;

public sealed class ImmediateControl : HwndHost
{
    private readonly Color clearColor;
    private ImmediateAPI api;

    private TimeSpan totalTime;
    private TimeSpan deltaTime;

    public event Action<ImmediateAPI, float> Update;

    public ImmediateControl(Color clearColor)
    {
        this.clearColor = clearColor;
    }

    private void OnRender(object sender, EventArgs e)
    {
        var args = (RenderingEventArgs)e;
        if (totalTime == args.RenderingTime)
        {
            return;
        }

        deltaTime = args.RenderingTime - totalTime;
        totalTime = args.RenderingTime;

        Render();
    }

    private void Render()
    {
        api.BeginFrame(clearColor);

        Update?.Invoke(api, (float)deltaTime.TotalSeconds);

        api.EndFrame();
    }

    protected override HandleRef BuildWindowCore(HandleRef hWndParent)
    {
        var handle = Win32.CreateWindowEx(0,
            "static",
            "",
            Win32.WS_VISIBLE | Win32.WS_CHILD,
            0,
            0,
            200,
            200,
            hWndParent.Handle,
            IntPtr.Zero,
            IntPtr.Zero,
            IntPtr.Zero);

        api = new(handle);
        CompositionTarget.Rendering += OnRender;

        return new(this, handle);
    }

    protected override void DestroyWindowCore(HandleRef hwnd)
    {
        CompositionTarget.Rendering -= OnRender;
        Win32.DestroyWindow(hwnd.Handle);
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
        api.Resize((int)sizeInfo.NewSize.Width, (int)sizeInfo.NewSize.Height);
    }
}