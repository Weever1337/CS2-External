using System;

namespace CS2External.Memory
{
    public class MemoryContext : IMemoryContext
    {
        private readonly IMemoryAccess _memoryAccess;

        public IntPtr Client { get; private set; }
        public IntPtr LocalPlayerPawn { get; private set; }
        public IntPtr ForceJump { get; private set; }

        public MemoryContext(IMemoryAccess memoryAccess)
        {
            _memoryAccess = memoryAccess ?? throw new ArgumentNullException(nameof(memoryAccess));
        }

        public void InitializeClient(IntPtr client)
        {
            Client = client;
        }

        public void InitializeLocalPlayerPawn(IntPtr localPlayerPawn)
        {
            LocalPlayerPawn = localPlayerPawn;
        }

        public void InitializeForceJump(IntPtr forceJump)
        {
            ForceJump = forceJump;
        }
    }

    public interface IMemoryContext
    {
        IntPtr Client { get; }
        IntPtr LocalPlayerPawn { get; }
        IntPtr ForceJump { get; }
        void InitializeClient(IntPtr client);
        void InitializeLocalPlayerPawn(IntPtr localPlayerPawn);
        void InitializeForceJump(IntPtr forceJump);
    }
}