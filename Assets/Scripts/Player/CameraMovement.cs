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
        _lastCamPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (_isMoving)
            {
                _isMoving = false;
            }
        }

        void StartMoveCamera(RoomSwitchSignal signal)
        {
            Debug.Log("!!!!!!!!!!!");
            _nextCamPosition = signal.RoomPosition;
            _isMoving = true;
            _timeFromBeginChange = 0;
        }
    }
}
