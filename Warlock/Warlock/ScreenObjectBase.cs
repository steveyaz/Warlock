using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock
{
    public delegate void ScreenObjectTapDelegate();

    public class ScreenObjectBase : IInteractable
    {
        public string AssetName { get; set; }
        public Vector2 ScreenPosition { get; set; }
        public ScreenObjectTapDelegate TapDelegate { get; set; }

        protected Vector2 Size { get; set; }

        public bool InteractGesture(GestureSample gesture)
        {
            if (TapDelegate != null
                && gesture.GestureType == GestureType.Tap
                && gesture.Position.X < ScreenPosition.X + Size.X && gesture.Position.X > ScreenPosition.X
                && gesture.Position.Y < ScreenPosition.Y + Size.Y && gesture.Position.Y > ScreenPosition.Y)
            {
                TapDelegate();
                return true;
            }
            return false;
        }

        public void InteractLocation(TouchLocation touchLocation)
        {
            // Do nothing
        }
    }
}
