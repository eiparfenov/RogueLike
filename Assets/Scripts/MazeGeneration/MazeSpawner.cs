using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using RoomBehaviour;
using Signals;
using UnityEngine;
using Utils.Signals;
using Random = UnityEngine.Random;

namespace MazeGeneration
{
    public class MazeSpawner: MonoBehaviour
    {
        private readonly MazeGenerator _mazeGenerator = new MazeGenerator();

        [SerializeField] private Transform mazeParent;
        [SerializeField] private Vector3 roomSize;
        
        [Expandable][SerializeField] private MazeSpawnerTheme[] mazeSpawnerTheme;
        [SerializeField] private int currentLevel;
        

        private void Start()
        {
            CreateMaze();
            SignalBus.AddListener<LevelFinishSignal>(OnLevelFinished);
        }

        [Button]
        private async void CreateMaze()
        {
            if (mazeParent)
            {
                DestroyImmediate(mazeParent.gameObject);
            }
            mazeParent = new GameObject("Maze parent").transform;

            var mazeRooms = mazeSpawnerTheme[currentLevel].MazeRooms
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
                var mazeRoom = roomToAdd.mazeRoom.OrderBy(x => Random.value).First();

                var createdRoom = Instantiate(
                    mazeRoom,
                    Vector3.up * roomSize.y * cell.Position.y + Vector3.right * roomSize.x * cell.Position.x,
                    Quaternion.identity,
                    mazeParent);
                
                createdRoom.ReplaceWalls(
                    SelectWall(mazeSpawnerTheme[currentLevel].WallsLeft, mazeSpawnerTheme[currentLevel].DoorLeft, cell.LeftWall),
                    SelectWall(mazeSpawnerTheme[currentLevel].WallsRight, mazeSpawnerTheme[currentLevel].DoorRight, cell.RightWall),
                    SelectWall(mazeSpawnerTheme[currentLevel].WallsUp, mazeSpawnerTheme[currentLevel].DoorUp, cell.UpWall),
                    SelectWall(mazeSpawnerTheme[currentLevel].WallsDown, mazeSpawnerTheme[currentLevel].DoorDown, cell.DownWall)
                );

                createdRoom.cellData = cell;
                Debug.Log(cell);
                cellsToAdd.Remove(cell);

                await UniTask.Yield(PlayerLoopTiming.Update);
                var spawners = FindObjectsOfType<EnemySpawner>();
                foreach (var enemySpawner in spawners)
                {
                    enemySpawner.DefaultItemsSet = mazeSpawnerTheme[currentLevel].ItemsSet;
                }
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

        private async void OnLevelFinished(LevelFinishSignal signal)
        {
            await UniTask.Delay((int) (signal.Duration * 1000));
            currentLevel++;
            if (currentLevel >= mazeSpawnerTheme.Length)
            {
                return;
            }
            CreateMaze();
        }

        private void OnDestroy()
        {
            SignalBus.RemoveListener<LevelFinishSignal>(OnLevelFinished);

        }
    }
}