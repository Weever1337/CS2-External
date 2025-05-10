using CS2External.Handlers;
using CS2External.Memory;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace CS2External.Functions
{
    public class Bunnyhop : ILoopedFunction
    {
        public string Name => "Bunnyhop";
        public bool Looped => true;

        private readonly IMemoryAccess _memoryAccess;
        private readonly IMemoryContext _memoryContext;
        private readonly IGameSettings _settings;

        private bool _lastJumpState = false;
        private long _lastJumpTime = 0;

        public Bunnyhop(IMemoryAccess memoryAccess, IMemoryContext memoryContext, IGameSettings settings)
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
                    if (!_settings.IsBunnyHopEnabled || _memoryContext.LocalPlayerPawn == IntPtr.Zero)
                    {
                        Thread.Sleep(10);
                        continue;
                    }

                    IntPtr localPlayerPawn = _memoryContext.LocalPlayerPawn;
                    if (localPlayerPawn == IntPtr.Zero)
                    {
                        Thread.Sleep(10);
                        continue;
                    }

                    uint flags = _memoryAccess.ReadUInt(localPlayerPawn + Offsets.m_fFlagsOffset);
                    bool isOnGround = (flags & Offsets.FL_ONGROUND) != 0;

                    bool isSpacePressed = (GetAsyncKeyState(0x20) & 0x8000) != 0;

                    long currentTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
                    if (isSpacePressed && isOnGround && (currentTime - _lastJumpTime) > 50)
                    {
                        if (!_lastJumpState)
                        {
                            _memoryAccess.WriteUInt(_memoryContext.ForceJump, Offsets.IN_JUMP);
                            _lastJumpTime = currentTime;
                            _lastJumpState = true;
                        }
                    }
                    else if (!isSpacePressed)
                    {
                        _lastJumpState = false;
                    }

                    Thread.Sleep(1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Bunnyhop error: {ex.Message}");
                    Thread.Sleep(100);
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
    }
}