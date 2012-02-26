using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warlock
{
    public enum GameModeIndex
    {
        Splash = 0,
        World = 1,
        City = 2,
        Battle = 3,
        Count = 4
    }

    interface IGameMode
    {
        void Initialize();

        void Update();

        void LoadContent();

        void Draw();
    }
}
