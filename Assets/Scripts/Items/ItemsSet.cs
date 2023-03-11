using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
    [CreateAssetMenu(fileName = "ItemsSet", menuName = "Custom/ItemsSet")]
    public class ItemsSet: ScriptableObject
    {
        [SerializeField] private List<ItemSetItem> items;
        [field: SerializeField] public ItemBehaviour ItemPref { get; private set; }

        public BaseItem GetItem()
        {
            var allChances = items.Select(x => x.Chance).Sum();
            var chance = Random.value * allChances;
            var i = 0;
            while (chance > items[i].Chance)
            {
                chance -= items[i].Chance;
                i++;
            }
            return items[i].Item;
        }
    }

    [Serializable]
    public class ItemSetItem
    {
        [field: SerializeField] public int Chance { get; private set; } = 1;
        [field: SerializeField] public BaseItem Item { get; private set; }
    }
}