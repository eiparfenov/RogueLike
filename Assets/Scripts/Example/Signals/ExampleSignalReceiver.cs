using UnityEngine;
using Utils.Signals;

namespace Example.Signals
{
    public class ExampleSignalReceiver: MonoBehaviour
    {
        private void OnEnable()
        {
            SignalBus.AddListener<ExampleSignal>(Log);
        }

        protected virtual void Log(ExampleSignal signal)
        {
            Debug.unityLogger.Log($"Received signal {signal.SomeNumber}!!!");
        }

        private void OnDisable()
        {
            SignalBus.RemoveListener<ExampleSignal>(Log);
        }
    }
}