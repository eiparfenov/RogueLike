using System;
using System.Collections.Generic;

namespace Utils.Signals
{
    public class SignalBus
    {
        private readonly List<ISignalHandler> _signals;
        public void AddListenerNotStatic<T>(Action<T> handler)
        {
            foreach (var signalHandler in _signals)
            {
                if (signalHandler.SignalType == typeof(T))
                {
                    ((SignalHandler<T>) signalHandler).Handler += handler;
                    return;
                }
            }

            var newSignalHandler = new SignalHandler<T>();
            newSignalHandler.Handler += handler;
            _signals.Add(newSignalHandler);
        }
        public void RemoveListenerNotStatic<T>(Action<T> handler)
        {
            foreach (var signalHandler in _signals)
            {
                if (signalHandler.SignalType == typeof(T))
                {
                    ((SignalHandler<T>) signalHandler).Handler -= handler;
                    return;
                }
            }
        }
        public void InvokeNotStatic<T>(T signal)
        {
            foreach (var signalHandler in _signals)
            {
                if (signalHandler.SignalType == typeof(T))
                {
                    ((SignalHandler<T>) signalHandler).Handler?.Invoke(signal);
                    return;
                }
            }
        }
        
        #region Singletone
        
        private static SignalBus instance;
        private static SignalBus Instance
        {
            get
            {
                instance ??= new SignalBus();
                return instance;
            }
        }
        private SignalBus()
        {
            _signals = new List<ISignalHandler>();
        }
        
        #endregion
        #region Static

        public static void AddListener<T>(Action<T> handler) => Instance.AddListenerNotStatic(handler);
        public static void RemoveListener<T>(Action<T> handler) => Instance.RemoveListenerNotStatic(handler);
        public static void Invoke<T>(T signal) => Instance.InvokeNotStatic(signal);

        #endregion
    }
}