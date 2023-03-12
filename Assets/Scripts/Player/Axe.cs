using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class Axe: MonoBehaviour
    {
        public PlayerStats playerStats;
        [SerializeField] private AudioSource source;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Enemy"))
            {
                return;
            }

            var damageable = col.GetComponent<IDamageable>();
            if (damageable != null)
            {
                source.Play();
                damageable.Damage(playerStats.Damage, col.transform.position-transform.position);
            }
        }

        private void Start()
        {
            source = GetComponent<AudioSource>();
        }
    }
}