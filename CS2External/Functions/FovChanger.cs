using CS2External.Handlers;
using CS2External.Memory;
using System;
using System.Threading;

namespace CS2External.Functions
{
    public class FovChanger : ILoopedFunction
    {
        public string Name => "FovChanger";
        public bool Looped => true;

        private readonly IMemoryAccess _memoryAccess;
        private readonly IMemoryContext _memoryContext;
        private readonly IGameSettings _settings;

        public FovChanger(IMemoryAccess memoryAccess, IMemoryContext memoryContext, IGameSettings settings)
        {
            _memoryAccess = memoryAccess ?? throw new ArgumentNullException(nameof(memoryAccess));
            _memoryContext = memoryContext ?? throw new ArgumentNullException(nameof(memoryContext));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Execute()
        {
            while (true)
            {
                try
                {
                    if (_memoryContext.LocalPlayerPawn == IntPtr.Zero)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    IntPtr cameraServices = _memoryAccess.ReadPointer(_memoryContext.LocalPlayerPawn, Offsets.m_pCameraServices);
                    if (cameraServices == IntPtr.Zero)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    bool isScoped = _memoryAccess.ReadBool(_memoryContext.LocalPlayerPawn, Offsets.m_bIsScoped);
                    if (!isScoped)
                    {
                        uint currentFov = _memoryAccess.ReadUInt(cameraServices, Offsets.m_iFOV);
                        if (currentFov != (uint)_settings.Fov)
                        {
                            _memoryAccess.WriteUInt(cameraServices, Offsets.m_iFOV, (uint)_settings.Fov);
                        }
                    }
                    Thread.Sleep(1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"FovChanger error: {ex.Message}");
                    Thread.Sleep(100);
                }
            }
        }
    }
}