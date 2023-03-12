using Items;
using NaughtyAttributes;
using UnityEngine;

namespace UI.Menu
{
    [CreateAssetMenu(fileName = "Available items", menuName = "Custom/Available items", order = 0)]
    public class ItemsAvailable : ScriptableObject
    {
        public BaseItem[] items;

        [Button]
        public void LockAll()
        {
            foreach (var item in items)
            {
                item.opened = false;
            }
        }
    }
}