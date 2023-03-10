using System;
using UnityEngine;

namespace Enemies
{
    
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected int health;
        [SerializeField] protected float speed;
        protected Vector3 moveDirection;
        protected Rigidbody2D rb;
        public Transform Player { get; set; }
        public bool Active { get; set; }
        
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
    }
}