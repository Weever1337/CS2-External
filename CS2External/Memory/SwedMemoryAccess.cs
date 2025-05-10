using Swed64;
using System;

namespace CS2External.Memory
{
    public class SwedMemoryAccess : IMemoryAccess
    {
        private readonly Swed _swed;

        public SwedMemoryAccess(string processName)
        {
            _swed = new Swed(processName);
        }

        public IntPtr GetModuleBase(string moduleName)
        {
            return _swed.GetModuleBase(moduleName);
        }

        public IntPtr ReadPointer(IntPtr address, int offset)
        {
            return _swed.ReadPointer(address, offset);
        }

        public uint ReadUInt(IntPtr address, int offset)
        {
            return _swed.ReadUInt(address + offset);
        }

        public void WriteUInt(IntPtr address, uint value)
        {
            _swed.WriteUInt(address, value);
        }

        public float ReadFloat(IntPtr address, int offset)
        {
            return _swed.ReadFloat(address + offset);
        }

        public void WriteFloat(IntPtr address, int offset, float value)
        {
            _swed.WriteFloat(address + offset, value);
        }

        public bool ReadBool(IntPtr address, int offset)
        {
            return _swed.ReadBool(address + offset);
        }

        public IntPtr ReadPointer(IntPtr address)
        {
            return _swed.ReadPointer(address);
        }

        public void WriteUInt(IntPtr address, int offset, uint value)
        {
            _swed.WriteUInt(address + offset, value);
        }

        public uint ReadUInt(nint v)
        {
            return _swed.ReadUInt(v);
        }
    }

    public interface IMemoryAccess
    {
        IntPtr GetModuleBase(string moduleName);
        IntPtr ReadPointer(IntPtr address, int offset);
        uint ReadUInt(IntPtr address, int offset);
        void WriteUInt(IntPtr address, uint value);
        float ReadFloat(IntPtr address, int offset);
        void WriteFloat(IntPtr address, int offset, float value);
        bool ReadBool(IntPtr address, int offset);
        IntPtr ReadPointer(IntPtr address);
        void WriteUInt(IntPtr address, int offset, uint value);
        uint ReadUInt(nint v);
    }
}