using UnityEngine;

namespace RoomBehaviour
{
    public class Door: MonoBehaviour
    {
        [SerializeField] protected GameObject doorFrame;

        public virtual async void Close()
        {
            doorFrame.SetActive(true);
        }
        
        public virtual async void Open()
        {
            doorFrame.SetActive(false);
        }
    }
}