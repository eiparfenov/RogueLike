using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MenuMainController: MonoBehaviour
    {
        [SerializeField] private CharacterSelection characterSelection;
        [SerializeField] private Button toBattle;
        [SerializeField] private Button items;
        [SerializeField] private ItemMenu itemMenu;
        [SerializeField] private StartSettings startSettings;
        [SerializeField] private ItemsAvailable allItems;

        private void Start()
        {
            toBattle.onClick.AddListener(RunBattle);
            items.onClick.AddListener(OpenItems);
        }

        private void RunBattle()
        {
            foreach (var item in allItems.items)
            {
                item.WasDropped = false;
            }
            startSettings.SelectedCharacter = characterSelection.SelectedCharacter;
            SceneManager.LoadSceneAsync("Gameplay");
        }

        private void OpenItems()
        {
            itemMenu.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            toBattle.onClick.AddListener(RunBattle);
            items.onClick.AddListener(OpenItems);
        }
    }
}