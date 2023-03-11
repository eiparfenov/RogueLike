using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Enemies
{
    public class Bat: BaseEnemy
    {
        [DisableIf(nameof(randomDirection))]
        [SerializeField] private bool randomDirection;

        private Rigidbody2D _rb;

        protected override bool isFlyingEnemy => true;

        protected override void Awake()
        {
            base.Awake();
            if (randomDirection)
            {
                moveDirection = (Vector3) PossibleDirections().OrderBy(x => Random.value).First().Value;
            }
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            TryDamagePlayer(col.collider);
            var normal = col.contacts[0].normal;
            var moveDir = moveDirection - Vector3.Project(moveDirection, normal) + (Vector3)normal;
            Debug.DrawRay(col.contacts[0].point, normal, Color.red);
            Debug.DrawRay(col.contacts[0].point, moveDir, Color.yellow);
            moveDirection = PossibleDirections()
                .Select(x => (Vector3) x.Value)
                .OrderByDescending(x => Vector3.Dot(moveDir, x))
                .First();
        }

        private DropdownList<Vector3> PossibleDirections() => new ()
        {
            {"ur", Vector3.up + Vector3.right},
            {"dr", Vector3.down + Vector3.right},
            {"dl", Vector3.down + Vector3.left},
            {"ul", Vector3.left + Vector3.up}
        };
    }
}