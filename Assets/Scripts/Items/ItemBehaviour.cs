using Player;
using UnityEngine;

namespace Items
{
    public class ItemBehaviour: MonoBehaviour
    {
        private BaseItem _item;
        public BaseItem Item
        {
            get => _item;
            set
            {
                _item = value;
                GetComponent<SpriteRenderer>().sprite = _item.Image;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                var player = col.GetComponent<PlayerMovement>();
                if (player)
                {
                    player.TakeItem(_item);
                }
                Destroy(gameObject);
            }
        }
    }
}