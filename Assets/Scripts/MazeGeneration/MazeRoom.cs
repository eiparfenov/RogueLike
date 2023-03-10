using System;
using System.Linq;
using System.Transactions;
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
        [Foldout("Walls")] [SerializeField] private Transform wallLeft;
        [Foldout("Walls")] [SerializeField] private Transform wallRight;
        [Foldout("Walls")] [SerializeField] private Transform wallUp;
        [Foldout("Walls")] [SerializeField] private Transform wallDown;

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

        private void SwapWall(Transform wallSpawnPosition, GameObject wallToReplace)
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
            
            SignalBus.Invoke(new RoomSwitchSignal(){RoomPosition = transform.position});

            await UniTask.Delay(2000);
            foreach (var door in GetComponentsInChildren<Door>())
            {
                door.Close();
            }
            var roomBehaviours = GetComponentsInChildren<IRoomBehaviour>();
            print(roomBehaviours.Length);
            foreach (var roomBehaviour in roomBehaviours)
            {
                roomBehaviour.OnRoomEntered(other.transform);
            }

            await UniTask.WaitUntil(() => roomBehaviours.All(x => x.Finished));
            
            foreach (var door in GetComponentsInChildren<Door>())
            {
                door.Open();
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