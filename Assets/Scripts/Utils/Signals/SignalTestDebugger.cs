using UnityEngine;

namespace Utils.Signals
{
    public class SignalTestDebugger<T>: MonoBehaviour
    {
        private void OnEnable()
        {
            SignalBus.AddListener<T>(Log);
        }

        protected virtual void Log(T signal)
        {
            Debug.unityLogger.Log(this.GetType().ToString(), signal.ToString());
        }

        private void OnDisable()
        {
            SignalBus.RemoveListener<T>(Log);
        }
    }
}