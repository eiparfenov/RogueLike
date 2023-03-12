using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RoomBehaviour.Traps
{
    public class SawTrap: MonoBehaviour, IRoomBehaviour
    {
        [SerializeField] private Saw sawPref;
        [SerializeField] private Transform[] sawPath;
        [SerializeField] private float sawSpeed;
        [SerializeField] private int damage;
        private Saw _saw;
        private bool _active;

        private async UniTask MoveSaw(int i)
        {
            var progress = 0f;
            var from = sawPath[i].transform.position;
            var to = sawPath[(i + 1) % sawPath.Length].position;
            var time = (from - to).magnitude / sawSpeed;

            while (progress < 1)
            {
                progress += Time.deltaTime / time;
                _saw.transform.position = Vector3.Lerp(from, to, progress);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        private async UniTask PerformMotion()
        {
            _saw = Instantiate(sawPref, transform);
            _saw.damage = damage;
            var i = 0;
            while (_active)
            {
                await MoveSaw(i);
                i++;
                i %= sawPath.Length;
            }
            Destroy(_saw);
        }

        public void OnRoomEnteredEarly(Transform player)
        {
            _active = true;
            PerformMotion();
        }

        public void OnRoomEnteredLate()
        {
            
        }

        public void OnRoomExited()
        {
            _active = false;
        }

        public bool Finished => true;
    }
}