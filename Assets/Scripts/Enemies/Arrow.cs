using System;
using UnityEngine;

namespace Enemies
{
    public class Arrow: MonoBehaviour
    {
        public Vector3 moveDirection;
        public float speed;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rb.velocity = moveDirection * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }

            if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }
}