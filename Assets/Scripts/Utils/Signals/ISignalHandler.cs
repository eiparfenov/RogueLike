using System;

namespace Utils.Signals
{
    public interface ISignalHandler
    {
        public Type SignalType { get; }
        public Action<object> HandlerAbstract { get; }
    }
}