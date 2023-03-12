using System;

namespace Signals
{
    public class PlayerDeadSignal
    {
        public Action Respawn { get; set; }
    }
}