using MazeGeneration;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MinimapCell:MonoBehaviour
    {
        [SerializeField] private Color path;
        [SerializeField] private Color wall;
        [SerializeField] private Vector2 size;
        [Space] 
        [SerializeField] private Image left;
        [SerializeField] private Image right;
        [SerializeField] private Image up;
        [SerializeField] private Image down;

        public Vector2 Size => size;

        public void ShowCell(MazeCell cell)
        {
            left.color = cell.LeftWall ? path : wall;
            right.color = cell.RightWall ? path : wall;
            up.color = cell.UpWall ? path : wall;
            down.color = cell.DownWall ? path : wall;

            transform.localPosition = Vector3.right * size.x * cell.Position.x + Vector3.up * size.y * cell.Position.y;
        }
    }
}