using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

namespace Warlock
{
    interface IInteractable
    {
        void InteractGesture(GestureSample gesture);

        void InteractLocation(TouchLocation touchLocation);
    }
}
