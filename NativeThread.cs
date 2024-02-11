using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WalkHeap;

[StructLayout(LayoutKind.Explicit)]
internal unsafe struct NativeThread
{
    public static ref NativeThread GetCurrentNativeThread()
    {
        return ref Unsafe.AsRef<NativeThread>((void*)GetNativeThread(Thread.CurrentThread));
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_DONT_USE_InternalThread")]
    static extern ref IntPtr GetNativeThread(Thread thread);

    [FieldOffset(0)]
    public nint m_stackLocalAllocator;

    [FieldOffset(8)]
    public int m_state;

    [FieldOffset(12)]
    public byte m_fPreemptiveGCDisabled;

    [FieldOffset(16)]
    public nint m_pFrame;

    [FieldOffset(24)]
    public nint m_pDomain;

    [FieldOffset(32)]
    public int m_ThreadId;

    [FieldOffset(40)]
    public nint m_pHead;

    [FieldOffset(48)]
    public LockEntry m_embeddedEntry;

    [FieldOffset(80)]
    public nint m_pBlockingLock;

    [FieldOffset(88)]
    public gc_alloc_context m_alloc_context;

    [FieldOffset(144)]
    public nint m_thAllocContextObj;

    [FieldOffset(152)]
    public nint m_pTEB;

    [FieldOffset(160)]
    public nint m_pRCWStack;

    [FieldOffset(168)]
    public int m_ThreadTasks;

    [FieldOffset(172)]
    public int m_StateNC;

    [FieldOffset(176)]
    public int m_dwForbidSuspendThread;

    [FieldOffset(180)]
    public int m_dwHashCodeSeed;

    [FieldOffset(184)]
    public nint m_pLoadLimiter;

    [FieldOffset(192)]
    public int m_AbortType;

    [FieldOffset(200)]
    public ulong m_AbortEndTime;

    [FieldOffset(208)]
    public ulong m_RudeAbortEndTime;

    [FieldOffset(216)]
    public int m_fRudeAbortInitiated;

    [FieldOffset(220)]
    public int m_AbortController;

    [FieldOffset(224)]
    public int m_AbortRequestLock;

    [FieldOffset(228)]
    public int m_ThrewControlForThread;

    [FieldOffset(232)]
    public nint m_OSContext;

    [FieldOffset(240)]
    public nint m_pPendingTypeLoad;

    [FieldOffset(248)]
    public nint m_Link;

    [FieldOffset(256)]
    public int m_dwLastError;

    [FieldOffset(264)]
    public nint m_CacheStackBase;

    [FieldOffset(272)]
    public nint m_CacheStackLimit;

    [FieldOffset(280)]
    public nint m_CacheStackSufficientExecutionLimit;

    [FieldOffset(288)]
    public nint m_CacheStackStackAllocNonRiskyExecutionLimit;

    [FieldOffset(296)]
    public nint m_pvHJRetAddr;

    [FieldOffset(304)]
    public nint m_ppvHJRetAddrPtr;

    [FieldOffset(312)]
    public nint m_HijackedFunction;

    [FieldOffset(320)]
    public int m_UserInterrupt;

    [FieldOffset(325)]
    public CLREvent m_DebugSuspendEvent;

    [FieldOffset(344)]
    public CLREvent m_EventWait;

    [FieldOffset(360)]
    public WaitEventLink m_WaitEventLink;

    [FieldOffset(408)]
    public nint m_ThreadHandle;

    [FieldOffset(416)]
    public nint m_ThreadHandleForClose;

    [FieldOffset(424)]
    public nint m_ThreadHandleForResume;

    [FieldOffset(432)]
    public int m_WeOwnThreadHandle;

    [FieldOffset(440)]
    public nint m_OSThreadId;

    [FieldOffset(448)]
    public nint m_ExposedObject;

    [FieldOffset(456)]
    public nint m_StrongHndToExposedObject;

    [FieldOffset(464)]
    public int m_Priority;

    [FieldOffset(468)]
    public int m_TraceCallCount;

    [FieldOffset(472)]
    public nint m_LastThrownObjectHandle;

    [FieldOffset(480)]
    public int m_ltoIsUnhandled;

    [FieldOffset(488)]
    public fixed byte m_ExceptionState[424];

    [FieldOffset(912)]
    public nint m_debuggerFilterContext;

    [FieldOffset(920)]
    public nint m_ProfilerFilterContext;

    [FieldOffset(928)]
    public int m_hijackLock;

    [FieldOffset(936)]
    public nint m_hCurrNotification;

    [FieldOffset(944)]
    public int m_fInteropDebuggingHijacked;

    [FieldOffset(948)]
    public int m_profilerCallbackState;

    [FieldOffset(952)]
    public fixed int m_dwProfilerEvacuationCounter[33];

    [FieldOffset(1084)]
    public uint m_monitorLockContentionCount;

    [FieldOffset(1088)]
    public int m_PreventAsync;

    [FieldOffset(1096)]
    public nint m_pSavedRedirectContext;

    [FieldOffset(1104)]
    public nint m_pOSContextBuffer;

    [FieldOffset(1112)]
    public fixed byte m_threadLocalBlock[40];

    [FieldOffset(1152)]
    public fixed byte m_tailCallTls[16];

    [FieldOffset(1168)]
    public int m_dwAVInRuntimeImplOkay;

    [FieldOffset(1176)]
    public nint m_pExceptionDuringStartup;

    [FieldOffset(1184)]
    public nint m_debuggerActivePatchSkipper;

    [FieldOffset(1192)]
    public int m_fAllowProfilerCallbacks;

    [FieldOffset(1196)]
    public int m_dwThreadHandleBeingUsed;

    [FieldOffset(1200)]
    public nint m_pCreatingThrowableForException;

    [FieldOffset(1208)]
    public int m_dwIndexClauseForCatch;

    [FieldOffset(1216)]
    public nint m_sfEstablisherOfActualHandlerFrame;

    [FieldOffset(1224)]
    public nint DebugBlockingInfo;

    [FieldOffset(1232)]
    public bool m_fDisableComObjectEagerCleanup;

    [FieldOffset(1233)]
    public bool m_fHasDeadThreadBeenConsideredForGCTrigger;

    [FieldOffset(1236)]
    public fixed byte m_random[236];

    [FieldOffset(1472)]
    public ulong m_uliInitializeSpyCookie;

    [FieldOffset(1480)]
    public bool m_fInitializeSpyRegistered;

    [FieldOffset(1488)]
    public nint m_pLastSTACtxCookie;

    [FieldOffset(1496)]
    public bool m_fGCSpecial;

    [FieldOffset(1504)]
    public nint m_pGCFrame;

    [FieldOffset(1512)]
    public int m_wCPUGroup;

    [FieldOffset(1520)]
    public nint m_pAffinityMask;

    [FieldOffset(1528)]
    public nint m_pAllLoggedTypes;

    [FieldOffset(1536)]
    public bool m_gcModeOnSuspension;

    [FieldOffset(1540)]
    public Guid m_activityId;

    [FieldOffset(1556)]
    public int m_HijackReturnKind;

    [FieldOffset(1560)]
    public nint m_currentPrepareCodeConfig;

    [FieldOffset(1568)]
    public bool m_isInForbidSuspendForDebuggerRegion;

    [FieldOffset(1569)]
    public bool m_hasPendingActivation;
}
