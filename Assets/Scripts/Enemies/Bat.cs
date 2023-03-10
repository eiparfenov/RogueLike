using System;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class Bat: BaseEnemy
    {
        [DisableIf(nameof(randomDirection))]
        [Dropdown(nameof(PossibleDirections))]
        [SerializeField] private Vector3 moveDirection;
        [SerializeField] private bool randomDirection;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            if (randomDirection)
            {
                moveDirection = (Vector3) PossibleDirections().OrderBy(x => Random.value).First().Value;
            }
        }

        private void FixedUpdate()
        {
            _rb.velocity = moveDirection * speed;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var normal = col.contacts[0].normal;
            moveDirection -= 2f * Vector3.Project(moveDirection, normal);
            
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