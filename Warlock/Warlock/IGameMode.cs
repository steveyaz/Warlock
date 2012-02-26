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
        Count = 4,
        Exit = 5
    };

    public enum CityEnum
    {
        Albador = 1,
        Hibador = 2,
        Midador = 3
    };

    interface IGameMode : IDrawable
    {
        void Initialize();

        void Update();

        void LoadContent();
    }
}
