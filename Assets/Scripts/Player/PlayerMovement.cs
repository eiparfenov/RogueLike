using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _rb;
    private Vector2 _movingDirection;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movingDirection = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CheckSwipe();
        if (_isTuching)
        {
            _TouchDuration = _TouchDuration + Time.deltaTime;
        }
      //  Debug.Log(_TouchDuration);
    }

    private void FixedUpdate()
    {
        _rb.velocity = speed * _movingDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _movingDirection = -_movingDirection;
    }
    

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;
    private float _TouchDuration;
    private bool _isTuching=false;

    private void CheckSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startTouchPosition = Input.mousePosition;
            _isTuching = true;
            _TouchDuration = 0;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _endTouchPosition = Input.mousePosition;
            ChangeDirection();
            _isTuching=false;
        }
        
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _startTouchPosition = touch.position;
                _isTuching=true;
                _TouchDuration = 0;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _endTouchPosition = touch.position;
                _isTuching=false;
                ChangeDirection();
                
            }
        }
    }

    private void ChangeDirection()
    {
        float swipeDistance = Vector2.Distance(_startTouchPosition, _endTouchPosition);

        if (swipeDistance > 50f)
        {
            Vector2 swipeDirection = _endTouchPosition - _startTouchPosition;

            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                if (swipeDirection.x > 0)
                {
                    // Swiped right
                    _movingDirection = new Vector2(1, 0);
                }
                else
                {
                    // Swiped left
                    _movingDirection = new Vector2(-1, 0);
                }
            }
            else
            {
                if (swipeDirection.y > 0)
                {
                    // Swiped up
                    _movingDirection = new Vector2(0, 1);
                }
                else
                {
                    // Swiped down
                    _movingDirection = new Vector2(0, -1);
                }
            }
        }
        else if (_TouchDuration < 0.5)// длительность тапа
        {
            Special();
        }
    }

    protected virtual void Special()
    {
        //Debug.Log("Bam");
    }
}
