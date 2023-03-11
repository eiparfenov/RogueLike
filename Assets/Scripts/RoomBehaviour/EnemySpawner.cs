using Cysharp.Threading.Tasks;
using Enemies;
using Items;
using UnityEngine;

namespace RoomBehaviour
{
    public class EnemySpawner: MonoBehaviour, IRoomBehaviour
    {
        [SerializeField] private BaseEnemy enemyToSpawn;
        [SerializeField] private ItemsSet itemsSet;
        private BaseEnemy _spawnedEnemy;
        private bool _isEnemyKilled;
        public void OnRoomEnteredEarly(Transform player)
        {
            if (_isEnemyKilled)
                return;
            _spawnedEnemy = Instantiate(enemyToSpawn, transform);
            _spawnedEnemy.Player = player;
            _spawnedEnemy.onDie += DropItem;
        }

        public async void OnRoomEnteredLate()
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

        private void DropItem(Vector3 pos)
        {
            if(!itemsSet) return;
            var baseItem = itemsSet.GetItem();
            if(baseItem == null) return;
            var item = Instantiate(itemsSet.ItemPref, pos, Quaternion.identity);
            item.Item = baseItem;
            _spawnedEnemy.onDie -= DropItem;
        }

        public bool Finished => !_spawnedEnemy;
    }
}