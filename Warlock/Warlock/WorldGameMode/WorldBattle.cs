using System.Collections.Generic;

namespace Warlock.WorldGameModeNS
{
    public class WorldBattle : WorldObjectBase
    {
        public WorldBattle()
        {
            ContextMenuItems = new Dictionary<string, ScreenObjectTapDelegate>();
            ContextMenuItems.Add("Enter Battle", EnterBattle);
            TapDelegate = ExecuteTap;
        }

        public void EnterBattle()
        {
            WarlockGame.Instance.ChangeGameMode(new BattleGameMode());
        }

        public void ExecuteTap()
        {
            WorldGameMode.m_Instance.MarkDestination(this);
            WorldGameMode.m_Instance.MovePlayer(WorldPosition);
        }
    }
}
