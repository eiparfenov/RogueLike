using System;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class ItemInfoButton: MonoBehaviour
    {
        [SerializeField] private Image backGround;
        [SerializeField] private Image icon;
        [SerializeField] private Color lockedColor;
        [SerializeField] private Color unlockedColor;
        [SerializeField] private Button button;
        [Space] 
        [SerializeField] private ItemInfoMenu itemInfoMenuPref;
        
        private BaseItem _item;
        private bool _unlocked;

        public void SetItem(BaseItem item)
        {
            _item = item;
            _unlocked = _item.opened;

            icon.sprite = _item.Image;
            backGround.color = _unlocked ? unlockedColor : lockedColor;
        }

        private void Start()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            var infoMenu = Instantiate(itemInfoMenuPref, transform.parent.parent);
            infoMenu.ShowItem(_item);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }
    }
}