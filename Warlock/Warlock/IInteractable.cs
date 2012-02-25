using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock
{
    interface IInteractable
    {
        void Interact(GestureSample gesture);
    }
}
