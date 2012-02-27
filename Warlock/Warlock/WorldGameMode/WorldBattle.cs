using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock
{
    public class WorldBattle : WorldObjectBase
    {
        public WorldBattle(string assetName, string battleID, int x, int y) : base(assetName, battleID, x, y) { }

        public override void PlayerEnter()
        {
            WarlockGame.Instance.ChangeGameMode(new BattleGameMode());
        }
    }
}
