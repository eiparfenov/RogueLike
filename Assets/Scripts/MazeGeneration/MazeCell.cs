using UnityEngine;

namespace MazeGeneration
{
    public class MazeCell
    {
        public Vector2Int Position { get; set; }
        public bool RightWall { get; set; }
        public bool DownWall { get; set; }
        public bool LeftWall { get; set; }
        public bool UpWall { get; set; }
        public int Distance { get; set; }

        public override string ToString()
        {
            return $"{Position}: {Distance} {RightWall}, {LeftWall}, {DownWall}, {UpWall}";
        }
    }
}