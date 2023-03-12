using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Player
{
    public class PlayerBerserk: PlayerMovement
    {
        [SerializeField] private Axe axe;
        [SerializeField] private Transform axeHolder;
        [SerializeField] private float rotationSpeed;
        protected async override void Start()
        {
            base.Start();
            axe.playerStats = playerStats;
            await UniTask.Yield(PlayerLoopTiming.Update);
            if (this.enabled)
                axe.gameObject.SetActive(true);
        }

        protected override void Special()
        {
            base.Special();
            rotationSpeed = -rotationSpeed;
        }

        protected override void Update()
        {
            base.Update();
            axe.transform.Rotate(Vector3.forward, rotationSpeed * playerStats.AttackSpeed * Time.deltaTime);
            
        }

        protected override void NewDirection(Vector2 direction)
        {
            base.NewDirection(direction);
            if (direction.y== 0)
            {
                if (direction.x < 0)
                {
                    axeHolder.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    axeHolder.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }
}
