using System.Linq;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using RoomBehaviour;
using Signals;
using UnityEngine;
using Utils.Signals;

namespace MazeGeneration
{
    public class MazeRoom: MonoBehaviour
    {
        [Foldout("Walls")] [SerializeField] protected Transform wallLeft;
        [Foldout("Walls")] [SerializeField] protected Transform wallRight;
        [Foldout("Walls")] [SerializeField] protected Transform wallUp;
        [Foldout("Walls")] [SerializeField] protected Transform wallDown;
        public MazeCell cellData;

        public void ReplaceWalls(
            GameObject toReplaceWallLeft,
            GameObject toReplaceWallRight,
            GameObject toReplaceWallUp,
            GameObject toReplaceWallDown)
        {
            SwapWall(wallLeft, toReplaceWallLeft);
            SwapWall(wallRight, toReplaceWallRight);
            SwapWall(wallDown, toReplaceWallDown);
            SwapWall(wallUp, toReplaceWallUp);
        }

        protected void SwapWall(Transform wallSpawnPosition, GameObject wallToReplace)
        {
            DestroyImmediate(wallSpawnPosition.GetChild(0).gameObject);
            Instantiate(wallToReplace, wallSpawnPosition);
        }

        private async void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }
            
            SignalBus.Invoke(new RoomSwitchSignal(){RoomPosition = transform.position, CellData = cellData});
            
            var roomBehaviours = GetComponentsInChildren<IRoomBehaviour>();
            foreach (var roomBehaviour in roomBehaviours)
            {
                roomBehaviour.OnRoomEnteredEarly(other.transform);
            }
            
            await UniTask.Delay(1500);
            if(!this)
                return;
            
            
            foreach (var roomBehaviour in roomBehaviours)
            {
                roomBehaviour.OnRoomEnteredLate();
            }

            if (!roomBehaviours.All(x => x.Finished))
            {
                SignalBus.Invoke(new FightSignal(){InProgress = true});
                foreach (var door in GetComponentsInChildren<Door>())
                {
                    door.Close();
                }
                await UniTask.WaitUntil(() => roomBehaviours.All(x => x.Finished));
                if (!this)
                    return;

                foreach (var door in GetComponentsInChildren<Door>())
                {
                    door.Open();
                }
                SignalBus.Invoke(new FightSignal(){InProgress = false});
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            var roomBehaviours = GetComponentsInChildren<IRoomBehaviour>();
            print(roomBehaviours.Length);
            foreach (var roomBehaviour in roomBehaviours)
            {
                roomBehaviour.OnRoomExited();
            }
        }
    }
}