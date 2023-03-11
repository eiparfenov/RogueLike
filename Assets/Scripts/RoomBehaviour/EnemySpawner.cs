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
        }

        public async void OnRoomEnteredLate()
        {
            if (_spawnedEnemy)
            {
                _spawnedEnemy.Active = true;
            }

            await UniTask.WaitUntil(() => !_spawnedEnemy);
            
            DropItem();
        }

        public void OnRoomExited()
        {
            _isEnemyKilled = !_spawnedEnemy;
            if (_spawnedEnemy)
            {
                Destroy(_spawnedEnemy.gameObject);
            }
        }

        private void DropItem()
        {
            var item = Instantiate(itemsSet.ItemPref, transform.position, Quaternion.identity);
            item.Item = itemsSet.GetItem();
        }

        public bool Finished => !_spawnedEnemy;
    }
}