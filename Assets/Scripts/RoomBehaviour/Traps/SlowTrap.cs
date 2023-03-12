using System;
using Interfaces;
using UnityEngine;

namespace RoomBehaviour.Traps
{
    public class SlowTrap: MonoBehaviour
    {
        [field: SerializeField] public float SpeedMult { get; private set; }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var target = col.GetComponent<ISlowTrapInteracting>();
            if(target == null) return;
            target.SlowTrap(true, this);
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            var target = col.GetComponent<ISlowTrapInteracting>();
            if(target == null) return;
            target.SlowTrap(false, this);
        }
    }
}