using System;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using Utils.Signals;

namespace Player
{
    public class CameraMovement : MonoBehaviour
    {
        private Vector2 _lastCamPosition;
        private Vector2 _nextCamPosition;
        private bool _isMoving=false;
        private float _timeFromBeginChange;
    
        [SerializeField] private float timeNeed2Change = 0.5f;
    
    
        void Start()
        {
            SignalBus.AddListener<RoomSwitchSignal>(StartMoveCamera);
            SignalBus.AddListener<LevelFinishSignal>(OnLevelFinish);
            _lastCamPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (_isMoving)
            {
                
                    _timeFromBeginChange = _timeFromBeginChange + Time.deltaTime;
                    var progress = Mathf.Min(1, _timeFromBeginChange / timeNeed2Change);
                    transform.position = _lastCamPosition + (_nextCamPosition - _lastCamPosition) * progress;
                    transform.position = transform.position + new Vector3(0, 0, -10);
                    if (progress >= 1)
                    {
                        _isMoving = false;
                        _lastCamPosition = transform.position;
                    }
                    
            }

                
        }
        

        void StartMoveCamera(RoomSwitchSignal signal)
        {
            Debug.Log("!!!!!!!!!!!");
            _nextCamPosition = signal.RoomPosition;
            _isMoving = true;
            _timeFromBeginChange = 0;
        }

        private async void OnLevelFinish(LevelFinishSignal signal)
        {
            _isMoving = false;
            await UniTask.Delay((int)(signal.Duration * 1000));
            _lastCamPosition = Vector3.back * 10;
            transform.position = Vector3.back * 10;
            await UniTask.Delay((int)(signal.Duration * 1000));
            _isMoving = true;
        }

        private void OnDestroy()
        {
            SignalBus.RemoveListener<RoomSwitchSignal>(StartMoveCamera);
            SignalBus.RemoveListener<LevelFinishSignal>(OnLevelFinish);
        }
    }
}
