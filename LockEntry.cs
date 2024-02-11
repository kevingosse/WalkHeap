using System.Runtime.InteropServices;

namespace WalkHeap;

[StructLayout(LayoutKind.Sequential)]
unsafe struct LockEntry
{
    public nint* pNext;
    public nint* pPrev;
    public int dwULockID;
    public int dwLLockID;
    public short wReaderLevel;
}
