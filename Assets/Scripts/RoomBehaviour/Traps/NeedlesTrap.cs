using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Interfaces;
using System.Linq;
using UnityEngine;

namespace RoomBehaviour.Traps
{
    public class NeedlesTrap: MonoBehaviour
    {
        [SerializeField] private Needles needlesPref;
        [SerializeField] private float delay;
        [SerializeField] private float needlesUpTime;
        [field: SerializeField] public int Damage { get; private set; }

        private bool _firing;
        private async void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player") && !col.CompareTag("Enemy")) return;
            if(_firing) return;

            _firing = true;
            await UniTask.Delay((int) (1000 * delay));
            var needles = Instantiate(needlesPref, transform);
            needles.damage = Damage;
            await UniTask.Delay((int) (1000 * needlesUpTime));
            Destroy(needles.gameObject);
            _firing = false;
        }
    }
}