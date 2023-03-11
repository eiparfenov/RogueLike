using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Custom/Items/Simple")]
    public class SimpleItem: BaseItem
    {
        public override bool FallowPlayer => false;
    }
}