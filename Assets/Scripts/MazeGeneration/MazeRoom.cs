using NaughtyAttributes;
using UnityEngine;

namespace MazeGeneration
{
    public class MazeRoom: MonoBehaviour
    {
        [Foldout("Walls")] [SerializeField] private Transform wallLeft;
        [Foldout("Walls")] [SerializeField] private Transform wallRight;
        [Foldout("Walls")] [SerializeField] private Transform wallUp;
        [Foldout("Walls")] [SerializeField] private Transform wallDown;
        public virtual async void LaunchRoom(){}

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
    }
}