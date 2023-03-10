using UnityEngine;

namespace RoomBehaviour
{
    public interface IRoomBehaviour
    {
        void OnRoomEntered(Transform player);
        void OnRoomExited();
    }
}