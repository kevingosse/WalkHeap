using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WalkHeap;

[StructLayout(LayoutKind.Explicit)]
internal unsafe struct MethodTable
{
    /// <summary>
    /// The low WORD of the first field is the component size for array and string types.
    /// </summary>
    [FieldOffset(0)]
    public ushort ComponentSize;

    /// <summary>
    /// The flags for the current method table (only for not array or string types).
    /// </summary>
    [FieldOffset(0)]
    private uint Flags;

    /// <summary>
    /// The base size of the type (used when allocating an instance on the heap).
    /// </summary>
    [FieldOffset(4)]
    public int BaseSize;

    // See additional native members in methodtable.h, not needed here yet.
    // 0x8: m_dwFlags2 (additional flags and token in upper 24 bits)
    // 0xC: m_wNumVirtuals

    /// <summary>
    /// The number of interfaces implemented by the current type.
    /// </summary>
    [FieldOffset(0x0E)]
    public ushort InterfaceCount;

    // For DEBUG builds, there is a conditional field here (see methodtable.h again).
    // 0x10: debug_m_szClassName (display name of the class, for the debugger)

    /// <summary>
    /// A pointer to the parent method table for the current one.
    /// </summary>
    [FieldOffset(ParentMethodTableOffset)]
    public MethodTable* ParentMethodTable;

    // Additional conditional fields (see methodtable.h).
    // m_pModule
    // m_pAuxiliaryData
    // union {
    //   m_pEEClass (pointer to the EE class)
    //   m_pCanonMT (pointer to the canonical method table)
    // }

    /// <summary>
    /// This element type handle is in a union with additional info or a pointer to the interface map.
    /// Which one is used is based on the specific method table being in used (so this field is not
    /// always guaranteed to actually be a pointer to a type handle for the element type of this type).
    /// </summary>
    [FieldOffset(ElementTypeOffset)]
    public void* ElementType;

    /// <summary>
    /// This interface map used to list out the set of interfaces. Only meaningful if InterfaceCount is non-zero.
    /// </summary>
    [FieldOffset(InterfaceMapOffset)]
    public MethodTable** InterfaceMap;

    // WFLAGS_LOW_ENUM
    private const uint enum_flag_GenericsMask = 0x00000030;
    private const uint enum_flag_GenericsMask_NonGeneric = 0x00000000; // no instantiation
    private const uint enum_flag_GenericsMask_GenericInst = 0x00000010; // regular instantiation, e.g. List<String>
    private const uint enum_flag_GenericsMask_SharedInst = 0x00000020; // shared instantiation, e.g. List<__Canon> or List<MyValueType<__Canon>>
    private const uint enum_flag_GenericsMask_TypicalInst = 0x00000030; // the type instantiated at its formal parameters, e.g. List<T>
    private const uint enum_flag_HasDefaultCtor = 0x00000200;
    private const uint enum_flag_IsByRefLike = 0x00001000;

    // WFLAGS_HIGH_ENUM
    private const uint enum_flag_ContainsPointers = 0x01000000;
    private const uint enum_flag_HasComponentSize = 0x80000000;
    private const uint enum_flag_HasTypeEquivalence = 0x02000000;
    private const uint enum_flag_Category_Mask = 0x000F0000;
    private const uint enum_flag_Category_ValueType = 0x00040000;
    private const uint enum_flag_Category_Nullable = 0x00050000;
    private const uint enum_flag_Category_PrimitiveValueType = 0x00060000; // sub-category of ValueType, Enum or primitive value type
    private const uint enum_flag_Category_TruePrimitive = 0x00070000; // sub-category of ValueType, Primitive (ELEMENT_TYPE_I, etc.)
    private const uint enum_flag_Category_ValueType_Mask = 0x000C0000;
    private const uint enum_flag_Category_Interface = 0x000C0000;
    // Types that require non-trivial interface cast have this bit set in the category
    private const uint enum_flag_NonTrivialInterfaceCast = 0x00080000 // enum_flag_Category_Array
                                                         | 0x40000000 // enum_flag_ComObject
                                                         | 0x00400000 // enum_flag_ICastable;
                                                         | 0x10000000 // enum_flag_IDynamicInterfaceCastable;
                                                         | 0x00040000; // enum_flag_Category_ValueType

    private const int DebugClassNamePtr = // adjust for debug_m_szClassName
#if DEBUG
#if TARGET_64BIT
            8
#else
        4
#endif
#else
            0
#endif
        ;

    private const int ParentMethodTableOffset = 0x10 + DebugClassNamePtr;

#if TARGET_64BIT
        private const int ElementTypeOffset = 0x30 + DebugClassNamePtr;
#else
    private const int ElementTypeOffset = 0x20 + DebugClassNamePtr;
#endif

#if TARGET_64BIT
        private const int InterfaceMapOffset = 0x38 + DebugClassNamePtr;
#else
    private const int InterfaceMapOffset = 0x24 + DebugClassNamePtr;
#endif

    public bool HasComponentSize => (Flags & enum_flag_HasComponentSize) != 0;

    public bool ContainsGCPointers => (Flags & enum_flag_ContainsPointers) != 0;

    public bool NonTrivialInterfaceCast => (Flags & enum_flag_NonTrivialInterfaceCast) != 0;

    public bool HasTypeEquivalence => (Flags & enum_flag_HasTypeEquivalence) != 0;

    internal static bool AreSameType(MethodTable* mt1, MethodTable* mt2) => mt1 == mt2;

    public bool HasDefaultConstructor => (Flags & (enum_flag_HasComponentSize | enum_flag_HasDefaultCtor)) == enum_flag_HasDefaultCtor;

    public bool IsMultiDimensionalArray
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            // See comment on RawArrayData for details
            return BaseSize > (uint)(3 * sizeof(IntPtr));
        }
    }

    // Returns rank of multi-dimensional array rank, 0 for sz arrays
    public int MultiDimensionalArrayRank
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            // See comment on RawArrayData for details
            return (int)((BaseSize - (uint)(3 * sizeof(IntPtr))) / (uint)(2 * sizeof(int)));
        }
    }

    public bool IsInterface => (Flags & enum_flag_Category_Mask) == enum_flag_Category_Interface;

    public bool IsValueType => (Flags & enum_flag_Category_ValueType_Mask) == enum_flag_Category_ValueType;

    public bool IsNullable => (Flags & enum_flag_Category_Mask) == enum_flag_Category_Nullable;

    public bool IsByRefLike => (Flags & (enum_flag_HasComponentSize | enum_flag_IsByRefLike)) == enum_flag_IsByRefLike;

    // Warning! UNLIKE the similarly named Reflection api, this method also returns "true" for Enums.
    public bool IsPrimitive => (Flags & enum_flag_Category_Mask) is enum_flag_Category_PrimitiveValueType or enum_flag_Category_TruePrimitive;

    public bool HasInstantiation => (Flags & enum_flag_HasComponentSize) == 0 && (Flags & enum_flag_GenericsMask) != enum_flag_GenericsMask_NonGeneric;

    public bool IsGenericTypeDefinition => (Flags & (enum_flag_HasComponentSize | enum_flag_GenericsMask)) == enum_flag_GenericsMask_TypicalInst;

    public bool IsConstructedGenericType
    {
        get
        {
            uint genericsFlags = Flags & (enum_flag_HasComponentSize | enum_flag_GenericsMask);
            return genericsFlags == enum_flag_GenericsMask_GenericInst || genericsFlags == enum_flag_GenericsMask_SharedInst;
        }
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern uint GetNumInstanceFieldBytes();
}
