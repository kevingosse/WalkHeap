using System.Runtime.InteropServices;

namespace WalkHeap;

[StructLayout(LayoutKind.Sequential)]
unsafe struct CLREvent
{
    public nint m_handle;
    public nint m_dwFlags;
}