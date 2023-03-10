using UnityEngine;

namespace RoomBehaviour
{
    public class Door: MonoBehaviour
    {
        [SerializeField] private GameObject doorFrame;

        public async void Close()
        {
            doorFrame.SetActive(true);
        }
        
        public async void Open()
        {
            doorFrame.SetActive(false);
        }
    }
}