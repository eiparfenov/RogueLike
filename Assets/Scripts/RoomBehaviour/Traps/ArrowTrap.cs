using System;
using Cysharp.Threading.Tasks;
using Enemies;
using UnityEngine;

namespace RoomBehaviour.Traps
{
    public class ArrowTrap: MonoBehaviour
    {
        [SerializeField] private float reloadTime = 4f;
        [SerializeField] private float arrowSpeed;
        [SerializeField] private Arrow arrowPref;
        [SerializeField] private Transform arrowSource;
        [SerializeField] private int damage;

        private bool _loaded = true;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!_loaded) return;
            if (!col.CompareTag("Player") && !col.CompareTag("Enemy")) return;
            
            var arrow = Instantiate(
                arrowPref, 
                arrowSource.position, 
                Quaternion.Euler(Vector3.forward * Vector3.SignedAngle(
                        Vector3.up,
                        arrowSource.up,
                        Vector3.forward
                    )
                )
            );
            arrow.speed = arrowSpeed;
            arrow.moveDirection = arrowSource.up;
            arrow.damage = damage;
        }

        private async void Reload()
        {
            _loaded = false;
            await UniTask.Delay((int) (reloadTime * 1000));
            _loaded = true;
        }
        
    }
}