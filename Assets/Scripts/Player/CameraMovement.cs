using System.Collections;
using System.Collections.Generic;
using Signals;
using Utils.Signals;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector2 _lastCamPosition;
    private Vector2 _nextCamPosition;
    private bool _isMoving=false;
    private float _timeFromBeginChange;
    
    [SerializeField] private float timeNeed2Change = 1f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SignalBus.AddListener<RoomSwitchSignal>(StartMoveCamera);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving)
        {
            _timeFromBeginChange = _timeFromBeginChange + Time.deltaTime;
            var progress = Mathf.Min(1, _timeFromBeginChange / timeNeed2Change);
            transform.position = _lastCamPosition + (_nextCamPosition - _lastCamPosition) * progress;
            if (progress >= 1)
            {
                _isMoving = false;
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
}
