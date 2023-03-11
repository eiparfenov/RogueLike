using Signals;
using UnityEngine;
using Utils.Signals;

namespace UI
{
    public class HealthIndicator: MonoBehaviour
    {
        [SerializeField] private Heart[] hearts;

        private void Start()
        {
            SignalBus.AddListener<PlayerHealthChangedSignal>(OnPlayerHealthChanged);
        }

        private void OnPlayerHealthChanged(PlayerHealthChangedSignal signal)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].State = signal.MaxHealth > i
                    ? signal.Health > i 
                        ? Heart.HeartState.On 
                        : Heart.HeartState.Hit
                    : Heart.HeartState.Off;
            }
        }

        private void OnDestroy()
        {
            SignalBus.RemoveListener<PlayerHealthChangedSignal>(OnPlayerHealthChanged);
        }
    }
}