using System.Runtime.InteropServices;
using TestInteropWpf.Data;

namespace TestInteropWpf;

public readonly partial struct ImmediateAPI : IDisposable
{
    private readonly IntPtr handle;

    public bool IsInitialized => handle != IntPtr.Zero;

    public ImmediateAPI(IntPtr hWnd)
    {
        handle = API.Init(hWnd);
    }

    public void BeginFrame()
    {
        API.BeginFrame(handle);
    }

    public void EndFrame()
    {
        API.EndFrame(handle);
    }

    public void Resize(int width, int height)
    {
        API.Resize(handle, width, height);
    }

    public void DrawCircle(Circle circle)
    {
        API.DrawCircle(handle, circle);
    }
    
    public void DrawLine(Line line)
    {
        API.DrawLine(handle, line);
    }

    public void Dispose()
    {
        API.Release(handle);
    }

    private static partial class API
    {
        [LibraryImport("ImmediateRendering.dll")]
        public static partial IntPtr Init(IntPtr hWnd);

        [LibraryImport("ImmediateRendering.dll")]
        public static partial void BeginFrame(IntPtr handle);

        [LibraryImport("ImmediateRendering.dll")]
        public static partial void EndFrame(IntPtr handle);

        [LibraryImport("ImmediateRendering.dll")]
        public static partial void Release(IntPtr handle);

        [LibraryImport("ImmediateRendering.dll")]
        public static partial void Resize(IntPtr handle, int width, int height);

        [LibraryImport("ImmediateRendering.dll")]
        public static partial void DrawCircle(IntPtr handle, Circle circle);
        
        [LibraryImport("ImmediateRendering.dll")]
        public static partial void DrawLine(IntPtr handle, Line line);
    }
}