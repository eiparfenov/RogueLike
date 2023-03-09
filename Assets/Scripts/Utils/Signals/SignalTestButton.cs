using NaughtyAttributes;
using UnityEngine;

namespace Utils.Signals
{
    public class SignalTestButton<T>: MonoBehaviour
    {
        [SerializeField] private T signal;

        [Button("Test signal")]
        public void Test()
        {
            SignalBus.Invoke(signal);
        }
    }
}