using System.Runtime.InteropServices;

namespace WalkHeap;

[StructLayout(LayoutKind.Sequential)]
unsafe struct WaitEventLink
{
    public nint m_WaitSB;
    public nint m_EventWait;
    public nint m_Thread;
    public nint m_Next;
    public nint m_LinkSB;
    public int m_RefCount;
}