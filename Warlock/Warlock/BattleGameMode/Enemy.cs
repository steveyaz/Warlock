using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarlockDataTypes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Warlock.BattleGameMode
{
    public class Enemy : BattleObjectBase
    {
        public string EnemyAsset { get; set; }
        private Vector2 HP_BAR_OFFSET = new Vector2() { X = -10, Y = 0 };
        private Vector2 HP_TILE_OFFSET = new Vector2() { X = -8, Y = 2 };
        private const int HP_TILE_HEIGHT = 5;

        public Enemy()
        {
            TapDelegate = ExecuteTap;
        }

        public override void Draw()
        {
            WarlockGame.Batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            WarlockGame.Batch.Draw(WarlockGame.TextureDictionary["hp_bar"], ScreenPosition + HP_BAR_OFFSET, Color.White);

            Vector2 tilePosition = ScreenPosition + HP_TILE_OFFSET;
            for (int i = 10; i > 0; i--)
            {
                if (HitPoints * 10 / OrigHitPoints >= i)
                {
                    WarlockGame.Batch.Draw(WarlockGame.TextureDictionary["hp_full"], tilePosition, Color.White);
                }
                else
                {
                    WarlockGame.Batch.Draw(WarlockGame.TextureDictionary["hp_empty"], tilePosition, Color.White);
                }
                tilePosition.Y += HP_TILE_HEIGHT;
            }

            WarlockGame.Batch.End();
            base.Draw();
        }

        public override void LoadContent()
        {
            WarlockGame.Instance.EnsureTexture("hp_bar");
            WarlockGame.Instance.EnsureTexture("hp_full");
            WarlockGame.Instance.EnsureTexture("hp_empty");
            WarlockGame.Instance.EnsureTexture(AssetName + "_dead");
            base.LoadContent();
        }

        public void Initialize()
        {
            EnemyData enemyData = WarlockGame.Instance.Content.Load<EnemyData>(@EnemyAsset);
            AssetName = enemyData.BattleImageAsset;
            HitPoints = enemyData.HitPoints;
            OrigHitPoints = enemyData.HitPoints;
        }

        public void ExecuteTap()
        {
            BattleGameMode.m_Instance.ExecuteTap(this);
        }
    }
}
