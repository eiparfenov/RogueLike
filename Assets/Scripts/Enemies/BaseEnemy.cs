using System;
using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;

namespace Enemies
{
    
    public abstract class BaseEnemy : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float health;
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

        public async void Damage(float damage)
        {
            Debug.Log($"{name} {damage}");
            Active = false;
            health -= damage;
            if (health < 0)
            {
                await Die();
                return;
            }
            await UniTask.Delay((int)(1000 * stunTime));
            Active = true;
        }

        protected virtual async UniTask Die()
        {
            await UniTask.Delay((int)(1000 * .5f));
            Destroy(gameObject);
        }
    }
}