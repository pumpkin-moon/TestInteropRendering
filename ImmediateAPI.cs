using System.Runtime.InteropServices;

namespace TestInteropWpf;

public readonly partial struct ImmediateAPI : IDisposable
{
    private readonly IntPtr handle;

    public ImmediateAPI(IntPtr hWnd)
    {
        handle = API.Init(hWnd);
    }

    public void Render()
    {
        API.Render(handle);
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
    }
}