using Enemies;
using UnityEngine;

namespace RoomBehaviour
{
    public class EnemySpawner: MonoBehaviour, IRoomBehaviour
    {
        [SerializeField] private BaseEnemy enemyToSpawn;
        private BaseEnemy _spawnedEnemy;
        public void OnRoomEnteredEarly(Transform player)
        {
            _spawnedEnemy = Instantiate(enemyToSpawn, transform);
            _spawnedEnemy.Player = player;
        }

        public void OnRoomEnteredLate()
        {
            _spawnedEnemy.Active = true;
        }

        public void OnRoomExited()
        {
            if (_spawnedEnemy)
            {
                Destroy(_spawnedEnemy.gameObject);
            }
        }

        public bool Finished => !_spawnedEnemy;
    }
}