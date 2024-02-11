using System.Runtime.InteropServices;

namespace WalkHeap;

[StructLayout(LayoutKind.Explicit, Size = 264)]
internal unsafe struct generation
{
    [FieldOffset(0)]
    public fixed byte allocation_context[56];

    [FieldOffset(56)]
    public heap_segment* start_segment;

    [FieldOffset(64)]
    public heap_segment* allocation_segment;

    [FieldOffset(72)]
    public IntPtr allocation_context_start_region;

    [FieldOffset(80)]
    public heap_segment* tail_region;

    [FieldOffset(88)]
    public heap_segment* plan_start_segment;

    [FieldOffset(96)]
    public heap_segment* tail_ro_region;

    [FieldOffset(104)]
    public fixed byte free_list_allocator[64];

    [FieldOffset(168)]
    public nint free_list_allocated;

    [FieldOffset(176)]
    public nint end_seg_allocated;

    [FieldOffset(184)]
    public nint condemned_allocated;

    [FieldOffset(192)]
    public nint sweep_allocated;

    [FieldOffset(200)]
    public int allocate_end_seg_p;

    [FieldOffset(208)]
    public nint free_list_space;

    [FieldOffset(216)]
    public nint free_obj_space;

    [FieldOffset(224)]
    public nint allocation_size;

    [FieldOffset(232)]
    public nint pinned_allocation_compact_size;

    [FieldOffset(240)]
    public nint pinned_allocation_sweep_size;

    [FieldOffset(248)]
    public int gen_num;

    [FieldOffset(252)]
    public int set_bgc_mark_bit_p;

    [FieldOffset(256)]
    public IntPtr last_free_list_allocated;
}
