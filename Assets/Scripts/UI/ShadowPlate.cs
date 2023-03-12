using System;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Utils.Signals;

namespace UI
{
    public class ShadowPlate: MonoBehaviour
    {
        [SerializeField] private Image image;

        private void Start()
        {
            SignalBus.AddListener<LevelFinishSignal>(OnLevelFinished);
        }

        private async void OnLevelFinished(LevelFinishSignal signal)
        {
            var progress = 0f;
            while (progress < 1)
            {
                progress += Time.deltaTime / signal.Duration;
                image.color = new Color(0, 0, 0, progress);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            progress = 0f;
            
            while (progress < 1)
            {
                progress += Time.deltaTime / signal.Duration;
                image.color = new Color(0, 0, 0, 1 - progress);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        private void OnDestroy()
        {
            SignalBus.RemoveListener<LevelFinishSignal>(OnLevelFinished);
        }
    }
}