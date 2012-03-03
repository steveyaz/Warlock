using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warlock.BattleGameMode
{
    public class Enemy : BattleObjectBase
    {
        public Enemy()
        {
            TapDelegate = ExecuteTap;
        }

        public void ExecuteTap()
        {
            BattleGameMode.m_Instance.ExecuteTap(this);
        }
    }
}
