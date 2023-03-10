using Enemies;
using UnityEngine;

namespace RoomBehaviour
{
    public class EnemySpawner: MonoBehaviour, IRoomBehaviour
    {
        [SerializeField] private BaseEnemy enemyToSpawn;
        private bool _isEnemyKilled;
        private BaseEnemy _spawnedEnemy;
        public void OnRoomEntered(Transform player)
        {
            
        }

        public void OnRoomExited()
        {
            
        }
    }
}