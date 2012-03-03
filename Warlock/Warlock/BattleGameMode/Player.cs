using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Warlock.BattleGameMode
{
    public class Player : BattleObjectBase
    {
        private SpellBase m_casting;
        private int m_castTime;

        public bool IsCasting { get; set; }

        public Player()
        {
            AssetName = "graywizard";
        }

        public override void Update()
        {
            if (IsCasting && m_castTime == 2 * WarlockGame.FPS)
            {
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
            m_casting = new SpellBase()
            {
                AssetName = spellName
            };
            m_castTime = 0;
            AssetName = "graywizard_cast_1";
        }

        public void ExecuteSpell(BattleObjectBase occupyingObject)
        {
            occupyingObject.FDraw = false;
            AssetName = "graywizard";
            IsCasting = false;
            BattleGameMode.m_Instance.CheckVictoryConditions();
        }
    }
}
