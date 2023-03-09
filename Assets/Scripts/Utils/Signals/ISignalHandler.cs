using System;

namespace Utils.Signals
{
    public interface ISignalHandler
    {
        public Type SignalType { get; }
    }
}