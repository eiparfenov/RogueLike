using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Custom/Items/Heart")]

    public class HeartItem: PlayerFallowItem
    {
        [field: SerializeField] public int Heal { get; private set; } = 1;
        public override bool FallowPlayer => true;
    }
}