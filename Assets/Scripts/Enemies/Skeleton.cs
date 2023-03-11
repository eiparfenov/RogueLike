using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class Skeleton: BaseEnemy
    {
        [SerializeField] private float reloadTime = 4f;
        [SerializeField] private float arrowSpeed;
        [SerializeField] private Arrow arrowPref;

        private async void Start()
        {
            await UniTask.WaitUntil(() => Player);
            await UniTask.WaitUntil(() => Active);
            await UniTask.Delay((int) (1000 * reloadTime * Random.value));
            while (this)
            {
                var arrow = Instantiate(
                    arrowPref, 
                    transform.position, 
                    Quaternion.Euler(Vector3.forward * Vector3.SignedAngle(
                        Vector3.up,
                        DirectionToPlayer,
                        Vector3.forward
                        )
                    )
                );
                arrow.speed = arrowSpeed;
                arrow.moveDirection = DirectionToPlayer;
                arrow.damage = damage;
                await UniTask.Delay((int) (1000 * reloadTime));
            }
        }
    }
}