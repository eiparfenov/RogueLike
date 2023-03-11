using NaughtyAttributes;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Custom/Items/Special")]

    public class SpecialItem: BaseItem
    {
        public override bool FallowPlayer => false;
        [field: SerializeField] public SpecialItemType Type { get; private set; }
        
        [field: ShowIf(nameof(RequireChance))]
        [field: SerializeField] public float Chance { get; private set; }
        
        [field: ShowIf(nameof(RequireMultiplier))]
        [field: SerializeField] public float Multiplier { get; private set; }

        [field: ShowIf(nameof(RequireRadius))] 
        [field: SerializeField] public float Radius { get; private set; }

        private bool RequireChance =>
            Type is SpecialItemType.Vampirism or SpecialItemType.Greed or SpecialItemType.LuckyChance;

        private bool RequireMultiplier =>
            Type is SpecialItemType.PowerHit or SpecialItemType.MoreSize;

        private bool RequireRadius => Type == SpecialItemType.RadiusHits;
        public enum SpecialItemType
        {
            Vampirism, Greed, MoreSize, MoreHit, PowerHit, AttackOnRotation, LuckyChance, RadiusHits
        }
    }
}