using System;
using Signals;
using UnityEngine;
using Utils.Signals;

namespace RoomBehaviour
{
    public class Finish: Door
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!col.CompareTag("Player")) return;
            SignalBus.Invoke(new LevelFinishSignal());
        }

        public override void Close()
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

        public override void Open()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}