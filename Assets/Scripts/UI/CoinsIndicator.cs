using System;
using Signals;
using TMPro;
using UnityEngine;
using Utils.Signals;

namespace UI
{
    public class CoinsIndicator: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsText;

        private void Start()
        {
            SignalBus.AddListener<PlayerCoinsChangedSignal>(OnCoinsChanged);
        }

        private void OnCoinsChanged(PlayerCoinsChangedSignal signal)
        {
            coinsText.text = signal.CurrentCoins.ToString();
        }

        private void OnDestroy()
        {
            SignalBus.RemoveListener<PlayerCoinsChangedSignal>(OnCoinsChanged);
        }
    }
}