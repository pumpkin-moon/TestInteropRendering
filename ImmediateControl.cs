using System.Windows;
using System.Windows.Input;
using Gemini.Framework.Controls;

namespace TestInteropWpf;

public sealed class ImmediateControl : HwndWrapper
{
    private ImmediateAPI api;

    protected override void Render(IntPtr windowHandle)
    {
        if (!api.IsInitialized)
        {
            api = new(windowHandle);
        }

        api.Render();
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