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

        public override void BattleTimeUnitIncrement()
        {
            if (IsCasting && m_castTime == 0)
            {
                IsCasting = false;
                BattleGameMode.m_Instance.CastingFinished();
            }
            else if (IsCasting)
            {
                m_castTime--;
            }
        }

        public void CastSpell(string spellName)
        {
            IsCasting = true;
            m_casting = new SpellBase()
            {
                AssetName = spellName
            };
            AssetName = "graywizard_cast";
            LoadContent();
            m_castTime = 2;
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
