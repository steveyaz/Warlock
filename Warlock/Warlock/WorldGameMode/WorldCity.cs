using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Warlock
{
    public class WorldCity : WorldObjectBase
    {
        public WorldCity(string assetName, string cityID, int x, int y) : base(assetName, cityID, x, y) { }

        public override void PlayerEnter()
        {
            CityGameMode cityGameMode = new CityGameMode();
            cityGameMode.CityAssetName = base.ObjectID;
            WarlockGame.Instance.ChangeGameMode(cityGameMode);
        }
    }
}
