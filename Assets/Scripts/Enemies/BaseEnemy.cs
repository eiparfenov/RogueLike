using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;

namespace Enemies
{
    
    public abstract class BaseEnemy : MonoBehaviour, IDamageable
    {
        [SerializeField] protected int damage;
        [SerializeField] protected int health;
        [SerializeField] protected float speed;
        [SerializeField] protected float stunTime;
        protected Vector3 moveDirection;
        protected Rigidbody2D rb;
        public Transform Player { get; set; }
        public bool Active { get; set; }
        protected Vector3 DirectionToPlayer
        {
            get
            {
                if(Player)
                {
                    return (Player.position - transform.position).normalized;
                }
                else
                {
                    return Vector3.zero;
                }
            }
        }

        private void FixedUpdate()
        {
            Debug.DrawRay(transform.position, moveDirection, Color.black);
            if (Active)
            {
                rb.velocity = moveDirection * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public async void Damage(int damage)
        {
            Active = false;
            health -= damage;
            Debug.Log($"{name} got {damage} for player, it's current health = {health}");
            if (health <= 0)
            {
                await Die();
                return;
            }
            await UniTask.Delay((int)(1000 * stunTime));
            if (!this)
                return;
            Active = true;
        }

        protected virtual async UniTask Die()
        {
            await UniTask.Delay((int)(1000 * .5f));
            Destroy(gameObject);
        }

        protected virtual void TryDamagePlayer(Collider2D col)
        {
            if(!col.CompareTag("Player") || !Active)
                return;

            var damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damage);
            }
        }
    }
}