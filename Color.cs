namespace TestInteropWpf;

public record struct Color(byte R, byte G, byte B, byte A = 0xFF)
{
    public Color(uint color) : this(
        (byte)((color >> 0) & 0xFF),
        (byte)((color >> 8) & 0xFF),
        (byte)((color >> 16) & 0xFF),
        (byte)((color >> 24) & 0xFF))
    {
    }

    public Color(uint color, byte alpha) : this(
        alpha,
        (byte)((color >> 8) & 0xFF),
        (byte)((color >> 16) & 0xFF),
        (byte)((color >> 24) & 0xFF))
    {
    }
};