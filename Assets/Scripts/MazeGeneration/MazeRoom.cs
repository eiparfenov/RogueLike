using System;
using System.Transactions;
using NaughtyAttributes;
using RoomBehaviour;
using UnityEngine;

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

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            var roomBehaviours = GetComponentsInChildren<IRoomBehaviour>();
            foreach (var roomBehaviour in roomBehaviours)
            {
                roomBehaviour.OnRoomEntered(other.transform);
            }
        }
    }
}