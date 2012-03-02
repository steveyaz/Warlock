using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock.BattleGameMode
{
    public class BattleObjectBase : ImageScreenObject
    {
        // override this to only interact with the base of the object
        public override bool InteractGesture(GestureSample gesture)
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

        public virtual void BattleTimeUnitIncrement() { }
    }
}
