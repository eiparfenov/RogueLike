using Items;
using UnityEngine;

namespace RoomBehaviour
{
    public class ItemSpawner: MonoBehaviour,IRoomBehaviour
    {
        [SerializeField] private ItemsSet itemsSet;
        private bool _wasDropped;

        public void OnRoomEnteredEarly(Transform player)
        {
            if(!itemsSet) return;
            if(_wasDropped) return;
            var baseItem = itemsSet.GetItem();
            if(baseItem == null) return;
            var item = Instantiate(itemsSet.ItemPref, transform.position, Quaternion.identity);
            item.Item = baseItem;
            _wasDropped = true;
        }

        public void OnRoomEnteredLate()
        {
        }

        public void OnRoomExited()
        {
        }

        public bool Finished => true;
    }
}