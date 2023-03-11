using RoomBehaviour.Traps;

namespace Interfaces
{
    public interface ISlowTrapInteracting
    {
        void SlowTrap(bool entered, SlowTrap trap);
    }
}