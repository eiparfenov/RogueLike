using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Custom/Items/SellableHeart")]

    public class SellableHeart: HeartItem
    {
        public override bool FallowPlayer => false;
        public override bool WasDropped
        {
            get => false;
            set { }
        }
    }
}