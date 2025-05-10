using CS2External.Handlers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CS2External
{
    public class FunctionManager : IFunctionManager
    {
        private readonly IEnumerable<IFunction> _functions;
        private readonly HashSet<ILoopedFunction> _startedLoopedFunctions = new HashSet<ILoopedFunction>();

        public FunctionManager(IEnumerable<IFunction> functions)
        {
            _functions = functions ?? throw new ArgumentNullException(nameof(functions));
        }

        public void Execute()
        {
            foreach (var function in _functions)
            {
                if (function is ILoopedFunction loopedFunction && !_startedLoopedFunctions.Contains(loopedFunction))
                {
                    var thread = new Thread(() => loopedFunction.Execute())
                    {
                        IsBackground = true
                    };
                    thread.Start();
                    _startedLoopedFunctions.Add(loopedFunction);
                }
                else if (function is not ILoopedFunction)
                {
                    function.Execute();
                }
            }
        }

        public void ExecuteByName(string name)
        {
            foreach (var function in _functions)
            {
                if (function.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    function.Execute();
                    break;
                }
            }
        }
    }

    public interface IFunctionManager
    {
        void Execute();
        void ExecuteByName(string name);
    }
}