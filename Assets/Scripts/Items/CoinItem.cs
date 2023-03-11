using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Custom/Items/Coin")]

    public class CoinItem: PlayerFallowItem
    {
        [field: SerializeField] public int Cash { get; private set; } = 1;
    }
}