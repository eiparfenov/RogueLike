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
                var direction = moveDirection;
                speed = 0;
                transform.parent = other.transform;
                Invoke("DestoyArrow",1);
                GetComponent<AudioSource>().Play();
                var damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Damage(damage,direction);
                }
            }

            if (other.CompareTag("Wall"))
            {
                speed = 0;
                Invoke("DestoyArrow",1);
                GetComponent<AudioSource>().Play();
            }
        }

        public void DestoyArrow()
        {
            Destroy(gameObject);
        }
    }
}