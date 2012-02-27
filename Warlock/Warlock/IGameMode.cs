using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warlock
{
    public interface IGameMode : IDrawable
    {
        void Initialize();

        void Update();
    }
}
