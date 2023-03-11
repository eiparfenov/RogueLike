using System;
using Interfaces;
using UnityEngine;

namespace RoomBehaviour.Traps
{
    public class PitTrap: MonoBehaviour
    {
        [SerializeField] private Transform center;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject leaves;
        [field: SerializeField] public float PlayerTimeInside { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        public Vector3 Center => center.position;
        public Vector3 SpawnPoint => spawnPoint.position;
        
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            var target = col.GetComponent<IPitTrapInteracting>();
            if(target == null) return;
            target.PitTrap(this);
            Open();
        }
        

        private async void Open()
        {
            if (leaves)
            {
                Destroy(leaves);
            }
        }
    }
}