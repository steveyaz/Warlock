using System.Collections.Generic;

namespace Warlock.WorldGameModeNS
{
    public class City : WorldObjectBase
    {
        public City()
        {
            ContextMenuItems = new Dictionary<string, ScreenObjectTapDelegate>();
            ContextMenuItems.Add("Enter City", EnterCity);
            TapDelegate = ExecuteTap;
        }

        public void EnterCity()
        {
            CityGameMode cityGameMode = new CityGameMode();
            cityGameMode.CityAssetName = base.ObjectID;
            WarlockGame.Instance.ChangeGameMode(cityGameMode);
        }

        public void ExecuteTap()
        {
            WorldGameMode.m_Instance.MarkDestination(this);
            WorldGameMode.m_Instance.MovePlayer(WorldPosition);
        }
    }
}
