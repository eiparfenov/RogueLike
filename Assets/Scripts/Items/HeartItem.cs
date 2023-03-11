using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Custom/Items/Heart")]

    public class HeartItem: BaseItem
    {
        [field: SerializeField] public int Heal { get; private set; }
        public override bool IsCoinOrHeal => true;
    }
}