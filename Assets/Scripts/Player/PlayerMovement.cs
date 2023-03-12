using System;
using Cysharp.Threading.Tasks;
using Interfaces;
using Items;
using RoomBehaviour.Traps;
using Signals;
using UnityEngine;
using Utils.Signals;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, IDamageable, IPitTrapInteracting, ISlowTrapInteracting
    {
        [SerializeField] protected PlayerStats playerStats;

        public PlayerStats PlayerStats => playerStats;

        [SerializeField] protected PlayerSound playerSound;
        private Rigidbody2D _rb;
        protected Animator anim;
        [SerializeField]  private AudioSource[] audioSource;
        protected virtual async void Start()
        {
            
            playerStats.RecalculateStats();
            SignalBus.AddListener<RoomSwitchSignal>(SetPauseMovement);
            SignalBus.AddListener<LevelFinishSignal>(OnLevelFinished);
            SignalBus.AddListener<EnemyDieSignal>(Vampirism);
            playerStats.Health = playerStats.MaxHealth;
            playerStats.Init();
            _rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            movingDirection = new Vector2(0, 0);
        
            // Тут мне нужно сообщить индикатору здоровья начальное состояние игрока
            // поскольку подписка на сигнал о изменении хп игрока происходит в start я не могу быть уверенным, что индикатор
            // уже подпишится на сигнал к моменту вызова. Поэтому я пропускаю кадр при помощи UniTask.Yield(PlayerLoopTiming.Update)
            // Да, пока у нас нет DI приходится танцевать с костылями ((
        
            await UniTask.Yield(PlayerLoopTiming.Update);
            SignalBus.Invoke(new PlayerHealthChangedSignal(){MaxHealth = playerStats.MaxHealth, Health = playerStats.Health});
        }
        protected virtual void Update()
        {
            if (movable)
            {
                CheckSwipe();
                if (_isTouching)
                {
                    _touchDuration = _touchDuration + Time.deltaTime;
                }
            }
        }
        protected virtual void FixedUpdate()
        {
            if (moving)
            {
                _rb.velocity = playerStats.Speed * movingDirection * _trapMoveK;
            }
        }
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            NewDirection(-movingDirection );
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Wall")
            {
                var vecDirection = _roomPosition - (Vector2) transform.position;
                vecDirection = vecDirection.normalized;
                if (Mathf.Abs(vecDirection.x / vecDirection.y) < 3.5 / 8)
                {
                    NewDirection(new Vector2(0, vecDirection.y > 0 ? 1 : -1));
                }
                else
                {
                    NewDirection(new Vector2( (vecDirection.x > 0 ? 1 : -1),0));
                }
                movable = false;
            } 
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Wall")
            {
                
                movable = true;
            } 
        }

        private async void OnLevelFinished(LevelFinishSignal signal)
        {
            movable = false;
            await UniTask.Delay((int)(signal.Duration * 1000));
            if(!this)
                return;
            movable = true;
            
            transform.position = Vector3.zero;
            movingDirection = Vector2.zero;
        }

        private void OnDestroy()
        {
            SignalBus.RemoveListener<RoomSwitchSignal>(SetPauseMovement);
            SignalBus.RemoveListener<LevelFinishSignal>(OnLevelFinished);
            SignalBus.RemoveListener<EnemyDieSignal>(Vampirism);
        }

        #region MovementProcessing
        public bool movable=true;
        public bool moving=true;
    
        protected Vector2 movingDirection;
        private Vector2 _startTouchPosition;
        private Vector2 _endTouchPosition;
        private float _touchDuration;
        private bool _isTouching=false;
        private float _trapMoveK = 1f;

        private void CheckSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startTouchPosition = Input.mousePosition;
                _isTouching = true;
                _touchDuration = 0;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _endTouchPosition = Input.mousePosition;
                ChangeDirection();
                _isTouching=false;
            }
        
        
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _startTouchPosition = touch.position;
                    _isTouching=true;
                    _touchDuration = 0;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    _endTouchPosition = touch.position;
                    _isTouching=false;
                    ChangeDirection();
                
                }
            }
        }
        private  void ChangeDirection()
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
                        
                        
                        NewDirection(new Vector2(1, 0));
                    }
                    else
                    {
                        // Swiped left
                        
                        
                        NewDirection(new Vector2(-1, 0));
                    }
                }
                else
                {
                    if (swipeDirection.y > 0)
                    {
                        // Swiped up
                        NewDirection(new Vector2(0, 1));
                    }
                    else
                    {
                        // Swiped down
                        NewDirection(new Vector2(0, -1));
                    }
                }
                if (playerStats.SpecialItems.RotHit)
                {
                    if (_readyToBlow) Special();
                }
            }
            else if (_touchDuration < 0.5)// длительность тапа
            {
                if (_readyToBlow)
                {
                    Special();
                    if(playerStats.SpecialItems.DoubleHit)
                        Invoke(nameof(Special),playerStats.AttackSpeed/2 );
                }
            }
        }


        protected virtual void NewDirection(Vector2 direction)
        {
            movingDirection = direction;
            if (direction.y== 0)
            {
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,
                        transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y,
                        transform.localScale.z);
                }
            }
        }

        public void Step()
        {
            playerSound.PlayStep(_trapMoveK<0.9,audioSource[0]);
        }
        
        
        protected float GetAngleFromDirection()
        {
            return Mathf.Atan2(movingDirection.y,movingDirection.x)*Mathf.Rad2Deg;
        }

        private Vector2 _roomPosition;
        private async void SetPauseMovement(RoomSwitchSignal signal)
        {
            _roomPosition=signal.RoomPosition;
            movable = false;
            await UniTask.Delay(500);
            if(!this)
                return;
            movable = true;
            //movingDirection=Vector2.zero;
        }
        #endregion
        #region Attack
        private bool _readyToBlow=true;
        protected virtual async void Special()
        {
            playerSound.PlayBlow(audioSource[1]);
            //Debug.Log("Bam");
            _readyToBlow = false;
            anim.SetTrigger("Atake");
            await UniTask.Delay((int) (1000 * playerStats.AttackSpeed));
            _readyToBlow = true;
        }
        #endregion
        #region DamageAble
        private bool _isInvincible; // if player is invincible after getting damage 

        public async void Damage(float damage)
        {
            if(_isInvincible)
                return;
            anim.SetTrigger("Damage");
            playerSound.PlayGetDamage(audioSource[0]);
            if(Random.value > playerStats.SpecialItems.LuckyChance)
                playerStats.Health -= Mathf.RoundToInt(damage);
            print($"Player got {damage} of damage");
            SignalBus.Invoke(new PlayerHealthChangedSignal(){MaxHealth = playerStats.MaxHealth, Health = playerStats.Health});
            if (playerStats.Health <= 0)
            {
                print("Player should die here.  ((");
                SignalBus.Invoke(new PlayerDeadSignal(){Respawn = Respawn});
                gameObject.SetActive(false);
            }
            ProcessInvincible();
        }
        
        public void Damage(float damage,Vector2 directionReclining)
        {
            if(_isInvincible)
                return;
            Damage(damage);
            RecliningPlayer(directionReclining,damage);
        }

        public void Respawn()
        {
            gameObject.SetActive(true);
            playerStats.Health = playerStats.MaxHealth;
        }


        // this method makes player invincible, waits invincible time and makes player damageable again
        // in future some other instructions can be added
        private async void ProcessInvincible()
        {
            if(_isInvincible)
                return;
            var col = GetComponent<Collider2D>() ;
            col.isTrigger = true;
            _isInvincible = true;
            anim.SetBool("Invincible",_isInvincible);
            await UniTask.Delay((int) (1000 * playerStats.InvincibleTime));
            _isInvincible = false;
            anim.SetBool("Invincible",_isInvincible);
            col.isTrigger = false;
            movable = true;
        }

        

        public async void RecliningPlayer(Vector2 direction, float strength)
        {
            var lastDirection = movingDirection;
            movable = false;
            movingDirection = direction.normalized * strength * playerStats.Reclining;
            await UniTask.Delay((int) (1000 * 0.5));
            movable = true;
            movingDirection = lastDirection;
        }
        
        #endregion
        #region Items

        public void TakeItem(BaseItem item)
        {
            playerSound.PlayCoin(audioSource[2]);
            playerStats.AddItem(item);
            SignalBus.Invoke(new PlayerHealthChangedSignal(){MaxHealth = playerStats.MaxHealth, Health = playerStats.Health});
        }

        private void Vampirism(EnemyDieSignal signal)
        {
            if (playerStats.Health == playerStats.MaxHealth) return;
            if(Random.value > playerStats.SpecialItems.VampirismChance) return;
            playerStats.Health++;
            SignalBus.Invoke(new PlayerHealthChangedSignal(){MaxHealth = playerStats.MaxHealth, Health = playerStats.Health});
        }

        #endregion

        #region Traps

        public async void PitTrap(PitTrap trap)
        {
            Damage(trap.Damage);
            _trapMoveK = 0f;
            transform.position = trap.Center;
            await UniTask.Delay((int) (1000 * trap.PlayerTimeInside));
            transform.position = trap.SpawnPoint;
            _trapMoveK = 1f;
        }

        public void SlowTrap(bool entered, SlowTrap trap)
        {
            if (entered)
            {
                _trapMoveK = trap.SpeedMult;
            }
            else
            {
                _trapMoveK = 1f;
            }
        }

        public void NeedlesTrap(Needles trap)
        {
            Damage(trap.damage);
        }

        #endregion
    }
}
