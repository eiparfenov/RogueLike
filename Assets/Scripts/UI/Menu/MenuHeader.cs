using System;
using TMPro;
using UnityEngine;
using Utils.Signals;

namespace UI.Menu
{
    public class MenuHeader: MonoBehaviour
    {
        [SerializeField] private StartSettings startSettings;
        [SerializeField] private TextMeshProUGUI coins;

        private void Start()
        {
            SignalBus.AddListener<ItemBoughtSignal>(OnItemBought);
            coins.text = startSettings.Coins.ToString();
        }

        private void OnItemBought(ItemBoughtSignal signal)
        {
            coins.text = startSettings.Coins.ToString();
        }

        private void OnDestroy()
        {
            SignalBus.RemoveListener<ItemBoughtSignal>(OnItemBought);
        }
    }
}