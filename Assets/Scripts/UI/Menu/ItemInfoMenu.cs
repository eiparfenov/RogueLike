using System;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Signals;

namespace UI.Menu
{
    public class ItemInfoMenu: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Image background;
        [SerializeField] private Image icon;
        [SerializeField] private Button buy;
        [SerializeField] private Button close;
        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private Color lockedColor;
        [SerializeField] private Color unlockedColor;
        [SerializeField] private GameObject[] stars;

        [Space] 
        [SerializeField] private StartSettings startSettings;

        private BaseItem _item;
        
        public void ShowItem(BaseItem item)
        {
            icon.sprite = item.Image;
            description.text = item.Description;
            var color = item.opened ? unlockedColor : lockedColor;
            background.color = color;
            buy.gameObject.SetActive(!item.opened);
            price.text = item.priceInMenu.ToString();
            _item = item;
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].SetActive(i < item.stars);
            }
        }

        private void Buy()
        {
            if(startSettings.Coins < _item.priceInMenu) return;
            startSettings.Coins -= _item.priceInMenu;
            _item.opened = true;
            SignalBus.Invoke(new ItemBoughtSignal());
            ShowItem(_item);
        }

        private void Close()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            buy.onClick.AddListener(Buy);
            close.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            buy.onClick.AddListener(Buy);
            close.onClick.AddListener(Close);
        }
    }
}