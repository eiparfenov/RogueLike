using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Enemies
{
    public class WalkingSkeleton: BaseEnemy
    {
        [SerializeField] private float reloadTime = 4f;
        [SerializeField] private float arrowSpeed;
        [SerializeField] private Arrow arrowPref;
        [SerializeField] private AudioClip arrowClip;
        private bool _reloaded = true;
        public void Update()
        {
            var mainDir = new[] {Vector3.up, Vector3.down, Vector3.right, Vector3.left}
                .OrderByDescending(x => Vector3.Dot(DirectionToPlayer, x))
                .First();
            
            if (_reloaded && Vector3.Dot(DirectionToPlayer, moveDirection) < 0)
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
                audio.clip = arrowClip;
                audio.Play();
                arrow.speed = arrowSpeed;
                arrow.moveDirection = mainDir.normalized;
                arrow.damage = enemyStats.Damage;
                Reload();
            }
            
            
            var secDir = Vector3.Cross(mainDir, Vector3.forward);
            secDir *= Mathf.Sign(Vector3.Dot(secDir, DirectionToPlayer));
            anim.SetFloat("VerticalDirection",mainDir.y);
            moveDirection = secDir;
            if (mainDir.x > 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y,
                    transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,
                    transform.localScale.z);
            }
            
        }

        protected override bool isFlyingEnemy => false;

        private async void Reload()
        {
            _reloaded = false;
            anim.SetBool("ReadyToShoot",_reloaded);
            await UniTask.Delay((int) (reloadTime * 1000));
            if (!this)
                return;
            _reloaded = true;
            anim.SetBool("ReadyToShoot",_reloaded);
        }
    }
}