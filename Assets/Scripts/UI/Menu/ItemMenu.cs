using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Signals;

namespace UI.Menu
{
    public class ItemMenu: MonoBehaviour
    {
        [SerializeField] private ItemsAvailable itemsAvailable;
        [SerializeField] private ItemInfoButton itemInfoButtonPref;
        [SerializeField] private Button back;
        [SerializeField] private Transform parentForButtons;

        private List<ItemInfoButton> buttons;
        private void Start()
        {
            buttons = new List<ItemInfoButton>();
            for (int i = 0; i < itemsAvailable.items.Length; i++)
            {
                var button = Instantiate(itemInfoButtonPref, parentForButtons);
                button.SetItem(itemsAvailable.items[i]);
                buttons.Add(button);
            }
            back.onClick.AddListener(Back);
            SignalBus.AddListener<ItemBoughtSignal>(OnItemBought);
            
        }

        private void OnItemBought(ItemBoughtSignal signal)
        {
            for (int i = 0; i < itemsAvailable.items.Length; i++)
            {
                buttons[i].SetItem(itemsAvailable.items[i]);
            }
        }

        private void Back()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            back.onClick.RemoveListener(Back);
            SignalBus.RemoveListener<ItemBoughtSignal>(OnItemBought);
        }
    }
}