using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock
{
    interface IInteractable
    {
        // return true if this gesture shouldn't propogate further
        bool InteractGesture(GestureSample gesture);

        void InteractLocation(TouchLocation touchLocation);
    }
}
