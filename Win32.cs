using System.Runtime.InteropServices;

namespace TestInteropWpf
{
    public static class Win32
    {
        public const int WS_CHILD = 0x40000000;
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_CLIPCHILDREN = 0x02000000;

        public const int WM_ACTIVATE = 0x0006;
        public static readonly IntPtr WA_ACTIVE = new IntPtr(1);
        public static readonly IntPtr WA_INACTIVE = new IntPtr(0);

        public record struct RectL(int Left, int Top, int Right, int Bottom);

        [DllImport("user32.dll")]
        public static extern IntPtr CreateWindowEx(
            int dwExStyle,
            string lpClassName,
            string lpWindowName,
            int dwStyle,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            object lpParam);

        [DllImport("user32.dll")]
        public static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RectL lpRect);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public enum Events
        {
            WM_MOVING = 0x0216
        }
    }
}