using System;
using Player;
using UnityEngine;

namespace Items
{
    public class ItemBehaviour: MonoBehaviour
    {
        private Transform _player;
        private BaseItem _item;
        private Rigidbody2D _rb;
        private float _speed;
        public BaseItem Item
        {
            get => _item;
            set
            {
                _item = value;
                if (_item.FallowPlayer)
                {
                    _rb = gameObject.AddComponent<Rigidbody2D>();
                    _rb.gravityScale = 0f;
                    _player = FindObjectOfType<PlayerMovement>().transform;
                    _speed = ((PlayerFallowItem) _item).FallowSpeed;
                }
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

        private void FixedUpdate()
        {
            if (_speed == 0) return;
            
            var directionToPlayer = _player.position - transform.position;
            directionToPlayer = directionToPlayer.normalized;
            _rb.velocity = directionToPlayer * _speed;
        }
    }
}