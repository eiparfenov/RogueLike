using System;
using Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils.Signals;

namespace UI.Menu
{
    public class GameEndUI: MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private Button home;
        [SerializeField] private Button restart;
        [SerializeField] private ItemsAvailable items;
        [SerializeField] private StartSettings settings;
        private void OnPlayerDie(GameEndedSignal signal)
        {
            menu.SetActive(true);
            settings.Coins += 1;
        }

        private void OnRestart()
        {
            foreach (var item in items.items)
            {
                item.WasDropped = false;
            }
            SceneManager.LoadScene("Gameplay");
        }

        private void OnHome()
        { 
            SceneManager.LoadScene("StartScene");
        }

        private void Start()
        {
            restart.onClick.AddListener(OnRestart);
            home.onClick.AddListener(OnHome);
            SignalBus.AddListener<GameEndedSignal>(OnPlayerDie);
        }
        
        private void OnDestroy()
        {
            restart.onClick.RemoveListener(OnRestart);
            home.onClick.RemoveListener(OnHome);
            SignalBus.RemoveListener<GameEndedSignal>(OnPlayerDie);
        }
    }
}