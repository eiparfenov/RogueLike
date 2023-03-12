using System;
using Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils.Signals;

namespace UI.Menu
{
    public class LevelFailedUI: MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private Button home;
        [SerializeField] private Button restart;
        [SerializeField] private Button @continue;
        [SerializeField] private ItemsAvailable items;
        [SerializeField] private StartSettings settings;
        private PlayerDeadSignal _signal;
        private void OnPlayerDie(PlayerDeadSignal signal)
        {
            _signal = signal;
            menu.SetActive(true);
        }

        private void OnRestart()
        {
            foreach (var item in items.items)
            {
                item.WasDropped = false;
            }
            if(_signal != null) _signal.Respawn = null;
            SceneManager.LoadScene("Gameplay");
        }

        private void OnHome()
        { 
            if(_signal != null) _signal.Respawn = null;
            SceneManager.LoadScene("StartScene");
        }

        private void OnContinue()
        {
            if(settings.Coins == 0) return;
            menu.gameObject.SetActive(false);
            _signal?.Respawn?.Invoke();
            if(_signal != null) _signal.Respawn = null;
        }

        private void Start()
        {
            restart.onClick.AddListener(OnRestart);
            home.onClick.AddListener(OnHome);
            @continue.onClick.AddListener(OnContinue);
            SignalBus.AddListener<PlayerDeadSignal>(OnPlayerDie);
        }
        
        private void OnDestroy()
        {
            if(_signal != null) _signal.Respawn = null;
            restart.onClick.RemoveListener(OnRestart);
            home.onClick.RemoveListener(OnHome);
            @continue.onClick.RemoveListener(OnContinue);
            SignalBus.RemoveListener<PlayerDeadSignal>(OnPlayerDie);
        }
    }
}