using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Enemies
{
    public class StrangeStalker: BaseEnemy
    {
        protected override async void Awake()
        {
            base.Awake();
            await UniTask.WaitUntil(() => Player);
            moveDirection = new[] {Vector3.up, Vector3.down, Vector3.right, Vector3.left}
                .OrderByDescending(x => Vector3.Dot(DirectionToPlayer, x))
                .First();
        }

        private void Update()
        {
            if (Vector3.Dot(DirectionToPlayer, moveDirection) < 0)
            {
                var newAxis = Vector3.Cross(moveDirection, Vector3.forward);
                moveDirection = newAxis.normalized * Mathf.Sign(Vector3.Dot(DirectionToPlayer, newAxis));
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            TryDamagePlayer(col.collider);
        }

        protected override bool isFlyingEnemy => false;
    }
}