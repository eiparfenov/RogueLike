using System;
using Items;
using NaughtyAttributes;
using UnityEngine;

namespace MazeGeneration
{
    [CreateAssetMenu(fileName = "MazeSpawnerTheme", menuName = "Custom/Maze/SpawnerTheme", order = 0)]
    public class MazeSpawnerTheme : ScriptableObject
    {
        [field: SerializeField]public MazeRoomContent[] MazeRooms { get; private set; }
        [field: SerializeField][field: Foldout("Walls")]public GameObject[] WallsRight { get; private set; }
        [field: SerializeField][field: Foldout("Walls")]public GameObject[] WallsLeft { get; private set; }
        [field: SerializeField][field: Foldout("Walls")]public GameObject[] WallsUp { get; private set; }
        [field: SerializeField][field: Foldout("Walls")]public GameObject[] WallsDown { get; private set; }
        [field: SerializeField][field: Foldout("Doors")]public GameObject[] DoorRight { get; private set; }
        [field: SerializeField][field: Foldout("Doors")]public GameObject[] DoorLeft { get; private set; }
        [field: SerializeField][field: Foldout("Doors")]public GameObject[] DoorUp { get; private set; }
        [field: SerializeField][field: Foldout("Doors")]public GameObject[] DoorDown { get; private set; }
        [field: SerializeField] public ItemsSet ItemsSet { get; private set; }
    }

    [Serializable]
    public class MazeRoomContent
    {
        public MazeRoom[] mazeRoom;
        public int count;
        public SpawnPriority spawnPriority;
    }

    public enum SpawnPriority
    {
        Farthest, Closest, Random 
    }
}