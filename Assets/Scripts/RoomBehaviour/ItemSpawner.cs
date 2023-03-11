using Items;
using UnityEngine;

namespace RoomBehaviour
{
    public class ItemSpawner: MonoBehaviour,IRoomBehaviour
    {
        [SerializeField] private ItemsSet itemsSet;

        public void OnRoomEnteredEarly(Transform player)
        {
            if(!itemsSet) return;
            var baseItem = itemsSet.GetItem();
            if(baseItem == null) return;
            var item = Instantiate(itemsSet.ItemPref, transform.position, Quaternion.identity);
            item.Item = baseItem;
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