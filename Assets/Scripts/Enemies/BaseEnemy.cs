using UnityEngine;

namespace Enemies
{
    
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected int health;
        [SerializeField] protected float speed;
        public Transform Player { get; private set; }
    }
}