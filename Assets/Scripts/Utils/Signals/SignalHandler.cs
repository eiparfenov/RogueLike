using System;

namespace Utils.Signals
{
    public class SignalHandler<T> : ISignalHandler
    {
        public Action<T> Handler { get; set; }
        public Type SignalType => typeof(T);
        public Action<object> HandlerAbstract => (signal) =>  Handler?.Invoke((T) signal);
    }
}