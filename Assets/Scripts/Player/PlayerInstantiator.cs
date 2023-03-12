using System;
using UI.Menu;
using UnityEngine;

namespace Player
{
    public class PlayerInstantiator: MonoBehaviour
    {
        [SerializeField] private StartSettings startSettings;

        private void Start()
        {
            Instantiate(startSettings.players[startSettings.SelectedCharacter], Vector3.zero, Quaternion.identity);
        }
    }
}