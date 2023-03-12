using UnityEngine;

namespace UI.Menu
{
    [CreateAssetMenu(fileName = "StartSettings", menuName = "Custom/StartSettings", order = 0)]
    public class StartSettings : ScriptableObject
    {
        [field: SerializeField] public int SelectedCharacter { get; set; }
        [field: SerializeField] public int Coins { get; set; }
    }
}