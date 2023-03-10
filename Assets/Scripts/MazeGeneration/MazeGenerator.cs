using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MazeGeneration
{
    public class MazeGenerator
    {
        public List<MazeCell> CreateMaze(int n)
        {
            var result = new List<MazeCell>(){new MazeCell()};
            for (int i = 0; i < n - 1; i++)
            {
                var possibleToAdd = result
                    .SelectMany(x => new (MazeCell, Direction, MazeCell)[] {
                        (x, Direction.Up, new MazeCell() {Position = x.Position + Vector2Int.up}),
                        (x, Direction.Down, new MazeCell() {Position = x.Position + Vector2Int.down}),
                        (x, Direction.Left, new MazeCell() {Position = x.Position + Vector2Int.left}),
                        (x, Direction.Right, new MazeCell() {Position = x.Position + Vector2Int.right}),})
                    .Where(x => result.FirstOrDefault(addedCell => addedCell.Position == x.Item3.Position) == null)
                    .OrderBy(x => Random.value);
                
                var cellToAdd = possibleToAdd.First();
                var from = cellToAdd.Item1;
                var to = cellToAdd.Item3;
                
                switch (cellToAdd.Item2)
                {
                    case Direction.Up:
                        from.UpWall = true;
                        to.DownWall = true;
                        break;
                    case Direction.Down:
                        from.DownWall = true;
                        to.UpWall = true;
                        break;
                    case Direction.Left:
                        from.LeftWall = true;
                        to.RightWall = true;
                        break;
                    case Direction.Right:
                        from.RightWall = true;
                        to.LeftWall = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                to.Distance = from.Distance + 1;
                result.Add(to);
            }
            
            return result;
        }

        private enum Direction
        {
            Up, Down, Left, Right
        }
    }
    
}