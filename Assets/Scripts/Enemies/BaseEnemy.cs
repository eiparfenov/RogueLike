using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;

namespace Enemies
{
    
    public abstract class BaseEnemy : MonoBehaviour, IDamageable
    {
        
        [SerializeField] protected EnemyStats enemyStats;
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
                rb.velocity = moveDirection * enemyStats.Speed;
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
            enemyStats.Health -= damage;
            Debug.Log($"{name} got {damage} for player, it's current health = {enemyStats.Health}");
            if (enemyStats.Health <= 0)
            {
                await Die();
                return;
            }
            await UniTask.Delay((int)(1000 * enemyStats.StunTime));
            if (!this)
                return;
            Active = true;
        }
        public void Damage(int damage,Vector2 directionReclining)
        {
            Damage(damage);
        }

        protected virtual async UniTask Die()
        {
            await UniTask.Delay((int)(1000 * .5f));
            if (gameObject) Destroy(gameObject);
        }

        protected virtual void TryDamagePlayer(Collider2D col)
        {
            if(!col.CompareTag("Player") || !Active)
                return;

            var damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(enemyStats.Damage,transform.position-col.transform.position);
            }
        }
    }
}