using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace WarlockDataTypes
{
    public class CityData
    {
        public string CityID;
        public int WorldMapXCoord;
        public int WorldMapYCoord;
    }

    public class BattleData
    {
        public string BattleID;
        public int WorldMapXCoord;
        public int WorldMapYCoord;
    }
}
