using UnityEngine;

namespace Items
{
    public class PlayerFallowItem: BaseItem
    {
        public override bool FallowPlayer => true;
        [field: SerializeField] public float FallowSpeed { get; private set; }
    }
}