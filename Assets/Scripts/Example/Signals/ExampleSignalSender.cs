using NaughtyAttributes;
using UnityEngine;
using Utils.Signals;

namespace Example.Signals
{
    public class ExampleSignalSender: MonoBehaviour
    {
        [SerializeField] private float someFloat;
        
        [Button]
        private void SendExampleSignal()
        {
            SignalBus.Invoke(new ExampleSignal(someFloat));
        }
    }
}