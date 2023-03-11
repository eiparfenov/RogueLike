using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        void Damage(int damage);
        void Damage(int damage, Vector2 directionReclining);
    }
}