using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Interfaces;
using Signals;
using UnityEngine;
using Utils.Signals;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rb;
    private int HP=3;
    
    private bool _readyToBlow=true;
    
    
    
    
    [SerializeField] protected float speed;
    
    [SerializeField] protected int maxHP=3;//максимально здоровье
    [SerializeField] protected int strength=1;//сила
    [SerializeField] protected float speedOfAtack=1;//скорость атаки
    [SerializeField] protected float invincibleDuration=1;//время неуязвимости
    
    
    
    protected Vector2 _movingDirection;
    
    
    public bool movable=true;
    public bool moving=true;

    private bool _isInvincible; // if player is invincible after getting damage 
    // Start is called before the first frame update
    protected virtual void Start()
    {
        SignalBus.AddListener<RoomSwitchSignal>(SetPauseMovement);
        HP = maxHP;
        _rb = GetComponent<Rigidbody2D>();
        _movingDirection = new Vector2(0, 0);
    }

    
    
    // Update is called once per frame
    protected virtual void Update()
    {
        if (movable)
        {
            CheckSwipe();
            if (_isTuching)
            {
                _TouchDuration = _TouchDuration + Time.deltaTime;
            }
        }
        
      //  Debug.Log(_TouchDuration);
    }

    protected virtual void FixedUpdate()
    {
        if (moving)
        {
            _rb.velocity = speed * _movingDirection;
        }
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
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
            if (_readyToBlow)
            {
                Special();
            }
        }
    }
    void GetReadyToBlow()
    {
        
        _readyToBlow = true;
    }
    protected virtual void Special()
    {
        //Debug.Log("Bam");
        _readyToBlow = false;
        Invoke( "GetReadyToBlow",speedOfAtack);
    }
    protected float GetAngleFromDirection()
    {
        return Mathf.Atan2(_movingDirection.y,_movingDirection.x)*Mathf.Rad2Deg;
    }
    
    async void SetPauseMovement(RoomSwitchSignal signal)
    {
        movable = false;
        await UniTask.Delay(1500);
        if(!this)
            return;
        movable = true;
    }

    public void Damage(int damage)
    {
        if(_isInvincible)
            return;
        HP -= damage;
        print($"Player got {damage} of damage");
        if (HP <= 0)
        {
            print("Player should die here.  ((");
        }
        ProcessInvincible();
    }

    // this method makes player invincible, waits invincible time and makes player damageable again
    // in future some other instructions can be added
    private async void ProcessInvincible()
    {
        if(_isInvincible)
            return;

        _isInvincible = true;
        await UniTask.Delay((int) (1000 * invincibleDuration));
        _isInvincible = false;
    }
}
