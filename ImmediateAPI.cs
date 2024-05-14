using System.Runtime.InteropServices;

namespace TestInteropWpf;

public readonly partial struct ImmediateAPI : IDisposable
{
    private readonly IntPtr handle;

    public bool IsInitialized => handle != IntPtr.Zero;

    public ImmediateAPI(IntPtr hWnd)
    {
        handle = API.Init(hWnd);
    }

    public void Render()
    {
        API.Render(handle);
    }

    public void Resize(int width, int height)
    {
        API.Resize(handle, width, height);
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
        public static partial void Render(IntPtr handle);

        [LibraryImport("ImmediateRendering.dll")]
        public static partial void Release(IntPtr handle);

        [LibraryImport("ImmediateRendering.dll")]
        public static partial void Resize(IntPtr handle, int width, int height);
    }
}