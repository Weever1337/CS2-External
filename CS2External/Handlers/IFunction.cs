namespace CS2External.Handlers
{
    public interface IFunction
    {
        string Name { get; }
        void Execute();
    }

    public interface ILoopedFunction : IFunction
    {
        bool Looped { get; }
    }
}