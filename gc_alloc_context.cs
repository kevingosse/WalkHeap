using System.Runtime.InteropServices;
using System.Text;

namespace WalkHeap;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct gc_alloc_context
{
    public nint alloc_ptr;
    public nint alloc_limit;
    public long alloc_bytes;
    public long alloc_bytes_uoh;
    public GCHeap* gc_reserved_1;
    public GCHeap* gc_reserved_2;
    public int alloc_count;

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine();
        sb.AppendFormat("\talloc_ptr: {0:x2}\r\n", (nint)alloc_ptr);
        sb.AppendFormat("\talloc_limit: {0:x2}\r\n", (nint)alloc_limit);
        sb.AppendFormat("\talloc_bytes: {0:x2}\r\n", alloc_bytes);
        sb.AppendFormat("\talloc_bytes_uoh: {0:x2}\r\n", alloc_bytes_uoh);
        sb.AppendFormat("\tgc_reserved_1: {0:x2}\r\n", (nint)gc_reserved_1);
        sb.AppendFormat("\tgc_reserved_2: {0:x2}\r\n", (nint)gc_reserved_2);
        sb.AppendFormat("\talloc_count: {0:x2}", alloc_count);
        return sb.ToString();
    }
}
