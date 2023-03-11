using Enemies;
using UnityEngine;

namespace RoomBehaviour
{
    public class EnemySpawner: MonoBehaviour, IRoomBehaviour
    {
        [SerializeField] private BaseEnemy enemyToSpawn;
        private BaseEnemy _spawnedEnemy;
        private bool _isEnemyKilled;
        public void OnRoomEnteredEarly(Transform player)
        {
            if (_isEnemyKilled)
                return;
            _spawnedEnemy = Instantiate(enemyToSpawn, transform);
            _spawnedEnemy.Player = player;
        }

        public void OnRoomEnteredLate()
        {
            if (_spawnedEnemy)
            {
                _spawnedEnemy.Active = true;
            }
        }

        public void OnRoomExited()
        {
            _isEnemyKilled = !_spawnedEnemy;
            if (_spawnedEnemy)
            {
                Destroy(_spawnedEnemy.gameObject);
            }
        }

        public bool Finished => !_spawnedEnemy;
    }
}