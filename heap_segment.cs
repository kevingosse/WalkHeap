using System.Runtime.InteropServices;

namespace WalkHeap;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal unsafe struct heap_segment
{
    public enum RegionFlags : long
    {
        Readonly = 1,
        Inrange = 2,
        Loh = 8,
        Swept = 16,
        Decommitted = 32,
        Committed = 64,
        PartiallyCommitted = 128,
        Uoh_delete = 256,
        Poh = 512
    }

    public nint allocated;
    public byte* committed;
    // For regions This could be obtained from region_allocator as each
    // busy block knows its size.
    public byte* reserved;
    public byte* used;
    // For regions this is the actual physical start + aligned_plug_and_gap.
    public byte* mem;
    // Currently we are using 12 bits for flags, see "flags description" right above.
    public RegionFlags flags;
    public heap_segment* next;
    public byte* background_allocated;
#if false
    gc_heap* heap;
#endif //MULTIPLE_HEAPS
    public byte* decommit_target;
    public byte* plan_allocated;
    // In the plan phase we change the allocated for a seg but we need this
    // value to correctly calculate how much space we can reclaim in
    // generation_fragmentation. But it's beneficial to truncate it as it
    // means in the later phases we only need to look up to the new allocated.
    public byte* saved_allocated;
    public byte* saved_bg_allocated;

    public nint survived;
    // These generation numbers are initialized to -1.
    // For plan_gen_num:
    // for all regions in condemned generations it needs
    // to be re-initialized to -1 when a GC is done.
    // When setting it we update the demotion decision accordingly.
    public byte gen_num;
    // This says this region was already swept during plan and its bricks
    // were built to indicates objects, ie, not the way plan builds them.
    // Other phases need to be aware of this so they don't assume bricks
    // indicate tree nodes.
    //
    // swept_in_plan_p can be folded into gen_num.
    public byte swept_in_plan_p;
    public byte padding1;
    public byte padding2;
    public int plan_gen_num;
    public int old_card_survived;
    public int pinned_survived;
    // at the end of each GC, we increase each region in the region free list
    // by 1. So we can observe if a region stays in the free list over many
    // GCs. We stop at 99. It's initialized to 0 when a region is added to
    // the region's free list.

    public int age_in_free;
    public int padding3;
    // This is currently only used by regions that are swept in plan -
    // we then thread this list onto the generation's free list.
    // We may keep per region free list later which requires more work.
    public byte* free_list_head;
    public byte* free_list_tail;
    public nint free_list_size;
    public nint free_obj_size;

    public heap_segment* prev_free_region;
    public IntPtr containing_free_list;
}
