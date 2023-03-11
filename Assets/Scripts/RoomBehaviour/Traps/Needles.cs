using System;
using Interfaces;
using UnityEngine;

namespace RoomBehaviour.Traps
{
    public class Needles:MonoBehaviour
    {
        public int damage;

        private void OnTriggerEnter2D(Collider2D col)
        {
            var target = col.GetComponent<IDamageable>();
            if(target == null) return;
            target.Damage(damage);
        }
    }
}