using UnityEngine;

namespace RoomBehaviour
{
    public interface IRoomBehaviour
    {
        void OnRoomEnteredEarly(Transform player);
        void OnRoomEnteredLate();
        void OnRoomExited();
        bool Finished { get; }
    }
}