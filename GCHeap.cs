using System.Runtime.InteropServices;


namespace WalkHeap;
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct GCHeap
{
    public IntPtr vtable;

    public gc_heap* pGenGCHeap;
}

[StructLayout(LayoutKind.Explicit)]
internal unsafe struct gc_heap
{
    //public GCHeap* vm_heap;
    //public int heap_number;
    [FieldOffset(0)]
    public IntPtr gc_done_event;

    [FieldOffset(8)]
    public int gc_done_event_lock;

    [FieldOffset(12)]
    public bool gc_done_event_set;

    [FieldOffset(16)]
    public int condemned_generation_num;

    [FieldOffset(20)]
    public bool blocking_collection;

    [FieldOffset(24)]
    public int elevation_requested;

    [FieldOffset(32)]
    public fixed byte mark_queue[136];

    [FieldOffset(168)]
    public int gc_policy;

    [FieldOffset(176)]
    public nint total_promoted_bytes;

    [FieldOffset(184)]
    public nint finalization_promoted_bytes;

    [FieldOffset(192)]
    public nint mark_stack_tos;

    [FieldOffset(200)]
    public nint mark_stack_bos;

    [FieldOffset(208)]
    public IntPtr oldest_pinned_plug;

    [FieldOffset(216)]
    public IntPtr mark_list;

    [FieldOffset(224)]
    public IntPtr mark_list_end;

    [FieldOffset(232)]
    public IntPtr mark_list_index;

    [FieldOffset(240)]
    public IntPtr mark_list_piece_start;

    [FieldOffset(248)]
    public IntPtr mark_list_piece_end;

    [FieldOffset(256)]
    public IntPtr min_overflow_address;

    [FieldOffset(264)]
    public IntPtr max_overflow_address;

    [FieldOffset(272)]
    public nint alloc_contexts_used;

    [FieldOffset(280)]
    public int sufficient_gen0_space_p;

    [FieldOffset(284)]
    public int ro_segments_in_range;

    [FieldOffset(288)]
    public bool no_gc_oom_p;

    [FieldOffset(296)]
    public IntPtr saved_loh_segment_no_gc;

    [FieldOffset(304)]
    public IntPtr min_fl_list;

    [FieldOffset(312)]
    public nint num_fl_items_rethreaded_stage2;

    [FieldOffset(320)]
    public IntPtr free_list_space_per_heap;

    [FieldOffset(328)]
    public int current_bgc_state;

    [FieldOffset(336)]
    public nint bgc_begin_loh_size;

    [FieldOffset(344)]
    public nint bgc_begin_poh_size;

    [FieldOffset(352)]
    public nint end_loh_size;

    [FieldOffset(360)]
    public nint end_poh_size;

    [FieldOffset(368)]
    public int processed_eph_overflow_p;

    [FieldOffset(376)]
    public nint c_mark_list_index;

    [FieldOffset(384)]
    public IntPtr next_sweep_obj;

    [FieldOffset(392)]
    public IntPtr current_sweep_pos;

    [FieldOffset(400)]
    public IntPtr current_sweep_seg;

    [FieldOffset(408)]
    public int background_overflow_p;

    [FieldOffset(416)]
    public fixed byte background_written_addresses[816];

    [FieldOffset(1232)]
    public ulong loh_a_no_bgc;

    [FieldOffset(1240)]
    public ulong loh_a_bgc_marking;

    [FieldOffset(1248)]
    public ulong loh_a_bgc_planning;

    [FieldOffset(1256)]
    public nint bgc_maxgen_end_fl_size;

    [FieldOffset(1264)]
    public int num_regions_freed_in_sweep;

    [FieldOffset(1268)]
    public fixed int sip_maxgen_regions_per_gen[3];

    [FieldOffset(1280)]
    public nint* reserved_free_regions_sip1;

    [FieldOffset(1288)]
    public nint* reserved_free_regions_sip2;

    [FieldOffset(1296)]
    public fixed int regions_per_gen[3];

    [FieldOffset(1308)]
    public fixed int planned_regions_per_gen[3];

    [FieldOffset(1320)]
    public nint end_gen0_region_space;

    [FieldOffset(1328)]
    public nint end_gen0_region_committed_space;

    [FieldOffset(1336)]
    public nint gen0_pinned_free_space;

    [FieldOffset(1344)]
    public bool gen0_large_chunk_found;

    [FieldOffset(1352)]
    public IntPtr survived_per_region;

    [FieldOffset(1360)]
    public IntPtr old_card_survived_per_region;

    [FieldOffset(1368)]
    public bool special_sweep_p;

    [FieldOffset(1372)]
    public uint card_mark_chunk_index_soh;

    [FieldOffset(1376)]
    public bool card_mark_done_soh;

    [FieldOffset(1380)]
    public uint card_mark_chunk_index_loh;

    [FieldOffset(1384)]
    public uint card_mark_chunk_index_poh;

    [FieldOffset(1388)]
    public bool card_mark_done_uoh;

    [FieldOffset(1392)]
    public nint n_eph_soh;

    [FieldOffset(1400)]
    public nint n_gen_soh;

    [FieldOffset(1408)]
    public nint n_eph_loh;

    [FieldOffset(1416)]
    public nint n_gen_loh;

    [FieldOffset(1424)]
    public nint gen2_removed_no_undo;

    [FieldOffset(1432)]
    public nint saved_pinned_plug_index;

    [FieldOffset(1440)]
    public nint loh_pinned_queue_tos;

    [FieldOffset(1448)]
    public nint loh_pinned_queue_bos;

    [FieldOffset(1456)]
    public int loh_compacted_p;

    [FieldOffset(1464)]
    public nint allocation_quantum;

    [FieldOffset(1472)]
    public fixed byte dynamic_data_table[840];

    [FieldOffset(2312)]
    public ulong loh_alloc_since_cg;

    [FieldOffset(2316)]
    public int last_gc_before_oom;

    [FieldOffset(2320)]
    public int alloc_context_count;

    [FieldOffset(2328)]
    public bool gen0_allocated_after_gc_p;

    [FieldOffset(2336)]
    public nint bgc_loh_size_increased;

    [FieldOffset(2344)]
    public nint bgc_poh_size_increased;

    [FieldOffset(2352)]
    public nint background_soh_alloc_count;

    [FieldOffset(2360)]
    public nint background_uoh_alloc_count;

    [FieldOffset(2368)]
    public int uoh_alloc_thread_count;

    [FieldOffset(2376)]
    public generation generation0;

    [FieldOffset(2640)]
    public generation generation1;

    [FieldOffset(2904)]
    public generation generation2;

    [FieldOffset(3168)]
    public generation generation3;

    [FieldOffset(3432)]
    public generation generation4;

    [FieldOffset(3696)]
    public nint mark_stack_array_length;

    [FieldOffset(5568)]
    public IntPtr vm_heap;

    [FieldOffset(5576)]
    public int heap_number;
}
