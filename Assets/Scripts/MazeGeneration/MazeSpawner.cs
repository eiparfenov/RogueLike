using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace MazeGeneration
{
    public class MazeSpawner: MonoBehaviour
    {
        private readonly MazeGenerator _mazeGenerator = new MazeGenerator();

        [SerializeField] private Transform mazeParent;
        [SerializeField] private Vector3 roomSize;
        [Expandable][SerializeField] private MazeSpawnerTheme mazeSpawnerTheme;

        [Button]
        private void CreateMaze()
        {
            if (mazeParent)
            {
                DestroyImmediate(mazeParent.gameObject);
            }
            mazeParent = new GameObject("Maze parent").transform;

            var mazeRooms = mazeSpawnerTheme.MazeRooms
                .Select(x => new MazeRoomContent(){count = x.count, mazeRoom = x.mazeRoom, spawnPriority = x.spawnPriority})
                .ToArray();
            
            
            var totalRoomsCount = mazeRooms
                .Select(x => x.count)
                .Sum();
            
            Debug.Log(totalRoomsCount);
            
            var cellsToAdd = _mazeGenerator.CreateMaze(totalRoomsCount);

            for (int i = 0; i < totalRoomsCount; i++)
            {
                var roomToSpawnFarthest = mazeRooms.FirstOrDefault(x => x.spawnPriority == SpawnPriority.Farthest && x.count > 0);
                var roomToSpawnClosest = mazeRooms.FirstOrDefault(x => x.spawnPriority == SpawnPriority.Closest && x.count > 0);
                
                MazeRoomContent roomToAdd;
                MazeCell cell;
                if (roomToSpawnClosest != null)
                {
                    roomToAdd = roomToSpawnClosest;
                    cell = cellsToAdd
                        .OrderBy(x => x.Distance)
                        .FirstOrDefault();
                }
                else if (roomToSpawnFarthest !=  null)
                {
                    roomToAdd = roomToSpawnFarthest;
                    cell = cellsToAdd
                        .Where(IsCornerCell)
                        .OrderByDescending(x => x.Distance)
                        .ThenBy(x => Random.value)
                        .FirstOrDefault();
                    if (cell == null)
                    {
                        cell = cellsToAdd
                            .OrderBy(x => Random.value)
                            .ThenByDescending(x => x.Distance)
                            .FirstOrDefault();
                    }
                }
                else
                {
                    roomToAdd = mazeRooms
                        .Where(x => x.count > 0)
                        .OrderBy(x => Random.value)
                        .First();
                    cell = cellsToAdd.OrderBy(x => Random.value).First();
                }

                roomToAdd.count -= 1;

                var createdRoom = Instantiate(
                    roomToAdd.mazeRoom,
                    Vector3.up * roomSize.y * cell.Position.y + Vector3.right * roomSize.x * cell.Position.x,
                    Quaternion.identity,
                    mazeParent);
                
                createdRoom.ReplaceWalls(
                    SelectWall(mazeSpawnerTheme.WallsLeft, mazeSpawnerTheme.DoorLeft, cell.LeftWall),
                    SelectWall(mazeSpawnerTheme.WallsRight, mazeSpawnerTheme.DoorRight, cell.RightWall),
                    SelectWall(mazeSpawnerTheme.WallsUp, mazeSpawnerTheme.DoorUp, cell.UpWall),
                    SelectWall(mazeSpawnerTheme.WallsDown, mazeSpawnerTheme.DoorDown, cell.DownWall)
                );
                Debug.Log(cell);
                cellsToAdd.Remove(cell);
            }
        }

        private GameObject SelectWall(GameObject[] walls, GameObject[] doors, bool door)
        {
            var collection = door ? doors : walls;
            return collection.OrderBy(x => Random.value).First();
        }

        private bool IsCornerCell(MazeCell cell)
        {
            var res = 0;
            res += cell.DownWall ? 1 : 0;
            res += cell.LeftWall ? 1 : 0;
            res += cell.RightWall ? 1 : 0;
            res += cell.UpWall ? 1 : 0;
            return res == 1;
        }
    }
}