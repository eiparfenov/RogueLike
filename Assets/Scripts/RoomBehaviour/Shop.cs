using System;
using Items;
using Player;
using TMPro;
using UnityEngine;

namespace RoomBehaviour
{
    public class Shop: MonoBehaviour, IRoomBehaviour
    {
        [SerializeField] private ItemsSet itemsSet;
        [SerializeField] private SpriteRenderer itemRenderer;
        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private TextMeshProUGUI description;

        private BaseItem _item;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            var player = col.GetComponent<PlayerMovement>();
            var playerStats = player.PlayerStats;
            
            var coins = playerStats.Coins;
            if(coins < _item.priceInShop) return;

            player.PlayerStats.Coins -= _item.priceInShop;
            player.TakeItem(_item);
            _item.wasDropped = true;
            Destroy(gameObject);
        }

        public void OnRoomEnteredEarly(Transform player)
        {
            if(_item && !_item.wasDropped) return;
            
            _item = itemsSet.GetItem();

            if (!_item) gameObject.SetActive(false);
            
            itemRenderer.sprite = _item.Image;
            price.text = _item.priceInShop.ToString();
            description.text = _item.Description;
        }

        public void OnRoomEnteredLate()
        {
        }

        public void OnRoomExited()
        {
        }

        public bool Finished => true;
    }
}