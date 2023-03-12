using System;
using Cysharp.Threading.Tasks;
using RoomBehaviour;
using UnityEngine;

namespace MazeGeneration
{
    public class FinishMazeRoom: MazeRoom
    {
        [SerializeField] private GameObject exitWallRight;
        [SerializeField] private GameObject exitWallLeft;
        [SerializeField] private GameObject exitWallUp;
        [SerializeField] private GameObject exitWallDown;
        private async void Start()
        {
            await UniTask.Yield(PlayerLoopTiming.Update);
            await UniTask.Yield(PlayerLoopTiming.Update);
            await UniTask.Yield(PlayerLoopTiming.Update);
            if (!this) return;
            if (wallDown.GetComponentInChildren<Door>()) SwapWall(wallUp, exitWallUp);
            else if (wallUp.GetComponentInChildren<Door>()) SwapWall(wallDown, exitWallDown);
            else if (wallRight.GetComponentInChildren<Door>()) SwapWall(wallLeft, exitWallLeft);
            else if (wallLeft.GetComponentInChildren<Door>()) SwapWall(wallRight, exitWallRight);
            
        }
    }
}