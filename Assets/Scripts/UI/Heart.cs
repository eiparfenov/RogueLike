using UnityEngine;

namespace UI
{
    public class Heart:MonoBehaviour
    {
        [SerializeField] private GameObject heart;
        [SerializeField] private GameObject background;

        public HeartState State
        {
            set
            {
                background.SetActive(value == HeartState.Hit || value == HeartState.On);
                heart.SetActive(value == HeartState.On);
            }
        }
        

        public enum HeartState
        {
            Off, Hit, On
        }
    }
    
}