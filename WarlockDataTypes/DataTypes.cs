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
    public enum WorldMapObjectType
    {
        City = 0,
        Battle = 1,
    }

    public class WorldMapObjectData
    {
        public WorldMapObjectType ObjectType;
        public string ObjectID;
        public string WorldMapAssetName;
        public int WorldMapXCoord;
        public int WorldMapYCoord;
    }

    public class WorldMapData
    {
        public int Height;
        public int Width;
    }

    public class WorldMapTileData
    {
        public string TileAssetName;
        public int TileXCoord;
        public int TileYCoord;
    }

    public enum BattleObjectType
    {
        Player = 0,
        Enemy = 1,
    }

    public class BattleObjectData
    {
        public BattleObjectType ObjectType;
        public string BattleObjectAssetName;
        public int BattleXCoord;
        public int BattleYCoord;
    }

    public class EnemyData
    {
        public string BattleImageAsset;
        public int HitPoints;
    }

    public class CityData
    {
        public string ObjectID;
        // other city data
    }
}
