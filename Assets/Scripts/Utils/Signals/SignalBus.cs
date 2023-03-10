using System;
using System.Collections.Generic;
using System.Reflection;

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

        public void InvokeAbstractNotStatic<T>(T signal)
        {
            Invoke(signal);
            
            foreach (var implementedInterface in typeof(T).GetTypeInfo().ImplementedInterfaces)
            {
                foreach (var signalHandler in _signals)
                {
                    if (signalHandler.SignalType == implementedInterface)
                    {
                        signalHandler.HandlerAbstract?.Invoke(signal);
                        return;
                    }
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
        public static void InvokeAbstract<T>(T signal) => Instance.InvokeAbstractNotStatic(signal);

        #endregion
    }
}