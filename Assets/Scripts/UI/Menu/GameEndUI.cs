using System;
using Signals;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI gems;
        private void OnPlayerDie(GameEndedSignal signal)
        {
            menu.SetActive(true);
            settings.Coins += signal.LevelsCount;
            gems.text = $"+{signal.LevelsCount}";
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