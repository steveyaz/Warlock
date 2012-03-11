using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock.BattleGameMode
{
    public class Player : BattleObjectBase
    {
        private int m_castTime;

        public bool IsCasting { get; set; }

        public Player()
        {
            AssetName = "graywizard";
            HitPoints = 10;
            OrigHitPoints = 10;
            Radius = 32;
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

        public override void Update()
        {
            if (IsCasting && m_castTime == 2 * WarlockGame.FPS)
            {
                AssetName = "graywizard";
                IsCasting = false;
                BattleGameMode.m_Instance.CastingFinished();
            }
            else if (IsCasting)
            {
                m_castTime++;
                if (m_castTime % WarlockGame.FPS <= WarlockGame.FPS / 4)
                {
                    AssetName = "graywizard_cast_1";
                }
                else if (m_castTime % WarlockGame.FPS <= WarlockGame.FPS / 2)
                {
                    AssetName = "graywizard_cast_2";
                }
                else if (m_castTime % WarlockGame.FPS <= WarlockGame.FPS * 3 / 4)
                {
                    AssetName = "graywizard_cast_1";
                }
                else
                {
                    AssetName = "graywizard_cast_2";
                }
            }
        }

        public override void LoadContent()
        {
            WarlockGame.Instance.EnsureTexture("graywizard");
            WarlockGame.Instance.EnsureTexture("graywizard_cast_1");
            WarlockGame.Instance.EnsureTexture("graywizard_cast_2");
            base.LoadContent();
        }

        public void CastSpell(string spellName)
        {
            IsCasting = true;
            m_castTime = 0;
            AssetName = "graywizard_cast_1";
        }
    }
}
