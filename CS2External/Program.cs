using System;
using System.Collections.Generic;
using CS2External;
using CS2External.Functions;
using CS2External.Handlers;
using CS2External.Memory;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Initializing...");
            IMemoryAccess memoryAccess = new SwedMemoryAccess("cs2");
            Console.WriteLine("Attached successfully.");
            IMemoryContext memoryContext = new MemoryContext(memoryAccess);
            memoryContext.InitializeClient(memoryAccess.GetModuleBase("client.dll"));
            if (memoryContext.Client == IntPtr.Zero)
            {
                Console.WriteLine("Failed to get client module base.");
                return;
            }
            memoryContext.InitializeLocalPlayerPawn(memoryAccess.ReadPointer(memoryContext.Client + 0x1874040));
            memoryContext.InitializeForceJump(memoryContext.Client + 0x186CD50);
            IGameSettings settings = new GameSettings();
            var functions = new List<IFunction>
            {
                new Bunnyhop(memoryAccess, memoryContext, settings),
                new AntiFlash(memoryAccess, memoryContext, settings),
                new FovChanger(memoryAccess, memoryContext, settings)
            };
            IFunctionManager functionManager = new FunctionManager(functions);
            IOverlay overlay = new Overlay(settings);
            overlay.Start().Wait();
            functionManager.Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}