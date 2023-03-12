using MazeGeneration;
using UnityEngine;

namespace Signals
{
    public class RoomSwitchSignal
    {
        public MazeCell CellData { get; set; }
        public Vector3 RoomPosition { get; set; }
    }
}