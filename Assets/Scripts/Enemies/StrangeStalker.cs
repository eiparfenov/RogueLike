using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class StrangeStalker: BaseEnemy
    {
        protected override async void Awake()
        {
            base.Awake();
            await UniTask.WaitUntil(() => Player);
            var directionToPlayer = Player.position - transform.position;
            moveDirection = new[] {Vector3.up, Vector3.down, Vector3.right, Vector3.left}
                .OrderByDescending(x => Vector3.Dot(directionToPlayer, x))
                .First();
        }

        private void Update()
        {
            var directionToPlayer = Player.position - transform.position;
            if (Vector3.Dot(directionToPlayer, moveDirection) < 0)
            {
                var newAxis = Vector3.Cross(moveDirection, Vector3.forward);
                moveDirection = newAxis.normalized * Mathf.Sign(Vector3.Dot(directionToPlayer, newAxis));
            }
        }
    }
}