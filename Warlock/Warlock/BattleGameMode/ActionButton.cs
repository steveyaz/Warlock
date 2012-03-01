using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warlock.BattleGameMode
{
    public class ActionButton : ImageScreenObject
    {
        public ActionButton()
        {
            TapDelegate = ExecuteTap;
        }

        public void ExecuteTap()
        {
            BattleGameMode.m_Instance.ExecuteTap(this);
        }
    }
}
