using Interfaces;
using UnityEngine;

namespace Enemies
{
    public class Arrow: MonoBehaviour
    {
        public Vector3 moveDirection;
        public float speed;
        public int damage;
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
                var damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Damage(damage);
                }
            }

            if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }
}