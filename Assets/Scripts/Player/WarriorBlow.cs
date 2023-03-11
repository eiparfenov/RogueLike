using Interfaces;
using UnityEngine;

namespace Player
{
    public class WarriorBlow : MonoBehaviour
    {
        // Start is called before the first frame update
        public float blowDuration;
        public int damage;
        void Start()
        {
            Invoke("DestroySelf", blowDuration);
        }

        void DestroySelf()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Enemy"))
            {
                return;
            }

            var damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damage,col.transform.position-transform.position);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
