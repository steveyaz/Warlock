using System.Collections.Generic;

namespace Warlock.WorldGameModeNS
{
    public class Battle : WorldObjectBase
    {
        public Battle()
        {
            ContextMenuItems = new Dictionary<string, ScreenObjectTapDelegate>();
            ContextMenuItems.Add("Enter Battle", EnterBattle);
            TapDelegate = ExecuteTap;
        }

        public void EnterBattle()
        {
            WarlockGame.Instance.ChangeGameMode(new BattleGameMode.BattleGameMode(ObjectID));
        }

        public void ExecuteTap()
        {
            WorldGameMode.m_Instance.MarkDestination(this);
            WorldGameMode.m_Instance.MovePlayer(WorldPosition);
        }
    }
}
